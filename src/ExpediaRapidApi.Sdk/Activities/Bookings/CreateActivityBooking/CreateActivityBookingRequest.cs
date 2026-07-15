using ExpediaRapidApi.Sdk.Lodging;

namespace ExpediaRapidApi.Sdk.Activities.Bookings.CreateActivityBooking;

public class CreateActivityBookingRequest
{
    /// <summary>
    /// Our own reference for this booking. Up to 28 characters, unique, and limited to letters, digits, "-" and "_".
    /// </summary>
    public required string AffiliateReferenceId { get; set; }

    public required string Email { get; set; }

    /// <summary>
    /// The token returned by the Pay API, which stands in for the card.
    /// </summary>
    public required string PaymentToken { get; set; }

    public required PrimaryTraveler PrimaryTraveler { get; set; }

    public List<AdditionalTraveler>? AdditionalTravelers { get; set; }

    /// <summary>
    /// Up to 256 characters of our own metadata, echoed back on every retrieve.
    /// Must be formatted as "key1:value|key2:value".
    /// </summary>
    public string? AffiliateMetadata { get; set; }

    /// <summary>
    /// The customer's taxpayer identification number. Only needed for Brazilian and Indian customers.
    /// </summary>
    public string? TaxRegistrationNumber { get; set; }
}

public class CreateActivityBookingResponse
{
    public string ItineraryId { get; set; } = default!;

    public CreateActivityBookingLinks? Links { get; set; }
}

public class CreateActivityBookingLinks
{
    public Link? Retrieve { get; set; }
}

public class CreateActivityBookingOptions : IHasCustomerHeaderOptions, IHasTestHeaderOptions
{
    /// <summary>
    /// Required on this call: Rapid rejects a create without the customer IP.
    /// </summary>
    public required CustomerHeaderOptions Customer { get; set; }

    public string? Test { get; set; }
}
