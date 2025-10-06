using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpediaRapidApi.Sdk.Models.Shared;

public class Phone
{
    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; } = string.Empty;

    [JsonPropertyName("area_code")]
    public string? AreaCode { get; set; }

    [JsonPropertyName("number")]
    public string Number { get; set; } = string.Empty;
}
