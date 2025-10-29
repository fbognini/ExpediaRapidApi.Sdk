using ExpediaRapidApi.Sdk.Models.Properties;
using ExpediaRapidApi.Sdk.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpediaRapidApi.Sdk.Shared;

public class Link
{
    [JsonPropertyName("method")]
    public string Method { get; set; } = string.Empty;

    [JsonPropertyName("href")]
    public string Href { get; set; } = string.Empty;

    [JsonPropertyName("expires")]
    public string? Expires { get; set; }

    public string? GetToken() => ExpediaHelpers.FindQueryParameterInLink(Href, "token");
}
