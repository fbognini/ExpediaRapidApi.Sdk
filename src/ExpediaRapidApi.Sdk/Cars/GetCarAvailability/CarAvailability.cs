using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Cars.GetCarAvailability;

public class CarAvailability
{
    [JsonPropertyName("car_rental_id")]
    public string CarRentalId { get; set; } = default!;

    [JsonPropertyName("vendor")]
    public Vendor Vendor { get; set; } = default!;

    [JsonPropertyName("locations")]
    public CarLocations Locations { get; set; } = default!;

    [JsonPropertyName("description")]
    public string Description { get; set; } = default!;

    [JsonPropertyName("number_of_passengers")]
    public double NumberOfPassengers { get; set; }

    [JsonPropertyName("number_of_doors")]
    public NumberOfDoors NumberOfDoors { get; set; }

    [JsonPropertyName("luggage_count")]
    public LuggageCount LuggageCount { get; set; }

    [JsonPropertyName("acriss_code")]
    public string AcrissCode { get; set; } = default!;

    [JsonPropertyName("mileage_tracking")]
    public string MileageTracking { get; set; } = default!;

    [JsonPropertyName("image")]
    public CarAvailabilityImage Image { get; set; } = default!;

    [JsonPropertyName("rate")]
    public Rate Rate { get; set; } = default!;

    [JsonPropertyName("cancel_penalties")]
    public List<CancelPenalty> CancelPenalties { get; set; }

    [JsonPropertyName("links")]
    public CarAvailabilityLinks Links { get; set; } = new();

}


public class CarAvailabilityImage
{
    [JsonPropertyName("caption")]
    public string Caption { get; set; } = default!;

    [JsonPropertyName("links")]
    public CarAvailabilityImageLinks Links { get; set; } = default!;
}

public class CarAvailabilityImageLinks
{
    [JsonPropertyName("rental_image")]
    public Link RentalImage { get; set; } = default!;
}



public class CarAvailabilityLinks
{
    [JsonPropertyName("details")]
    public Link Details { get; set; } = new();
}