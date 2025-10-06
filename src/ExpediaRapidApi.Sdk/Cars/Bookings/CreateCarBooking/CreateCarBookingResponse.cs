using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Cars.Bookings.CreateCarBooking;

public class CreateCarBookingResponse
{
    public string ItineraryId { get; set; } = default!;

    [JsonPropertyName("links")]
    public CreateCarBookingLinks Links { get; set; }
}

public class CreateCarBookingLinks
{
    [JsonPropertyName("retrieve")]
    public Link Cancel { get; set; }
}
