using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models.Cars;

public class CarLocations
{
    [JsonPropertyName("pickup")]
    public CarLocation Pickup { get; set; }

    [JsonPropertyName("dropoff")]
    public CarLocation Dropoff { get; set; }
}


public class CarLocation
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("address")]
    public Address Address { get; set; }

    [JsonPropertyName("coordinates")]
    public Coordinates Coordinates { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("airport_code")]
    public string AirportCode { get; set; }

    [JsonPropertyName("vendor")]
    public string Vendor { get; set; }

    [JsonPropertyName("hours_of_operation")]
    public List<HoursOfOperation> HoursOfOperation { get; set; }

    [JsonPropertyName("rating")]
    public Rating Rating { get; set; }
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
    [JsonPropertyName("count")]
    public double Count { get; set; }

    [JsonPropertyName("overall")]
    public double Overall { get; set; }
}
