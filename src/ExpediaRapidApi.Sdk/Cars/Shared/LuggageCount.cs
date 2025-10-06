using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Cars.Shared;

public class LuggageCount
{
    [JsonPropertyName("small")]
    public double Small { get; set; }

    [JsonPropertyName("large")]
    public double Large { get; set; }
}