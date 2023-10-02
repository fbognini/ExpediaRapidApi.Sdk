using ExpediaRapidApi.Sdk.Models.Properties;
using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models.Bookings
{
    public partial class BookingResponse
    {
        [JsonPropertyName("itinerary_id")]
        public required string ItineraryId { get; set; }

        [JsonPropertyName("links")]
        public BookingResponseLinks? Links { get; set; }

        [JsonPropertyName("encoded_challenge_config")]
        public string? EncodedChallengeConfig { get; set; }
    }

    public partial class BookingResponseLinks
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
}
