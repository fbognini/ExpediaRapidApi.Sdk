using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Cars.Shared;

public class CarDetails
{
    [JsonPropertyName("locations")]
    public CarLocations Locations { get; set; }

    [JsonPropertyName("vendor")]
    public Vendor Vendor { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    /// The date and time the car will be picked up. (local-date-time)
    /// </summary>
    [JsonPropertyName("pickup_time")]
    public DateTime PickupTime { get; set; }

    /// <summary>
    /// The date and time the car will be dropped off. (local-date-time)
    /// </summary>
    [JsonPropertyName("dropoff_time")]
    public DateTime DropoffTime { get; set; }

    [JsonPropertyName("number_of_passengers")]
    public double NumberOfPassengers { get; set; }

    [JsonPropertyName("number_of_doors")]
    public NumberOfDoors NumberOfDoors { get; set; }

    [JsonPropertyName("acriss_code")]
    public string AcrissCode { get; set; }

    [JsonPropertyName("mileage_tracking")]
    public string MileageTracking { get; set; }

    [JsonPropertyName("luggage_count")]
    public LuggageCount LuggageCount { get; set; }

    [JsonPropertyName("fuel_level")]
    public string FuelLevel { get; set; }

    [JsonPropertyName("image")]
    public CarDetailsImage Image { get; set; }

    [JsonPropertyName("policies")]
    public List<Policy> Policies { get; set; } = [];

    [JsonPropertyName("rules_and_restrictions")]
    public List<RuleAndRestriction> RulesAndRestrictions { get; set; } = [];

    [JsonPropertyName("optional_extras")]
    public List<OptionalExtra> OptionalExtras { get; set; }

    [JsonPropertyName("rate")]
    public Rate Rate { get; set; }

    public List<CarCancelPenality>? CancelPenalties { get; set; }

    [JsonPropertyName("links")]
    public CarDetailsLinks Links { get; set; }
}

public class Policy
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("descriptions")]
    public List<string> Descriptions { get; set; }

    [JsonPropertyName("policy_statements")]
    public List<PolicyStatement> PolicyStatements { get; set; }

    [JsonPropertyName("policy_type")]
    public string PolicyType { get; set; }
}

public class PolicyStatement
{
    [JsonPropertyName("statement")]
    public string Statement { get; set; }

    [JsonPropertyName("statement_condition")]
    public string StatementCondition { get; set; }
}

public class RuleAndRestriction
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
    [JsonPropertyName("statement")]
    public string Statement { get; set; } = string.Empty;
}

public class OptionalExtra
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("daily_rate")]
    public Charge DailyRate { get; set; }
}

public class CarDetailsImage
{
    [JsonPropertyName("caption")]
    public string Caption { get; set; }

    [JsonPropertyName("links")]
    public Dictionary<string, Link> Links { get; set; }
}

public class CarDetailsLinks
{
    [JsonPropertyName("payment")]
    public Link Payment { get; set; }

    [JsonPropertyName("book")]
    public Link Book { get; set; }
}