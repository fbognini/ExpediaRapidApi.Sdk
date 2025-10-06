using ExpediaRapidApi.Sdk.Models.Properties;
using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Lodging.Bookings.CreateLodgingBooking;

public partial class CreateLodgingBookingResponse
{
    [JsonPropertyName("itinerary_id")]
    public string ItineraryId { get; set; } = string.Empty;

    [JsonPropertyName("links")]
    public CreateLodgingBookingLinks? Links { get; set; }

    [JsonPropertyName("encoded_challenge_config")]
    public string? EncodedChallengeConfig { get; set; }
}

public partial class CreateLodgingBookingLinks
{
    [JsonPropertyName("retrieve")]
    public Link? Retrieve { get; set; }

    [JsonPropertyName("resume")]
    public Link? Resume { get; set; }

    [JsonPropertyName("cancel")]
    public Link? Cancel { get; set; }

    [JsonPropertyName("complete_payment_session")]
    public Link? CompletePaymentSession { get; set; }
}
