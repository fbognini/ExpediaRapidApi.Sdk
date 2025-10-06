using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpediaRapidApi.Sdk.Models.Shared;

public class CancelPenalty
{
    [JsonPropertyName("start")]
    public DateTimeOffset Start { get; set; }
    [JsonPropertyName("end")]
    public DateTimeOffset End { get; set; }
    [JsonPropertyName("nights")]
    public string? Nights { get; set; }
    [JsonPropertyName("amount")]
    public double? Amount { get; set; }
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }
    /// <summary>
    /// Percentage of total booking charged for as penalty. A thirty percent penalty would be returned as 30%
    /// </summary>
    [JsonPropertyName("percent")]
    public string? Percent { get; set; }
}