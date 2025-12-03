using ExpediaRapidApi.Sdk.Lodging;

namespace ExpediaRapidApi.Sdk.Cars.Bookings.RetrieveCarBooking;

public class RetrieveCarBookingOptions : IHasCustomerHeaderOptions
{
    public required CustomerHeaderOptions Customer { get; set; }
}
