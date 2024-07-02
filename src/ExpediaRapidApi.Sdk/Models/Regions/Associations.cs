using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models.Regions
{
    public class Associations
    {
        [JsonPropertyName("point_of_interest")]
        public List<string> PointOfInterests { get; set; } = new();
    }
}
