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

public class PriceCheckOptions : IHasCustomerHeaderOptions, IHasTestHeaderOptions
{
    public CustomerHeaderOptions? Customer { get; set; }

    public string? Test { get; set; }
}
