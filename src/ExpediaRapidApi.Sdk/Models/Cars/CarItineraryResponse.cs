using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models.Cars;

public class CarItineraryResponse
{
    [JsonPropertyName("itinerary_id")]
    public string ItineraryId { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("confirmation_id")]
    public string ConfirmationId { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("phone")]
    public Phone Phone { get; set; }

    [JsonPropertyName("billing_contact")]
    public BillingContact BillingContact { get; set; }

    [JsonPropertyName("creation_time")]
    public DateTime CreationTime { get; set; }

    [JsonPropertyName("car_details")]
    public CarDetails CarDetails { get; set; }

    [JsonPropertyName("primary_driver")]
    public PrimaryDriver PrimaryDriver { get; set; }

    [JsonPropertyName("flight_details")]
    public FlightDetails FlightDetails { get; set; }

    [JsonPropertyName("traveler_loyalty_member_id")]
    public string TravelerLoyaltyMemberId { get; set; }

    [JsonPropertyName("affiliate_reference_id")]
    public string AffiliateReferenceId { get; set; }

    [JsonPropertyName("affiliate_metadata")]
    public string AffiliateMetadata { get; set; }

    [JsonPropertyName("links")]
    public CarItineraryLinks Links { get; set; }
}

public class PrimaryDriver
{
    [JsonPropertyName("given_name")]
    public string GivenName { get; set; }

    [JsonPropertyName("middle_name")]
    public string MiddleName { get; set; }

    [JsonPropertyName("family_name")]
    public string FamilyName { get; set; }

    [JsonPropertyName("phone")]
    public Phone Phone { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("age")]
    public int? Age { get; set; }
}

public class FlightDetails
{
    [JsonPropertyName("air_carrier_code")]
    public string AirCarrierCode { get; set; }

    [JsonPropertyName("flight_number")]
    public int FlightNumber { get; set; }
}

public class CarItineraryLinks
{
    [JsonPropertyName("cancel")]
    public Link Cancel { get; set; }
}
