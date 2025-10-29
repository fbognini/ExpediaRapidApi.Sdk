using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Cars.Bookings;

public class PrimaryDriver
{
    [JsonPropertyName("given_name")]
    public string GivenName { get; set; }

    [JsonPropertyName("middle_name")]
    public string? MiddleName { get; set; }

    [JsonPropertyName("family_name")]
    public string FamilyName { get; set; }

    [JsonPropertyName("phone")]
    public Phone Phone { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("age")]
    public int? Age { get; set; }
}
