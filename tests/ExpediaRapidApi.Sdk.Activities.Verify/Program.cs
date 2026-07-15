using ExpediaRapidApi.Sdk;
using ExpediaRapidApi.Sdk.Activities;
using ExpediaRapidApi.Sdk.Activities.Bookings.CancelActivityBooking;
using ExpediaRapidApi.Sdk.Activities.Content;
using ExpediaRapidApi.Sdk.Activities.Geography;
using ExpediaRapidApi.Sdk.Activities.Shopping;
using ExpediaRapidApi.Sdk.Lodging;
using fbognini.Sdk.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;

// Drives the new Activities client against a stub transport: no credentials, no network.
// Checks the things that are easy to get wrong and that a compiler cannot catch:
// query strings, path vs query parameters, Link pagination, deserialization, the 202 on cancel.

var spy = new SpyHandler();

var config = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
{
    ["ExpediaRapidApiSettings:EnvironmentName"] = "STG",
    ["ExpediaRapidApiSettings:ApiKey:ApiKey"] = "k",
    ["ExpediaRapidApiSettings:ApiKey:ApiSecret"] = "s",
    ["ExpediaRapidApiSettings:OAuth:ClientId"] = "cars-client",
    ["ExpediaRapidApiSettings:OAuth:ClientSecret"] = "cars-secret",
    ["ExpediaRapidApiSettings:ActivitiesOAuth:ClientId"] = "activities-client",
    ["ExpediaRapidApiSettings:ActivitiesOAuth:ClientSecret"] = "activities-secret",
}).Build();

var services = new ServiceCollection();
services.AddLogging();
services.AddExpediaRapidApiService(config);

// Swap the transport of the Activities client, and short-circuit its token so nothing calls the identity service.
services.AddHttpClient(nameof(IExpediaActivitiesApiClient)).ConfigurePrimaryHttpMessageHandler(() => spy);
services.AddSingleton<IExpediaActivitiesCurrentUserService>(new StubToken());

var client = services.BuildServiceProvider().GetRequiredService<IExpediaActivitiesApiClient>();

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
Check("categories are repeated, not indexed", spy.Query == "?category_id=12&category_id=34");
Check("ids deserialized", region.Response.Count == 3 && region.Response[0] == "1001");
Check("next link read from rel=\"next\"", region.NextPageLink == "https://test.ean.com/v2/regions/6054439/activities?token=NEXT");
Check("total results read from header", region.TotalResults == 812);

// --- 2. Last page: no Link header means no next page --------------------------------------
spy.Reply("""["1004"]""");
var lastPage = await client.GetRegionActivities(new GetRegionActivitiesRequest { RegionId = "1" });
Check("last page has no next link", lastPage.NextPageLink is null);

// --- 3. Content: batching and the delta date ----------------------------------------------
spy.Reply("""
[{"id":"822672","title":"Tuk Tuk by night","duration":"PT4H","cancellation_policy":{"type":"before_start_date","hours":48},
  "category_ids":["7"],"ratings":{"guest":{"count":128,"overall":"4.8"}},
  "media":{"images":[{"caption":"poster","links":{"350px":{"method":"GET","href":"https://img/350.jpg"}}}]}}]
""");

var content = await client.GetActivitiesContent(new GetActivitiesContentRequest
{
    ActivityId = ["822672", "822673"],
    Language = "it-IT",
    DateUpdatedStart = new DateOnly(2026, 7, 10),
});

Check("activity ids repeated", spy.Query.Contains("activity_id=822672&activity_id=822673"));
Check("language sent", spy.Query.Contains("language=it-IT"));
Check("date_updated_start is snake_case and ISO", spy.Query.Contains("date_updated_start=2026-07-10"));
Check("content deserialized", content[0].Title == "Tuk Tuk by night");
Check("cancellation policy enum parsed", content[0].CancellationPolicy?.Type == ActivityCancellationPolicyType.before_start_date);
Check("nested rating parsed", content[0].Ratings?.Guest?.Overall == "4.8");
Check("media links keyed by size", content[0].Media?.Images[0].Links["350px"].Href == "https://img/350.jpg");

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

// --- 6. Price check: tickets encoding, dynamic fields, tokens ------------------------------
spy.Reply("""
{"status":"available",
 "offer_pricing":{"totals":{"inclusive":{"value":"34.17","currency":"EUR"},"total_ticket_count":3}},
 "required_booking_fields":{"passport":{"permitted_options":[],"applies_to_all_travelers":true},
                            "pickup":{"permitted_options":["flight"],"applies_to_all_travelers":false}},
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
Check("required fields drive the dynamic form", priceCheck.RequiredBookingFields["pickup"].PermittedOptions[0] == "flight");
Check("payment token extracted from link", priceCheck.Links?.Payment?.GetToken() == "PAYTOKEN");
Check("create token extracted from link", priceCheck.Links?.Create?.GetToken() == "CREATETOKEN");

// --- 7. sold_out and price_changed must be readable, not exceptions ------------------------
spy.Reply("""{"status":"sold_out"}""");
var soldOut = await client.PriceCheck(new PriceCheckRequest { ActivityId = "1", Token = "t" }, new PriceCheckOptions());
Check("sold_out parsed", soldOut.Status == ActivityPriceCheckStatus.sold_out);

// --- 8. Cancel: a 202 is NOT a success ----------------------------------------------------
var cancelOptions = new CancelActivityBookingOptions { Customer = new CustomerHeaderOptions("1.2.3.4") };

spy.Reply("", HttpStatusCode.NoContent);
Check("204 means cancelled", await client.CancelBooking("123", cancelOptions) == CancelActivityBookingResult.Cancelled);

spy.Reply("", HttpStatusCode.Accepted);
Check("202 means unknown, not cancelled", await client.CancelBooking("123", cancelOptions) == CancelActivityBookingResult.Unknown);

Console.WriteLine();
Console.WriteLine(failures == 0 ? "TUTTI I CONTROLLI SUPERATI" : $"{failures} CONTROLLI FALLITI");
return failures;


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
