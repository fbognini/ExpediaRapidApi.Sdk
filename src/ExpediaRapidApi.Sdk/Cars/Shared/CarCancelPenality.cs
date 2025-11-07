namespace ExpediaRapidApi.Sdk.Cars.Shared;

public class CarCancelPenality
{
    /// <summary>
    /// The time after which the cancellation penalty applies, provided in ISO 8601 format. If start_time is not provided, the cancellation penalty applies from the time of booking until the next start_time or the rental pickup_time, whichever occurs first.
    /// </summary>
    public DateTime? Start { get; set; }
    /// <summary>
    /// The time before which the cancellation penalty applies, provided in ISO 8601 format. If end_time is not provided, the cancellation penalty applies until the start of the rental period.
    /// </summary>
    public DateTime? End { get; set; }
    public double? Amount { get; set; }
    public string? Currency { get; set; }
    /// <summary>
    /// Percentage of total booking charged for as penalty. A thirty percent penalty would be returned as 30%
    /// </summary>
    public string? Percent { get; set; }
}