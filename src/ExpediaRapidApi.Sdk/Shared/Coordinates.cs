using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Shared;

public class Coordinates
{
    [JsonPropertyName("latitude")]
    public double Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public double Longitude { get; set; }
}
