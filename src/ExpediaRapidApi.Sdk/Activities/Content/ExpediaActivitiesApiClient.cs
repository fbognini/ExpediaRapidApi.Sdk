using ExpediaRapidApi.Sdk.Activities.Content;
using fbognini.Sdk.Utils;

namespace ExpediaRapidApi.Sdk.Activities;

internal partial class ExpediaActivitiesApiClient
{
    public async Task<List<ActivityContent>> GetActivitiesContent(GetActivitiesContentRequest request, CancellationToken cancellationToken = default)
    {
        var url = request.ToQueryString("v2/experiences/activities/content", QueryStringBuilderFromJsonOptions);
        return await GetApiAsync<List<ActivityContent>>(url, cancellationToken: cancellationToken);
    }

    public async Task<List<ActivityGroupContent>> GetActivityGroupsContent(GetActivityGroupsContentRequest request, CancellationToken cancellationToken = default)
    {
        var url = request.ToQueryString("v2/experiences/activity-groups/content", QueryStringBuilderFromJsonOptions);
        return await GetApiAsync<List<ActivityGroupContent>>(url, cancellationToken: cancellationToken);
    }

    public async Task<List<ExperienceContent>> GetExperiencesContent(GetExperiencesContentRequest request, CancellationToken cancellationToken = default)
    {
        var url = request.ToQueryString("v2/experiences/content", QueryStringBuilderFromJsonOptions);
        return await GetApiAsync<List<ExperienceContent>>(url, cancellationToken: cancellationToken);
    }

    public async Task<List<ActivityOperatingHours>> GetActivityOperatingHours(GetActivityOperatingHoursRequest request, CancellationToken cancellationToken = default)
    {
        var url = request.ToQueryString($"v2/experiences/activities/{request.ActivityId}/operating-hours", QueryStringBuilderFromJsonOptions);
        return await GetApiAsync<List<ActivityOperatingHours>>(url, cancellationToken: cancellationToken);
    }

    public async Task<List<GuestReview>> GetGuestReviews(GetGuestReviewsRequest request, CancellationToken cancellationToken = default)
    {
        var url = request.ToQueryString($"v2/experiences/activities/{request.ActivityId}/guest-reviews", QueryStringBuilderFromJsonOptions);
        return await GetApiAsync<List<GuestReview>>(url, cancellationToken: cancellationToken);
    }

    public Task<ExpediaPaginationResponse<List<ActivityReference>>> GetActivitiesCategories(GetReferencesRequest request, CancellationToken cancellationToken = default)
        => GetReferences(request, "v2/experiences/activities/categories", cancellationToken);

    public Task<ExpediaPaginationResponse<List<ActivityReference>>> GetActivitiesAttributes(GetReferencesRequest request, CancellationToken cancellationToken = default)
        => GetReferences(request, "v2/experiences/activities/attributes", cancellationToken);

    public Task<ExpediaPaginationResponse<List<ActivityReference>>> GetExperiencesCategories(GetReferencesRequest request, CancellationToken cancellationToken = default)
        => GetReferences(request, "v2/experiences/categories", cancellationToken);

    public Task<ExpediaPaginationResponse<List<ActivityReference>>> GetExperiencesAttributes(GetReferencesRequest request, CancellationToken cancellationToken = default)
        => GetReferences(request, "v2/experiences/attributes", cancellationToken);

    public async Task<ExpediaPaginationResponse<List<ActivityReference>>> GetReferencesByLink(string link, CancellationToken cancellationToken = default)
    {
        return await GetPaginatedApi<List<ActivityReference>>(link, cancellationToken: cancellationToken);
    }

    private async Task<ExpediaPaginationResponse<List<ActivityReference>>> GetReferences(GetReferencesRequest request, string path, CancellationToken cancellationToken)
    {
        var url = request.ToQueryString(path, QueryStringBuilderFromJsonOptions);
        return await GetPaginatedApi<List<ActivityReference>>(url, cancellationToken: cancellationToken);
    }
}
