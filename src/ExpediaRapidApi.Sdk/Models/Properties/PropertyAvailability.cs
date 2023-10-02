using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models.Properties
{
    public class PropertyAvailability
    {
        [JsonPropertyName("property_id")]
        public long PropertyId { get; set; }
        [JsonPropertyName("rooms")]
        public List<Room> Rooms { get; set; }
        [JsonPropertyName("links")]
        public Links Links { get; set; }
        [JsonPropertyName("score")]
        public long Score { get; set; }
    }
}
