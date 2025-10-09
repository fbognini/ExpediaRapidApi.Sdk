using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Cars.Shared;

public class CarLocations
{
    [JsonPropertyName("pickup")]
    public CarLocation Pickup { get; set; } = default!;

    [JsonPropertyName("dropoff")]
    public CarLocation Dropoff { get; set; } = default!;
}

public class CarLocation
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = default!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("address")]
    public Address Address { get; set; } = default!;

    [JsonPropertyName("coordinates")]
    public Coordinates Coordinates { get; set; } = default!;

    /// <summary>
    /// The type of location being offered.
    /// </summary>
    [JsonPropertyName("type")]
    public CarLocationType Type { get; set; }

    [JsonPropertyName("airport_code")]
    public string? AirportCode { get; set; }

    [JsonPropertyName("vendor")]
    public string? Vendor { get; set; }

    [JsonPropertyName("hours_of_operation")]
    public List<HoursOfOperation>? HoursOfOperation { get; set; }

    [JsonPropertyName("rating")]
    public Rating? Rating { get; set; }
}

public enum CarLocationType
{
    airport,
    non_airport,
}

public class Address
{
    [JsonPropertyName("line_1")]
    public string Line1 { get; set; }

    [JsonPropertyName("line_2")]
    public string Line2 { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("state_province_name")]
    public string StateProvinceName { get; set; }

    [JsonPropertyName("postal_code")]
    public string PostalCode { get; set; }

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; }
}

public class HoursOfOperation
{
    [JsonPropertyName("start_time")]
    public DateTime StartTime { get; set; }

    [JsonPropertyName("end_time")]
    public DateTime EndTime { get; set; }
}


public class Rating
{
    public double Count { get; set; }

    public double Overall { get; set; }
}
