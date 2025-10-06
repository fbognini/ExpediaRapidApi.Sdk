namespace ExpediaRapidApi.Sdk.Cars.Bookings.CreateCarBooking;

public class CreateCarBookingRequest
{
    /// <summary>
    /// Specifies the affiliate reference ID associated with the booking.This field supports a minimum of 3 characters, maximum of 28 characters, and is required to be unique per booking.Entering special characters("<", ">", "(", ")", and "&") in this field will result in the request being rejected.
    /// </summary>
    public string AffiliateReferenceId { get; set; } = default!;
    public PrimaryDriver PrimaryDriver { get; set; } = default!;
    public FlightDetails? FlightDetails { get; set; }
    public string? TravelerLoyaltyMemberId { get; set; }
    public List<string> RequestedExtras { get; set; } = [];
    public string? AffiliateMetadata { get; set; }
    public string PaymentToken { get; set; } = default!;
}
