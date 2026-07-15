using ExpediaRapidApi.Sdk.Activities.Content;
using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Activities.Geography;

/// <summary>
/// Lists the ids that fall within the bounding polygon of a region. The same shape serves activities, activity
/// groups and experiences.
/// </summary>
public class GetRegionActivitiesRequest : ActivityFilterRequest
{
    /// <summary>
    /// Path parameter: not part of the query string.
    /// </summary>
    [JsonIgnore]
    public string RegionId { get; set; } = default!;
}
