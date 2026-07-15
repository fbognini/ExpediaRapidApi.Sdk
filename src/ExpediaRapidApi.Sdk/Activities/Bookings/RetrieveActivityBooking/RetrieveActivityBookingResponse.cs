using ExpediaRapidApi.Sdk.Activities.Content;
using ExpediaRapidApi.Sdk.Activities.Shopping;
using ExpediaRapidApi.Sdk.Lodging;

namespace ExpediaRapidApi.Sdk.Activities.Bookings.RetrieveActivityBooking;

public class RetrieveActivityBookingResponse
{
    public string ItineraryId { get; set; } = default!;

    /// <summary>
    /// The state of the itinerary. E.g. "BOOKED".
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// The supplier's own reference for the booking.
    /// </summary>
    public string? ConfirmationId { get; set; }

    public BookedActivity? Activity { get; set; }

    public BookedTraveler? PrimaryTraveler { get; set; }

    public List<BookedTraveler> AdditionalTravelers { get; set; } = [];

    public string? Email { get; set; }

    public Phone? Phone { get; set; }

    public BillingContact? BillingContact { get; set; }

    public DateTime? CreationTime { get; set; }

    public string? AffiliateMetadata { get; set; }

    public string? AffiliateReferenceId { get; set; }

    public RetrieveActivityBookingLinks? Links { get; set; }
}

public class RetrieveActivityBookingLinks
{
    public Link? Cancel { get; set; }

    public Link? RetrieveVoucher { get; set; }
}

/// <summary>
/// The activity as it was booked, with the cancellation terms that actually apply to this itinerary.
/// </summary>
public class BookedActivity
{
    public string ActivityId { get; set; } = default!;

    public string? Title { get; set; }

    public string? Description { get; set; }

    public List<ActivityDisclaimer> Disclaimers { get; set; } = [];

    public List<string> Amenities { get; set; } = [];

    public ActivityMediaCollection? Media { get; set; }

    /// <summary>
    /// How the traveller redeems the booking. E.g. "REDEMPTION_TYPE_PRINT".
    /// </summary>
    public string? RedemptionType { get; set; }

    public string? SupplierName { get; set; }

    public List<ActivityLocation> RedemptionLocations { get; set; } = [];

    public ActivityLocation? EventLocation { get; set; }

    public OfferPriceSummary? Tickets { get; set; }

    public TraderInformation? TraderInformation { get; set; }

    /// <summary>
    /// When the activity starts, local, without timezone offset. E.g. "2025-04-01T14:45:04".
    /// </summary>
    public string? OfferStart { get; set; }

    /// <summary>
    /// Whether the itinerary can still be cancelled right now.
    /// </summary>
    public bool Cancellable { get; set; }

    /// <summary>
    /// The cancellation penalties that apply to this itinerary. Unlike the content's cancellation policy, these are
    /// concrete: they carry the windows and the amounts. Only available once the booking exists.
    /// </summary>
    public List<ActivityCancelPenalty> CancelPenalties { get; set; } = [];

    public List<HoursOfOperation> HoursOfOperation { get; set; } = [];

    /// <summary>
    /// How long the activity lasts, as an ISO-8601 duration.
    /// </summary>
    public string? Duration { get; set; }

    public List<string> Language { get; set; } = [];

    public bool AllDay { get; set; }

    public List<string> Callouts { get; set; } = [];

    public List<string> Inclusions { get; set; } = [];

    public List<string> Exclusions { get; set; } = [];

    public string? RedemptionInstructions { get; set; }

    public string? TermsAndConditions { get; set; }
}

public class ActivityDisclaimer
{
    public string? Type { get; set; }

    public string? Description { get; set; }
}

/// <summary>
/// What cancelling costs within a given window.
/// </summary>
public class ActivityCancelPenalty
{
    /// <summary>
    /// Currency of <see cref="Amount"/>, in ISO 4217 format.
    /// </summary>
    public string? Currency { get; set; }

    /// <summary>
    /// When the penalty starts applying, local, without timezone offset.
    /// </summary>
    public string? Start { get; set; }

    /// <summary>
    /// When the penalty stops applying, local. Absent on the last window, which runs to the start of the activity.
    /// </summary>
    public string? End { get; set; }

    public string? Amount { get; set; }

    /// <summary>
    /// The penalty as a percentage of the booking, formatted with the sign. E.g. "30%".
    /// </summary>
    public string? Percent { get; set; }

    /// <summary>
    /// Parses <see cref="Percent"/>, stripping the trailing "%".
    /// </summary>
    public decimal? PercentAsDecimal() => string.IsNullOrWhiteSpace(Percent)
        ? null
        : decimal.Parse(Percent.TrimEnd('%'), System.Globalization.CultureInfo.InvariantCulture);
}

public class HoursOfOperation
{
    public string? DayOfWeek { get; set; }

    /// <summary>
    /// Local opening time, as HH:MM:SS.
    /// </summary>
    public string? StartTime { get; set; }

    /// <summary>
    /// Local closing time, as HH:MM:SS.
    /// </summary>
    public string? EndTime { get; set; }
}

public class BookedTraveler
{
    public TravelerName? Name { get; set; }

    /// <summary>
    /// The ticket type this traveller was booked under.
    /// </summary>
    public string? TravelerType { get; set; }
}

public class RetrieveActivityBookingOptions : IHasCustomerHeaderOptions, IHasTestHeaderOptions
{
    /// <summary>
    /// Required on this call: Rapid rejects a retrieve without the customer IP.
    /// </summary>
    public required CustomerHeaderOptions Customer { get; set; }

    public string? Test { get; set; }
}
