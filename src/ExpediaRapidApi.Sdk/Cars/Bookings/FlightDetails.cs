using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Cars.Bookings;

public class FlightDetails
{
    [JsonPropertyName("air_carrier_code")]
    public string AirCarrierCode { get; set; } = default!;

    [JsonPropertyName("flight_number")]
    public int FlightNumber { get; set; }
}
