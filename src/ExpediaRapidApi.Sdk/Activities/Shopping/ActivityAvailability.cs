using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Activities.Shopping;

/// <summary>
/// Availability of a set of activities: which days and time slots can be booked, and at what price.
/// </summary>
public class ActivityAvailability
{
    public List<ActivityOfferDetails> Activities { get; set; } = [];

    /// <summary>
    /// The ticket types referenced by the offers, keyed by ticket id. Shared across every activity in the response,
    /// so the offers only carry the id.
    /// </summary>
    public Dictionary<string, TicketDetails> TicketDetails { get; set; } = [];
}

public class ActivityOfferDetails
{
    public string Id { get; set; } = default!;

    public List<ActivityOfferDate> Dates { get; set; } = [];
}

public class ActivityOfferDate
{
    public DateOnly Date { get; set; }

    public List<ActivityOfferOption> Options { get; set; } = [];

    public BookingTaxesAndFees? BookingTaxesAndFees { get; set; }
}

/// <summary>
/// A bookable time slot.
/// </summary>
public class ActivityOfferOption
{
    /// <summary>
    /// Local start time, without timezone offset. E.g. "11:59".
    /// </summary>
    public string? StartTime { get; set; }

    /// <summary>
    /// The most tickets that can be bought in one order. The ticket picker must enforce it.
    /// </summary>
    public int? MaxTicketsPerOrder { get; set; }

    /// <summary>
    /// The fewest tickets that must be bought in one order. The ticket picker must enforce it.
    /// </summary>
    public int? MinTicketsPerOrder { get; set; }

    public List<TicketPriceSummary> Tickets { get; set; } = [];

    public ActivityOfferOptionLinks? Links { get; set; }
}

public class ActivityOfferOptionLinks
{
    /// <summary>
    /// Carries the token that the price check needs.
    /// </summary>
    public Link? PriceCheck { get; set; }
}

/// <summary>
/// Day-by-day availability of one activity, with its starting price. This is the cheap call: it is what prices a
/// list of search results.
/// </summary>
public class CalendarAvailability
{
    public string ActivityId { get; set; } = default!;

    public List<CalendarDay> Days { get; set; } = [];
}

public class CalendarDay
{
    public DateOnly Date { get; set; }

    public bool Available { get; set; }

    /// <summary>
    /// The lowest price on this date, for the most common ticket type (typically Adult). Null when not available.
    /// </summary>
    public string? FromPrice { get; set; }

    /// <summary>
    /// Currency of <see cref="FromPrice"/>, in ISO 4217 format.
    /// </summary>
    public string? Currency { get; set; }

    public decimal? FromPriceAsDecimal() => string.IsNullOrWhiteSpace(FromPrice)
        ? null
        : decimal.Parse(FromPrice, System.Globalization.CultureInfo.InvariantCulture);
}

/// <summary>
/// The outcome of a price check: whether the selection can still be booked, at what final price, what the booking
/// will require, and the tokens to pay and create it.
/// </summary>
public class ActivityPriceCheck
{
    public ActivityPriceCheckStatus Status { get; set; }

    public OfferPriceSummary? OfferPricing { get; set; }

    public TraderInformation? TraderInformation { get; set; }

    public string? RedemptionInstructions { get; set; }

    public string? TermsAndConditions { get; set; }

    /// <summary>
    /// Which traveller fields this specific activity requires, keyed by field name (passport, date_of_birth, height,
    /// weight, language, address, nationality, dietary_preference, pickup, dropoff, ...). It varies per activity, so
    /// the booking form has to be built from it at runtime rather than hardcoded.
    /// </summary>
    public Dictionary<string, RequiredBookingField> RequiredBookingFields { get; set; } = [];

    public ActivityPriceCheckLinks? Links { get; set; }
}

public enum ActivityPriceCheckStatus
{
    available,

    /// <summary>
    /// Still bookable, but not at the price we quoted: re-present it to the customer before booking.
    /// </summary>
    price_changed,

    sold_out
}

public class RequiredBookingField
{
    /// <summary>
    /// What is accepted for this field. For pickup and dropoff, which transit types are supported
    /// (flight, rail, bus, ship, hotel).
    /// </summary>
    public List<string> PermittedOptions { get; set; } = [];

    /// <summary>
    /// True when every traveller must provide it, false when only the primary one must.
    /// </summary>
    public bool AppliesToAllTravelers { get; set; }
}

public class ActivityPriceCheckLinks
{
    /// <summary>
    /// Carries the token for the Pay API, which turns card details into a payment token.
    /// </summary>
    public Link? Payment { get; set; }

    /// <summary>
    /// Carries the token for the create booking call.
    /// </summary>
    public Link? Create { get; set; }
}

public class TraderInformation
{
    public List<Trader> Traders { get; set; } = [];
}

public class Trader
{
    public string? Name { get; set; }

    public ActivityAddress? Address { get; set; }

    public string? BusinessRegisterName { get; set; }

    public string? BusinessRegisterNumber { get; set; }

    public bool? SelfCertification { get; set; }

    public string? RightToWithdrawMessage { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public bool? PrivateHost { get; set; }
}
