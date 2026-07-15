using ExpediaRapidApi.Sdk.Lodging;

namespace ExpediaRapidApi.Sdk.Activities.Bookings.CancelActivityBooking;

public class CancelActivityBookingOptions : IHasCustomerHeaderOptions, IHasTestHeaderOptions
{
    /// <summary>
    /// Required on this call: Rapid rejects a cancel without the customer IP.
    /// </summary>
    public required CustomerHeaderOptions Customer { get; set; }

    public string? Test { get; set; }
}

/// <summary>
/// The outcome of a cancellation. Rapid answers a cancel with either a 204 or a 202, and the difference matters:
/// a 202 is not a success, it means Expedia could not determine the state of the itinerary. Treating it as a
/// success would refund a customer whose booking may still be live.
/// </summary>
public enum CancelActivityBookingResult
{
    /// <summary>
    /// 204. The booking is cancelled.
    /// </summary>
    Cancelled,

    /// <summary>
    /// 202. The request was valid but the state of the itinerary is unknown: neither cancelled nor rejected.
    /// Retrieve the itinerary to find out, and do not refund until you know.
    /// </summary>
    Unknown
}
