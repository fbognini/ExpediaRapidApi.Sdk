using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models.Cars;


public class Vendor
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("image")]
    public VendorImage Image { get; set; }
}

public class VendorImage
{
    [JsonPropertyName("caption")]
    public string Caption { get; set; }

    [JsonPropertyName("links")]
    public Dictionary<string, Link> Links { get; set; }
}

public class NumberOfDoors
{
    [JsonPropertyName("min")]
    public int Min { get; set; }

    [JsonPropertyName("max")]
    public int Max { get; set; }
}

public class LuggageCount
{
    [JsonPropertyName("small")]
    public double Small { get; set; }

    [JsonPropertyName("large")]
    public double Large { get; set; }
}

public class Car
{
    [JsonPropertyName("car_rental_id")]
    public string CarRentalId { get; set; }

    [JsonPropertyName("vendor")]
    public Vendor Vendor { get; set; }

    [JsonPropertyName("locations")]
    public CarLocations Locations { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("number_of_passengers")]
    public double NumberOfPassengers { get; set; }

    [JsonPropertyName("number_of_doors")]
    public NumberOfDoors NumberOfDoors { get; set; }

    [JsonPropertyName("luggage_count")]
    public LuggageCount LuggageCount { get; set; }

    [JsonPropertyName("acriss_code")]
    public string AcrissCode { get; set; }

    [JsonPropertyName("mileage_tracking")]
    public string MileageTracking { get; set; }

    [JsonPropertyName("image")]
    public CarImage Image { get; set; }

    [JsonPropertyName("rate")]
    public Rate Rate { get; set; }

    [JsonPropertyName("cancel_penalties")]
    public List<CancelPenalty> CancelPenalties { get; set; }

    [JsonPropertyName("links")]
    public CarLinks Links { get; set; }
}

public class CarImage
{
    [JsonPropertyName("caption")]
    public string Caption { get; set; }

    [JsonPropertyName("links")]
    public CarImageLinks Links { get; set; }
}

public class CarImageLinks
{
    [JsonPropertyName("rental_image")]
    public Link RentalImage { get; set; }
}

public class CarLinks
{
    [JsonPropertyName("details")]
    public Link Details { get; set; }
}

public class Rate
{
    [JsonPropertyName("merchant_of_record")]
    public string MerchantOfRecord { get; set; }

    [JsonPropertyName("sale_scenario")]
    public SaleScenario SaleScenario { get; set; }

    [JsonPropertyName("pricing")]
    public Pricing Pricing { get; set; }
}

public class SaleScenario
{
    [JsonPropertyName("package")]
    public bool Package { get; set; }

    [JsonPropertyName("member")]
    public bool Member { get; set; }

    [JsonPropertyName("mobile_promotion")]
    public bool MobilePromotion { get; set; }
}

public class Pricing
{
    [JsonPropertyName("daily_rate_strikethrough")]
    public Charge DailyRateStrikethrough { get; set; }

    [JsonPropertyName("daily_rate")]
    public Charge DailyRate { get; set; }

    [JsonPropertyName("fees")]
    public List<Fee> Fees { get; set; }

    [JsonPropertyName("totals")]
    public PricingTotals Totals { get; set; }
}

public class PricingTotals
{
    [JsonPropertyName("inclusive_strikethrough")]
    public Charge InclusiveStrikethrough { get; set; }

    [JsonPropertyName("inclusive")]
    public Charge Inclusive { get; set; }

    [JsonPropertyName("exclusive")]
    public Charge Exclusive { get; set; }

    [JsonPropertyName("fees")]
    public Charge Fees { get; set; }

    [JsonPropertyName("extras")]
    public Charge Extras { get; set; }

    [JsonPropertyName("due_at_booking")]
    public Charge DueAtBooking { get; set; }
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

public class CarDetails
{
    [JsonPropertyName("locations")]
    public CarLocations Locations { get; set; }

    [JsonPropertyName("vendor")]
    public Vendor Vendor { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("pickup_time")]
    public DateTime PickupTime { get; set; }

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
    public List<Policy> Policies { get; set; }

    [JsonPropertyName("optional_extras")]
    public List<OptionalExtra> OptionalExtras { get; set; }

    [JsonPropertyName("rate")]
    public Rate Rate { get; set; }

    [JsonPropertyName("cancel_penalties")]
    public List<CancelPenalty> CancelPenalties { get; set; }

    [JsonPropertyName("links")]
    public CarDetailsLinks Links { get; set; }
}

public class CarDetailsImage
{
    [JsonPropertyName("caption")]
    public string Caption { get; set; }

    [JsonPropertyName("links")]
    public Dictionary<string, Link> Links { get; set; }
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

public class CarDetailsLinks
{
    [JsonPropertyName("payment")]
    public Link Payment { get; set; }

    [JsonPropertyName("book")]
    public Link Book { get; set; }
}

public class Fee
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }
}