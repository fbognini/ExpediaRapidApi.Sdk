using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Shared;

public class BillingContact
{
    public string GivenName { get; set; } = default!;

    public string FamilyName { get; set; } = default!;

    public BillingContactAddress Address { get; set; } = default!;
}

public class BillingContactAddress
{
    // The snake_case naming policy turns "Line1" into "line1", but Rapid expects "line_1". Without these
    // attributes the address lines are silently dropped by Expedia as unknown properties.
    [JsonPropertyName("line_1")]
    public string Line1 { get; set; } = default!;

    [JsonPropertyName("line_2")]
    public string? Line2 { get; set; }

    [JsonPropertyName("line_3")]
    public string? Line3 { get; set; }

    public string City { get; set; } = default!;

    public string? StateProvinceCode { get; set; }

    public string PostalCode { get; set; } = default!;

    public string CountryCode { get; set; } = default!;
}

