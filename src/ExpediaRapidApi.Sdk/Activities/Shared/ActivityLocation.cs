using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Activities.Shared;

public class ActivityLocation
{
    public ActivityAddress? Address { get; set; }

    public Coordinates? Coordinate { get; set; }
}

public class ActivityAddress
{
    // The snake_case naming policy would turn "Line1" into "line1", but Rapid sends "line_1".
    [JsonPropertyName("line_1")]
    public string? Line1 { get; set; }

    [JsonPropertyName("line_2")]
    public string? Line2 { get; set; }

    [JsonPropertyName("line_3")]
    public string? Line3 { get; set; }

    public string? City { get; set; }

    public string? StateProvinceCode { get; set; }

    public string? PostalCode { get; set; }

    /// <summary>
    /// Two-letter country code, in ISO 3166-1 alpha-2 format.
    /// </summary>
    public string? CountryCode { get; set; }
}

/// <summary>
/// An image or a video associated with an activity, experience or activity group.
/// </summary>
public class ActivityMedia
{
    public string? Caption { get; set; }

    public string? Credit { get; set; }

    /// <summary>
    /// One link per available size, keyed by size ("default", "350px", "1000px", ...).
    /// </summary>
    public Dictionary<string, Link> Links { get; set; } = [];
}

/// <summary>
/// The images and videos of an activity, experience or activity group.
/// </summary>
public class ActivityMediaCollection
{
    public List<ActivityMedia> Images { get; set; } = [];

    public List<ActivityMedia> Videos { get; set; } = [];
}

public class StarRating
{
    /// <summary>
    /// How many guest reviews this rating averages.
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// The average rating, between 1.0 and 5.0.
    /// </summary>
    public string? Overall { get; set; }
}
