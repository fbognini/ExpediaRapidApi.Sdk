namespace ExpediaRapidApi.Sdk.Activities.Content;

/// <summary>
/// Filters shared by every content and geography endpoint. All four accept 1 to 20 values.
/// </summary>
public abstract class ActivityFilterRequest
{
    public List<string>? CategoryId { get; set; }

    public List<string>? CategoryIdExclude { get; set; }

    public List<string>? AttributeId { get; set; }

    public List<string>? AttributeIdExclude { get; set; }
}

public class GetActivitiesContentRequest : ActivityFilterRequest
{
    /// <summary>
    /// The activities to fetch. Between 1 and <see cref="MaxIdsPerRequest"/>.
    /// </summary>
    public List<string> ActivityId { get; set; } = [];

    /// <summary>
    /// Desired language, as hyphenated ISO 639-1 / ISO 3166-1 alpha-2 pairs. E.g. "it-IT".
    /// </summary>
    public string Language { get; set; } = default!;

    /// <summary>
    /// Only return content updated on or after this UTC date. This is what makes the nightly import a delta
    /// instead of a full reload.
    /// </summary>
    public DateOnly? DateUpdatedStart { get; set; }

    public const int MaxIdsPerRequest = 20;
}

public class GetActivityGroupsContentRequest : ActivityFilterRequest
{
    /// <summary>
    /// The activity groups to fetch. Between 1 and <see cref="MaxIdsPerRequest"/>.
    /// </summary>
    public List<string> ActivityGroupId { get; set; } = [];

    public string Language { get; set; } = default!;

    public DateOnly? DateUpdatedStart { get; set; }

    public const int MaxIdsPerRequest = 25;
}

public class GetExperiencesContentRequest : ActivityFilterRequest
{
    /// <summary>
    /// The experiences to fetch. Between 1 and <see cref="MaxIdsPerRequest"/>.
    /// </summary>
    public List<string> ExperienceId { get; set; } = [];

    public string Language { get; set; } = default!;

    public DateOnly? DateUpdatedStart { get; set; }

    public const int MaxIdsPerRequest = 20;
}

public class GetActivityOperatingHoursRequest
{
    /// <summary>
    /// Path parameter: not part of the query string.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    public string ActivityId { get; set; } = default!;

    public string Language { get; set; } = default!;

    public DateOnly StartDate { get; set; }

    /// <summary>
    /// At most <see cref="MaxDays"/> days after <see cref="StartDate"/>.
    /// </summary>
    public DateOnly EndDate { get; set; }

    public const int MaxDays = 90;
}

public class GetGuestReviewsRequest
{
    /// <summary>
    /// Path parameter: not part of the query string.
    /// </summary>
    [System.Text.Json.Serialization.JsonIgnore]
    public string ActivityId { get; set; } = default!;

    public string Language { get; set; } = default!;

    /// <summary>
    /// Between 1 and 100.
    /// </summary>
    public int Limit { get; set; } = 10;

    public GuestReviewsSort Sort { get; set; } = GuestReviewsSort.relevancy;
}

public enum GuestReviewsSort
{
    date,
    relevancy,
    rating
}

/// <summary>
/// Request for the four reference endpoints (activity and experience categories and attributes).
/// </summary>
public class GetReferencesRequest
{
    /// <summary>
    /// Results per page. Between 1 and 100.
    /// </summary>
    public int PaginationSize { get; set; } = 100;

    public string Language { get; set; } = default!;
}
