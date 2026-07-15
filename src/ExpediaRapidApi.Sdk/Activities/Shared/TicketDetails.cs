namespace ExpediaRapidApi.Sdk.Activities.Shared;

/// <summary>
/// A ticket type of an activity: Adult, Child, Infant, and so on.
/// </summary>
public class TicketDetails
{
    public string Id { get; set; } = default!;

    /// <summary>
    /// The name of the ticket type. E.g. "Adult".
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// The long form description of the ticket type, restrictions included. E.g. "Ages 11-80".
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Languages supported by this ticket type, in ISO 639 format.
    /// </summary>
    public List<string> AvailableLanguages { get; set; } = [];

    /// <summary>
    /// How many visitors a single ticket covers.
    /// </summary>
    public int? MaxVistorsPerTicket { get; set; }

    /// <summary>
    /// This ticket can only be booked together with at least one of these ticket types. Typically an infant ticket
    /// requires an adult one: the ticket picker must enforce this.
    /// </summary>
    public List<string> ValidWithTicketIds { get; set; } = [];
}

/// <summary>
/// What a given number of tickets of one type costs.
/// </summary>
public class TicketPricing
{
    /// <summary>
    /// Minimum number of tickets this price applies to.
    /// </summary>
    public int? MinTicketCount { get; set; }

    /// <summary>
    /// Maximum number of tickets this price applies to.
    /// </summary>
    public int? MaxTicketCount { get; set; }

    /// <summary>
    /// The pre-discount price, when there is a discount to show.
    /// </summary>
    public StrikethroughPrice? Strikethrough { get; set; }

    public ActivityTotals? Totals { get; set; }
}

public class StrikethroughPrice
{
    public ActivityAmount? Inclusive { get; set; }
}

/// <summary>
/// A price broken down into its components.
/// </summary>
public class ActivityTotals
{
    /// <summary>
    /// Base price, before taxes and fees.
    /// </summary>
    public ActivityAmount? Exclusive { get; set; }

    public BookingTaxesAndFees? BookingTaxesAndFees { get; set; }

    /// <summary>
    /// What is due at booking: <see cref="Exclusive"/> plus the pay-at-booking fees. This is what we charge.
    /// </summary>
    public ActivityAmount? Inclusive { get; set; }

    /// <summary>
    /// The full price the traveller ends up paying: <see cref="Inclusive"/> plus the fees collected on site.
    /// Show it, but do not charge it.
    /// </summary>
    public ActivityAmount? InclusiveLocation { get; set; }
}

/// <summary>
/// The tickets of an offer, priced.
/// </summary>
public class TicketPriceSummary
{
    /// <summary>
    /// Maps to the ticket details returned alongside the offer.
    /// </summary>
    public string Id { get; set; } = default!;

    public List<TicketPricing> PricePerTicket { get; set; } = [];
}

/// <summary>
/// The full pricing of an offer: per ticket type, and in total.
/// </summary>
public class OfferPriceSummary
{
    public List<PricePerTicket> PricePerTicket { get; set; } = [];

    public OfferTotals? Totals { get; set; }
}

public class PricePerTicket
{
    public TicketDetails? Ticket { get; set; }

    /// <summary>
    /// What the cost refers to, for display. E.g. "per ticket type".
    /// </summary>
    public string? Scope { get; set; }

    public StrikethroughPrice? Strikethrough { get; set; }

    public ActivityTotals? TicketTotal { get; set; }

    /// <summary>
    /// How many tickets of this type are being sold.
    /// </summary>
    public int TicketCount { get; set; }
}

public class OfferTotals : ActivityTotals
{
    /// <summary>
    /// How many tickets are being sold in total.
    /// </summary>
    public int TotalTicketCount { get; set; }
}
