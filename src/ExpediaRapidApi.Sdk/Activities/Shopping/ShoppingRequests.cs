using ExpediaRapidApi.Sdk.Lodging;
using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Activities.Shopping;

public class GetAvailabilityRequest
{
    /// <summary>
    /// The activities to price. Between 1 and <see cref="MaxIdsPerRequest"/>.
    /// </summary>
    public List<string> ActivityId { get; set; } = [];

    public string Language { get; set; } = default!;

    /// <summary>
    /// Requested currency for the rates, in ISO 4217 format.
    /// </summary>
    public string Currency { get; set; } = default!;

    public DateOnly StartDate { get; set; }

    /// <summary>
    /// At most <see cref="MaxDays"/> days after <see cref="StartDate"/>.
    /// </summary>
    public DateOnly EndDate { get; set; }

    /// <summary>
    /// Two-letter country code of the traveller's point of sale, in ISO 3166-1 alpha-2 format.
    /// </summary>
    public string CountryCode { get; set; } = default!;

    /// <summary>
    /// The booking question data types we are able to ask the traveller.
    /// Required: Rapid rejects the call without at least one value.
    /// See ActivityBookingDataTypes.
    /// </summary>
    public List<string> SupportedBookingDataTypes { get; set; } = [];

    public const int MaxIdsPerRequest = 20;
    public const int MaxDays = 14;
}

public class GetCalendarAvailabilityRequest
{
    /// <summary>
    /// The activities to price. Between 1 and <see cref="MaxIdsPerRequest"/>. This cap is the reason a region-wide
    /// sweep costs one call per 20 activities.
    /// </summary>
    public List<string> ActivityId { get; set; } = [];

    public string Currency { get; set; } = default!;

    public DateOnly StartDate { get; set; }

    /// <summary>
    /// At most <see cref="MaxDays"/> days after <see cref="StartDate"/>.
    /// </summary>
    public DateOnly EndDate { get; set; }

    public const int MaxIdsPerRequest = 20;
    public const int MaxDays = 30;
}

public class PriceCheckRequest
{
    /// <summary>
    /// Path parameter: not part of the query string.
    /// </summary>
    [JsonIgnore]
    public string ActivityId { get; set; } = default!;

    /// <summary>
    /// The token from the price_check link of the availability response.
    /// </summary>
    public string Token { get; set; } = default!;

    /// <summary>
    /// The tickets being bought, one entry per ticket type, each formatted as "ticketId-count".
    /// Build them with <see cref="ActivityTickets.Format"/> rather than by hand.
    /// </summary>
    public List<string> Tickets { get; set; } = [];
}

/// <summary>
/// The "ticketId-count" encoding that the price check expects on the wire.
/// </summary>
public static class ActivityTickets
{
    public static string Format(string ticketId, int count) => $"{ticketId}-{count}";

    public static List<string> Format(IEnumerable<KeyValuePair<string, int>> tickets)
        => tickets.Where(x => x.Value > 0).Select(x => Format(x.Key, x.Value)).ToList();
}

/// <summary>
/// The booking question data types, as they appear in supported_booking_data_types and in Type.
/// </summary>
// ⚠️ supported_booking_data_types is a promise, not a filter for convenience: it tells Rapid which questions we can put to a traveller, and Rapid answers with only the activities we could actually book.
// Declaring a type the booking form cannot render means showing an activity that will fail at the last step, after the card has been charged.
// Keep this list and what the form renders in step.
// The authoritative catalogue is the data types endpoint — see GetBookingQuestionDataTypes.
// These constants are the types documented at the time of writing.
public static class ActivityBookingDataTypes
{
    public const string Text = "text";
    public const string Date = "date";
    public const string DateTime = "datetime";
    public const string Time = "time";
    public const string Email = "email";
    public const string Phone = "phone";
    public const string Boolean = "boolean";
    public const string Integer = "integer";
    public const string Selection = "selection";
    public const string Measurement = "measurement";
    public const string Address = "address";
    public const string TravelDocument = "travel_document";

    /// <summary>
    /// Every type, handled.
    /// Only send it if the booking form really can cope with a question type it has never seen.
    /// </summary>
    public const string Wildcard = "*";

    /// <summary>
    /// Only activities that ask nothing at all.
    /// </summary>
    public const string None = "none";

    /// <summary>
    /// The documented types, wildcard and "none" excluded.
    /// </summary>
    public static readonly IReadOnlyList<string> All =
    [
        Text, Date, DateTime, Time, Email, Phone, Boolean, Integer, Selection, Measurement, Address, TravelDocument
    ];
}

/// <summary>
/// Request for the data type catalogue.
/// </summary>
public class GetBookingQuestionDataTypesRequest
{
    /// <summary>
    /// Only return the types whose date_added is on or after this UTC date.
    /// Left null, the whole catalogue comes back.
    /// The validation rules come back whole either way.
    /// </summary>
    public DateOnly? DateUpdatedStart { get; set; }
}

public class PriceCheckOptions : IHasCustomerHeaderOptions, IHasTestHeaderOptions
{
    public CustomerHeaderOptions? Customer { get; set; }

    public string? Test { get; set; }
}
