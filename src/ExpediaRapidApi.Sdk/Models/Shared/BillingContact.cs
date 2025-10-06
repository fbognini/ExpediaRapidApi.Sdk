using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpediaRapidApi.Sdk.Models.Shared;

public class BillingContact
{
    [JsonPropertyName("given_name")]
    public string GivenName { get; set; }

    [JsonPropertyName("family_name")]
    public string FamilyName { get; set; }

    [JsonPropertyName("address")]
    public BillingContactAddress Address { get; set; }
}

public class BillingContactAddress
{
    [JsonPropertyName("line_1")]
    public string? Line1 { get; set; }

    [JsonPropertyName("line_2")]
    public string? Line2 { get; set; }

    [JsonPropertyName("line_3")]
    public string? Line3 { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("state_province_code")]
    public string? StateProvinceCode { get; set; }

    [JsonPropertyName("postal_code")]
    public string? PostalCode { get; set; }

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; } = string.Empty;
}

