using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpediaRapidApi.Sdk.Lodging.Shared;

public class LodgingCancelPenality
{
    /// <summary>
    /// Effective date and time of cancellation penalty in extended ISO 8601 format, with ±hh:mm timezone offset
    /// </summary>
    public DateTimeOffset Start { get; set; }
    /// <summary>
    /// End date and time of cancellation penalty in extended ISO 8601 format, with ±hh:mm timezone offset
    /// </summary>
    public DateTimeOffset End { get; set; }
    public int? Nights { get; set; }
    public double? Amount { get; set; }
    public string? Currency { get; set; }
    /// <summary>
    /// Percentage of total booking charged for as penalty. A thirty percent penalty would be returned as 30%
    /// </summary>
    public string? Percent { get; set; }
}