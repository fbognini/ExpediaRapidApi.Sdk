using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models.Regions
{
    public class Coordinates
    {
        [JsonPropertyName("center_longitude")]
        public double CenterLongitude { get; set; }

        [JsonPropertyName("center_latitude")]
        public double CenterLatitude { get; set; }
    }
}
