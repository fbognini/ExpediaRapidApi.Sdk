using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models.Regions
{
    public class Associations
    {
        [JsonPropertyName("point_of_interest")]
        public required List<string> PointOfInterests { get; set; }
    }
}
