using ExpediaRapidApi.Sdk;
using ExpediaRapidApi.Sdk.Activities;
using ExpediaRapidApi.Sdk.Activities.Bookings;
using ExpediaRapidApi.Sdk.Activities.Bookings.CancelActivityBooking;
using ExpediaRapidApi.Sdk.Activities.Bookings.CreateActivityBooking;
using ExpediaRapidApi.Sdk.Activities.Bookings.RetrieveActivityBooking;
using ExpediaRapidApi.Sdk.Activities.Content;
using ExpediaRapidApi.Sdk.Activities.Geography;
using ExpediaRapidApi.Sdk.Activities.Shared;
using ExpediaRapidApi.Sdk.Activities.Shopping;
using ExpediaRapidApi.Sdk.Lodging;
using ExpediaRapidApi.Sdk.Shared;
using fbognini.Sdk.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;

// Drives the new Activities client against a stub transport: no credentials, no network.
// Checks the things that are easy to get wrong and that a compiler cannot catch:
// query strings, path vs query parameters, Link pagination, deserialization, the 202 on cancel.

var spy = new SpyHandler();

var settings = new Dictionary<string, string?>
{
    ["ExpediaRapidApiSettings:EnvironmentName"] = "STG",
    ["ExpediaRapidApiSettings:TrafficProfile"] = "Activities_GHC",
    ["ExpediaRapidApiSettings:ApiKey:ApiKey"] = "k",
    ["ExpediaRapidApiSettings:ApiKey:ApiSecret"] = "s",
    ["ExpediaRapidApiSettings:OAuth:ClientId"] = "cars-client",
    ["ExpediaRapidApiSettings:OAuth:ClientSecret"] = "cars-secret",
    ["ExpediaRapidApiSettings:ActivitiesOAuth:ClientId"] = "activities-client",
    ["ExpediaRapidApiSettings:ActivitiesOAuth:ClientSecret"] = "activities-secret",
};

var client = BuildClient(settings);

var failures = 0;

// --- 1. Region: the region id belongs in the path, never in the query ---------------------
spy.Reply("""["1001","1002","1003"]""", link: "<https://test.ean.com/v2/regions/6054439/activities?token=NEXT>; rel=\"next\";", totalResults: 812);

var region = await client.GetRegionActivities(new GetRegionActivitiesRequest
{
    RegionId = "6054439",
    CategoryId = ["12", "34"],
});

Check("region id is a path segment", spy.Path == "/v2/regions/6054439/activities");
Check("region id is not repeated in the query", !spy.Query.Contains("region_id"));
Check("categories are repeated, not indexed", spy.Query.Contains("category_id=12&category_id=34"));
Check("geography paginates", spy.Query.Contains("pagination_size=100"));
Check("ids deserialized", region.Response.Count == 3 && region.Response[0] == "1001");
Check("next link read from rel=\"next\"", region.NextPageLink == "https://test.ean.com/v2/regions/6054439/activities?token=NEXT");
Check("total results read from header", region.TotalResults == 812);

// --- 1b. Activities: the Link header is a bare url on an internal host, only its token is usable ---
spy.Reply("""["1004"]""", link: "https://rapid-activities-shop-main-us-east-1.epsdecaf.prod.exp-aws.net/v2/regions/179899/activities?traffic_profile=GHC&pagination_token=P~ODIgb3VFvVk=");

var tokenized = await client.GetRegionActivities(new GetRegionActivitiesRequest { RegionId = "179899", PaginationSize = 100 });

Check("internal host dropped", tokenized.NextPageLink?.Contains("exp-aws.net") == false);
Check("pagination token carried over to our url", tokenized.NextPageLink!.EndsWith("&pagination_token=P~ODIgb3VFvVk="));
Check("our own parameters survive", tokenized.NextPageLink.Contains("pagination_size=100") && tokenized.NextPageLink.Contains("traffic_profile=Activities_GHC"));
Check("next page is a request the client can issue", tokenized.NextPageLink.StartsWith("v2/regions/179899/activities?"));

// Walking twice must not stack a second token onto the url.
spy.Reply("""["1005"]""", link: "https://rapid-activities-shop-main-us-east-1.epsdecaf.prod.exp-aws.net/v2/regions/179899/activities?pagination_token=SECOND");
var walked = await client.GetRegionIdsByLink(tokenized.NextPageLink);

Check("token sent on the next request", spy.Query.Contains("pagination_token=P~ODIgb3VFvVk="));
Check("token replaced, not appended", walked.NextPageLink!.EndsWith("&pagination_token=SECOND") && !walked.NextPageLink.Contains("P~ODIgb3VFvVk"));

// --- 2. Last page: no Link header means no next page --------------------------------------
spy.Reply("""["1004"]""");
var lastPage = await client.GetRegionActivities(new GetRegionActivitiesRequest { RegionId = "1", PaginationSize = 50 });
Check("last page has no next link", lastPage.NextPageLink is null);
Check("page size is ours to choose", spy.Query.Contains("pagination_size=50"));

// --- 2b. traffic_profile rides on every call, however the url was built -------------------- Rapid wants it everywhere, so it is appended centrally rather than carried by each request object.
Check("traffic profile on a url that already had a query", spy.Query.Contains("&traffic_profile=Activities_GHC"));

spy.Reply("{}");
await client.RetrieveBooking("123", new RetrieveActivityBookingOptions { Customer = new CustomerHeaderOptions("1.2.3.4") });
Check("traffic profile opens the query when there was none", spy.Query == "?traffic_profile=Activities_GHC");

// Left unconfigured, the parameter is simply not sent: it must not turn into an empty one.
var unprofiled = BuildClient(new Dictionary<string, string?>(settings) { ["ExpediaRapidApiSettings:TrafficProfile"] = null });
spy.Reply("""["1004"]""");
await unprofiled.GetRegionActivities(new GetRegionActivitiesRequest { RegionId = "1" });
Check("no traffic profile configured, none sent", !spy.Query.Contains("traffic_profile"));

// --- 3. Content: batching, the delta date and the data types we claim to support ------------
spy.Reply("""
[{"id":"822672","title":"Tuk Tuk by night","duration":"PT4H","cancellation_policy":{"type":"before_start_date","hours":48},
  "category_ids":["7"],"ratings":{"guest":{"count":128,"overall":"4.8"}},
  "booking_question_types":["text","date","measurement"],
  "media":{"images":[{"caption":"poster","links":{"350px":{"method":"GET","href":"https://img/350.jpg"}}}]}}]
""");

var content = await client.GetActivitiesContent(new GetActivitiesContentRequest
{
    ActivityId = ["822672", "822673"],
    Language = "it-IT",
    DateUpdatedStart = new DateOnly(2026, 7, 10),
    SupportedBookingDataTypes = [ActivityBookingDataTypes.Measurement, ActivityBookingDataTypes.Text],
});

Check("activity ids repeated", spy.Query.Contains("activity_id=822672&activity_id=822673"));
Check("language sent", spy.Query.Contains("language=it-IT"));
Check("date_updated_start is snake_case and ISO", spy.Query.Contains("date_updated_start=2026-07-10"));
Check("supported_booking_data_types repeated, not indexed", spy.Query.Contains("supported_booking_data_types=measurement&supported_booking_data_types=text"));
Check("content deserialized", content[0].Title == "Tuk Tuk by night");
Check("cancellation policy enum parsed", content[0].CancellationPolicy?.Type == ActivityCancellationPolicyType.before_start_date);
Check("nested rating parsed", content[0].Ratings?.Guest?.Overall == "4.8");
Check("media links keyed by size", content[0].Media?.Images[0].Links["350px"].Href == "https://img/350.jpg");
Check("booking_question_types parsed", content[0].BookingQuestionTypes.SequenceEqual(["text", "date", "measurement"]));

// --- 4. Reference taxonomy: type is the parent's name -------------------------------------
spy.Reply("""[{"id":"1","name":"Bungy Jumping","type":"Outdoor Activities"}]""");
var categories = await client.GetActivitiesCategories(new GetReferencesRequest { Language = "it-IT", PaginationSize = 100 });
Check("pagination_size sent", spy.Query.Contains("pagination_size=100"));
Check("reference triple parsed", categories.Response[0].Name == "Bungy Jumping" && categories.Response[0].Type == "Outdoor Activities");

// --- 5. Calendar availability: the cheap call that prices a result list --------------------
spy.Reply("""[{"activity_id":"1001","days":[{"date":"2026-08-01","available":true,"from_price":"73.00","currency":"EUR"},{"date":"2026-08-02","available":false}]}]""");

var calendar = await client.GetCalendarAvailability(new GetCalendarAvailabilityRequest
{
    ActivityId = ["1001"],
    Currency = "EUR",
    StartDate = new DateOnly(2026, 8, 1),
    EndDate = new DateOnly(2026, 8, 30),
});

Check("calendar path", spy.Path == "/v2/experiences/activities/calendars/availability");
Check("from_price parsed as decimal", calendar[0].Days[0].FromPriceAsDecimal() == 73.00m);
Check("unavailable day has no price", calendar[0].Days[1].FromPriceAsDecimal() is null);

// --- 5b. Availability: the slots call now has to declare what questions we can handle -------
spy.Reply("""{"activities":[{"id":"1001","dates":[]}],"ticket_details":{}}""");

await client.GetAvailability(new GetAvailabilityRequest
{
    ActivityId = ["1001"],
    Language = "it-IT",
    Currency = "EUR",
    CountryCode = "IT",
    StartDate = new DateOnly(2026, 8, 1),
    EndDate = new DateOnly(2026, 8, 14),
    SupportedBookingDataTypes = [ActivityBookingDataTypes.Selection, ActivityBookingDataTypes.Measurement],
});

Check("availability path", spy.Path == "/v2/experiences/activities/availability");
Check("availability declares the supported data types", spy.Query.Contains("supported_booking_data_types=selection&supported_booking_data_types=measurement"));

// --- 6. Price check: tickets encoding, booking questions, tokens --------------------------- The response is the spec's price-check-with-conditional-booking-questions example, trimmed to what is being asserted: a parent selection with children, and a child gated by a condition.
spy.Reply("""
{"status":"available",
 "offer_pricing":{"totals":{"inclusive":{"value":"34.17","currency":"EUR"},"total_ticket_count":3}},
 "booking_questions":[
   {"id":"height_1","type":"measurement","question":"What is your height?",
    "description":"Required for safety equipment fitting","applies_to":"per_traveler",
    "ticket_ids":["189894","189895"],
    "allowed_options":[{"id":"in","label":"in"},{"id":"cm","label":"cm"}]},
   {"id":"contact_method_1","type":"selection","question":"How would you prefer to be contacted?",
    "applies_to":"per_traveler","allow_multiple":false,
    "children":["contact_email_1"],
    "allowed_options":[{"id":"email","label":"Email"},{"id":"phone","label":"Phone Call"}]},
   {"id":"contact_email_1","type":"email","question":"What is your email address?",
    "applies_to":"per_traveler",
    "conditional":{"parent_question_id":"contact_method_1",
                   "show_when":{"operator":"equals","field":"selected","values":["email"]}}},
   {"id":"emergency_contact_1","type":"text","question":"Who should we contact in an emergency?",
    "applies_to":"per_booking"}],
 "links":{"payment":{"method":"POST","href":"/v2/payments?token=PAYTOKEN"},
          "create":{"method":"POST","href":"/v2/itineraries/activity?token=CREATETOKEN"}}}
""");

var priceCheck = await client.PriceCheck(
    new PriceCheckRequest
    {
        ActivityId = "1001",
        Token = "SHOPTOKEN",
        Tickets = ActivityTickets.Format([new("189894", 2), new("189895", 1), new("189896", 0)]),
    },
    new PriceCheckOptions
    {
        Customer = new CustomerHeaderOptions("1.2.3.4"),
        Test = "standard",
    });

Check("price-check path carries the activity id", spy.Path == "/v2/experiences/activities/1001/price-check");
Check("activity id not duplicated in query", !spy.Query.Contains("activity_id"));
Check("tickets encoded as id-count, zeros dropped", spy.Query.Contains("tickets=189894-2&tickets=189895-1") && !spy.Query.Contains("189896"));
Check("shopping token sent", spy.Query.Contains("token=SHOPTOKEN"));
Check("Customer-Ip header sent", spy.Headers.Contains("Customer-Ip: 1.2.3.4"));
Check("Test header sent", spy.Headers.Contains("Test: standard"));
Check("status parsed", priceCheck.Status == ActivityPriceCheckStatus.available);
Check("total parsed", priceCheck.OfferPricing?.Totals?.Inclusive?.ToDecimal() == 34.17m);
Check("payment token extracted from link", priceCheck.Links?.Payment?.GetToken() == "PAYTOKEN");
Check("create token extracted from link", priceCheck.Links?.Create?.GetToken() == "CREATETOKEN");

// The booking questions ARE the dynamic form: everything the form renders is asserted here.
var height = priceCheck.BookingQuestions.Single(x => x.Id == "height_1");
var contactMethod = priceCheck.BookingQuestions.Single(x => x.Id == "contact_method_1");
var contactEmail = priceCheck.BookingQuestions.Single(x => x.Id == "contact_email_1");
var emergency = priceCheck.BookingQuestions.Single(x => x.Id == "emergency_contact_1");

Check("questions drive the dynamic form", priceCheck.BookingQuestions.Count == 4);
Check("question type is the widget", height.Type == ActivityBookingDataTypes.Measurement);
Check("question text comes localized", height.Question == "What is your height?");
Check("measurement units come from allowed_options", height.AllowedOptions.Select(x => x.Id).SequenceEqual(["in", "cm"]));
Check("ticket_ids scope a question to some travellers", height.TicketIds.SequenceEqual(["189894", "189895"]));
Check("a question with no ticket_ids applies to all", contactMethod.TicketIds.Count == 0);
Check("per_traveler recognized", height.AppliesTo == BookingQuestionAppliesTo.PerTraveler);
Check("per_booking recognized", emergency.AppliesTo == BookingQuestionAppliesTo.PerBooking);
Check("selection options parsed", contactMethod.AllowedOptions.Select(x => x.Label).SequenceEqual(["Email", "Phone Call"]));
Check("allow_multiple parsed", !contactMethod.AllowMultiple);
Check("children declared on the parent", contactMethod.Children.SequenceEqual(["contact_email_1"]));
Check("child is gated by a condition", contactEmail.Conditional?.ParentQuestionId == "contact_method_1");
Check("condition operator parsed", contactEmail.Conditional?.ShowWhen?.Operator == BookingQuestionOperators.Equal);
Check("condition field parsed", contactEmail.Conditional?.ShowWhen?.Field == BookingQuestionAnswers.SelectedField);
Check("condition values parsed", contactEmail.Conditional?.ShowWhen?.Values.SequenceEqual(["email"]) == true);
Check("an unconditional question has no conditional", height.Conditional is null);

// --- 7. sold_out and price_changed must be readable, not exceptions ------------------------
spy.Reply("""{"status":"sold_out"}""");
var soldOut = await client.PriceCheck(new PriceCheckRequest { ActivityId = "1", Token = "t" }, new PriceCheckOptions());
Check("sold_out parsed", soldOut.Status == ActivityPriceCheckStatus.sold_out);

// --- 7b. Data types catalogue: the reference that keeps our whitelist honest ---------------- Body is trimmed from the live response: an object wrapping the two lists, not a bare array of types.
spy.Reply("""
{"data_types":[
  {"type":"measurement","name":"Measurement",
   "description":"Used for collecting any measurement with a numeric value and unit.",
   "date_added":"2026-07-14",
   "schema_definition":{"type":"object","required":["value","unit"],
                        "properties":{"value":{"type":"string"},"unit":{"type":"string"}}},
   "validation_rules_options":{"value":["min_value","max_value"]}},
  {"type":"selection","name":"Multiple Choice Selection",
   "date_added":"2026-07-14",
   "schema_definition":{"type":"object","required":["selected"],
                        "properties":{"selected":{"type":"array","items":{"type":"string"}}}}}],
 "validation_rules":{"max_length":{"description":"Maximum length of the string value.","type":"integer"}}}
""");

var catalog = await client.GetBookingQuestionDataTypes(new GetBookingQuestionDataTypesRequest
{
    DateUpdatedStart = new DateOnly(2026, 7, 10),
});

Check("data types path", spy.Path == "/v2/experiences/booking-questions/data-types");
Check("data types accept a delta date", spy.Query.Contains("date_updated_start=2026-07-10"));
Check("catalogue parsed", catalog.DataTypes.Count == 2);

var measurementType = catalog.DataTypes.Single(x => x.Type == ActivityBookingDataTypes.Measurement);
Check("data type parsed", measurementType.Name == "Measurement");
Check("date_added parsed", measurementType.DateAdded == new DateOnly(2026, 7, 14));
Check("schema_definition kept raw", measurementType.SchemaDefinition.ContainsKey("properties"));
Check("validation_rules_options parsed", measurementType.ValidationRulesOptions["value"].SequenceEqual(["min_value", "max_value"]));
Check("a type with nothing to validate has none", catalog.DataTypes.Single(x => x.Type == ActivityBookingDataTypes.Selection).ValidationRulesOptions.Count == 0);
Check("validation rules catalogue parsed", catalog.ValidationRules["max_length"].Type == "integer");

// The answer shapes the catalogue documents, built through the helpers rather than by hand.
var measurementAnswer = BookingQuestionAnswers.Measurement("height_1", "72", "in");
var singleAnswer = BookingQuestionAnswers.Selected("contact_method_1", "email");
var multiAnswer = BookingQuestionAnswers.Selected("contact_method_1", ["email", "phone"]);
var booleanAnswer = BookingQuestionAnswers.Boolean("pregnant_1", false);
var integerAnswer = BookingQuestionAnswers.Integer("age_1", 42);
var phoneAnswer = BookingQuestionAnswers.Phone("pickup_phone_1", "39", "3331234567");
var documentAnswer = BookingQuestionAnswers.TravelDocument("passport_1", "YA1234567", "IT", new DateOnly(2030, 5, 1));
Check("measurement answer is value + unit", measurementAnswer.Answer["value"] as string == "72" && measurementAnswer.Answer["unit"] as string == "in");
Check("a single selection is still a list", singleAnswer.Answer["selected"] is List<string> { Count: 1 });
Check("multi selection answer is a list", multiAnswer.Answer["selected"] is List<string> { Count: 2 });
Check("boolean answer is a real boolean", booleanAnswer.Answer["value"] is false);
Check("integer answer is a real number", integerAnswer.Answer["value"] is 42);
Check("phone answer is split from its calling code", phoneAnswer.Answer["country_code"] as string == "39" && phoneAnswer.Answer["number"] as string == "3331234567");
Check("travel document dates are ISO", documentAnswer.Answer["expiration_date"] as string == "2030-05-01");
Check("travel document omits what was not asked", !documentAnswer.Answer.ContainsKey("given_name"));

// --- 7c. Create booking: a PSD2 challenge is not a booking ---------------------------------
var createOptions = new CreateActivityBookingOptions { Customer = new CustomerHeaderOptions("1.2.3.4"), Test = "standard" };
var createRequest = new CreateActivityBookingRequest
{
    AffiliateReferenceId = "GHC123",
    Email = "mario.rossi@example.com",
    PaymentToken = "PAYMENTTOKEN",
    PrimaryTraveler = new PrimaryTraveler
    {
        TicketId = "189894",
        Name = new TravelerName { GivenName = "Mario", FamilyName = "Rossi" },
        Phone = new Phone { CountryCode = "39", Number = "3331234567" },
        BookingQuestionAnswers = [BookingQuestionAnswers.Measurement("height_1", "180", "cm")],
    },
    BookingQuestionAnswers = [BookingQuestionAnswers.Value("emergency_contact_1", "Jane Doe, +39 555 1234")],
};

// The body of the spec's create-successful example: a confirmed booking, nothing to complete.
spy.Reply("""
{"itinerary_id":"0000000000000",
 "links":{"retrieve":{"method":"GET","href":"/v2/itineraries/0000000000000/activity","expires":""}}}
""", HttpStatusCode.Created);

var created = await client.CreateBooking("CREATETOKEN", createRequest, createOptions);

Check("create path", spy.Path == "/v2/itineraries/activity");
Check("create token sent", spy.Query.Contains("token=CREATETOKEN"));
Check("itinerary id parsed", created.ItineraryId == "0000000000000");
Check("a plain create carries no challenge", !created.RequiresPaymentChallenge);

// The body of the spec's create-challenge example: the card needs authenticating first.
spy.Reply("""
{"itinerary_id":"8999989898988",
 "encoded_challenge_config":"ABEiM0RVZneImaq7zN3u/w==",
 "links":{"complete_payment_session":{"method":"PUT","href":"/v2/itineraries/8999989898988/activity/payment-sessions?token=SESSION"}}}
""", HttpStatusCode.Created);

var challenged = await client.CreateBooking("CREATETOKEN", createRequest, createOptions);

Check("challenge config parsed", challenged.EncodedChallengeConfig == "ABEiM0RVZneImaq7zN3u/w==");
Check("complete_payment_session link parsed", challenged.Links?.CompletePaymentSession?.GetToken() == "SESSION");
Check("a challenged create is NOT a booking", challenged.RequiresPaymentChallenge);

// Either half on its own is enough: whichever one Rapid sends, the booking is not confirmed.
spy.Reply("""{"itinerary_id":"1","encoded_challenge_config":"ABC"}""", HttpStatusCode.Created);
Check("challenge config alone is enough", (await client.CreateBooking("t", createRequest, createOptions)).RequiresPaymentChallenge);

spy.Reply("""{"itinerary_id":"1","links":{"complete_payment_session":{"method":"PUT","href":"/x"}}}""", HttpStatusCode.Created);
Check("completion link alone is enough", (await client.CreateBooking("t", createRequest, createOptions)).RequiresPaymentChallenge);

// --- 8. Cancel: a 202 is NOT a success ----------------------------------------------------
var cancelOptions = new CancelActivityBookingOptions { Customer = new CustomerHeaderOptions("1.2.3.4") };

spy.Reply("", HttpStatusCode.NoContent);
Check("204 means cancelled", await client.CancelBooking("123", cancelOptions) == CancelActivityBookingResult.Cancelled);

spy.Reply("", HttpStatusCode.Accepted);
Check("202 means unknown, not cancelled", await client.CancelBooking("123", cancelOptions) == CancelActivityBookingResult.Unknown);

Console.WriteLine();
Console.WriteLine(failures == 0 ? "TUTTI I CONTROLLI SUPERATI" : $"{failures} CONTROLLI FALLITI");
return failures;


IExpediaActivitiesApiClient BuildClient(Dictionary<string, string?> configuration)
{
    var services = new ServiceCollection();
    services.AddLogging();
    services.AddExpediaRapidApiService(new ConfigurationBuilder().AddInMemoryCollection(configuration).Build());

    // Swap the transport of the Activities client, and short-circuit its token so nothing calls the identity service.
    services.AddHttpClient(nameof(IExpediaActivitiesApiClient)).ConfigurePrimaryHttpMessageHandler(() => spy);
    services.AddSingleton<IExpediaActivitiesCurrentUserService>(new StubToken());

    return services.BuildServiceProvider().GetRequiredService<IExpediaActivitiesApiClient>();
}

void Check(string what, bool ok)
{
    var color = Console.ForegroundColor;
    Console.ForegroundColor = ok ? ConsoleColor.Green : ConsoleColor.Red;
    Console.WriteLine($"  [{(ok ? "OK" : "KO")}] {what}");
    Console.ForegroundColor = color;

    if (!ok)
    {
        failures++;
        Console.WriteLine($"        richiesta: {spy.Method} {spy.Path}{spy.Query}");
        Console.WriteLine($"        header:    {spy.Headers}");
    }
}

internal sealed class SpyHandler : HttpMessageHandler
{
    private string _body = "{}";
    private HttpStatusCode _status = HttpStatusCode.OK;
    private string? _link;
    private int? _totalResults;

    public string Method { get; private set; } = "";
    public string Path { get; private set; } = "";
    public string Query { get; private set; } = "";
    public string Headers { get; private set; } = "";

    public void Reply(string body, HttpStatusCode status = HttpStatusCode.OK, string? link = null, int? totalResults = null)
    {
        _body = body;
        _status = status;
        _link = link;
        _totalResults = totalResults;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Method = request.Method.Method;
        Path = request.RequestUri!.AbsolutePath;
        Query = Uri.UnescapeDataString(request.RequestUri.Query);
        Headers = string.Join(" | ", request.Headers.Select(h => $"{h.Key}: {string.Join(',', h.Value)}"));

        var response = new HttpResponseMessage(_status)
        {
            Content = new StringContent(_body, Encoding.UTF8, "application/json"),
        };

        if (_link is not null)
        {
            response.Headers.TryAddWithoutValidation("Link", _link);
        }

        if (_totalResults is not null)
        {
            response.Headers.TryAddWithoutValidation("Pagination-Total-Results", _totalResults.ToString());
        }

        return Task.FromResult(response);
    }
}

internal sealed class StubToken : IExpediaActivitiesCurrentUserService
{
    public Task<string> GetAccessToken() => Task.FromResult("stub-token");
    public Task<bool> IsAuthenticated() => Task.FromResult(true);
    public Task<string> ReloadAccessToken() => Task.FromResult("stub-token");
}
