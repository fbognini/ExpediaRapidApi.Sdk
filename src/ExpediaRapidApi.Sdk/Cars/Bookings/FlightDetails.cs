using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Cars.Bookings;

public class FlightDetails
{
    /// <summary>
    /// The air carrier code. Must be a 2 character code.
    /// </summary>
    [JsonPropertyName("air_carrier_code")]
    public string AirCarrierCode { get; set; } = default!;

    /// <summary>
    /// The flight number. Must be a 1-4 digit number.
    /// </summary>
    [JsonPropertyName("flight_number")]
    public int FlightNumber { get; set; }
}
