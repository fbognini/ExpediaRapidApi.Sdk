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

    /// <summary>
    /// How many ids to return per page, between 1 and 100.
    /// Rapid is making pagination mandatory on the geography endpoints; the pages themselves are followed through the Link header as before.
    /// </summary>
    public int PaginationSize { get; set; } = MaxPaginationSize;

    public const int MaxPaginationSize = 100;
}
