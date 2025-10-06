using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Cars.Shared;

public class Vendor
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("image")]
    public VendorImage Image { get; set; }
}

public class VendorImage
{
    [JsonPropertyName("caption")]
    public string Caption { get; set; }

    [JsonPropertyName("links")]
    public Dictionary<string, Link> Links { get; set; }
}