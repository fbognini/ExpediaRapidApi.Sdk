using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Cars.Shared;

public class Vendor
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("image")]
    public VendorImage Image { get; set; } = default!;
}

public class VendorImage
{
    [JsonPropertyName("caption")]
    public string Caption { get; set; } = default!;

    [JsonPropertyName("links")]
    public VendorImageLinks Links { get; set; } = default!;
}

public class VendorImageLinks
{
    [JsonPropertyName("vendor_image")]
    public Link VendorImage { get; set; } = default!;
}
