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
    /// The answers to the questions the price check declared PerBooking: asked once for the whole itinerary rather than traveller by traveller.
    /// The per-traveller ones go on BookingQuestionAnswers instead.
    /// </summary>
    public List<BookingQuestionAnswer>? BookingQuestionAnswers { get; set; }

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

    /// <summary>
    /// The PSD2 challenge configuration, base64 encoded, when the payment provider wants the cardholder to authenticate before the booking is confirmed.
    /// Present only in that case.
    /// </summary>
    public string? EncodedChallengeConfig { get; set; }

    public CreateActivityBookingLinks? Links { get; set; }

    /// <summary>
    /// Whether Rapid answered with a payment challenge instead of a confirmed booking.
    /// </summary>
    // ⚠️ A response carrying a challenge is not a booking: the itinerary stays unconfirmed until the cardholder completes the challenge and complete_payment_session is called.
    // Treating it as a success means taking the customer's money for an activity nobody reserved.
    // Check this before recording anything.
    public bool RequiresPaymentChallenge
        => !string.IsNullOrEmpty(EncodedChallengeConfig) || Links?.CompletePaymentSession is not null;
}

public class CreateActivityBookingLinks
{
    public Link? Retrieve { get; set; }

    /// <summary>
    /// Where to confirm the booking once the cardholder has completed the PSD2 challenge.
    /// Present only alongside EncodedChallengeConfig.
    /// </summary>
    public Link? CompletePaymentSession { get; set; }
}

public class CreateActivityBookingOptions : IHasCustomerHeaderOptions, IHasTestHeaderOptions
{
    /// <summary>
    /// Required on this call: Rapid rejects a create without the customer IP.
    /// </summary>
    public required CustomerHeaderOptions Customer { get; set; }

    public string? Test { get; set; }
}
