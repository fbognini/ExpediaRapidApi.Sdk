using ExpediaRapidApi.Sdk.Activities.Geography;
using fbognini.Sdk.Utils;

namespace ExpediaRapidApi.Sdk.Activities;

internal partial class ExpediaActivitiesApiClient
{
    public async Task<ExpediaPaginationResponse<List<string>>> GetRegionActivities(GetRegionActivitiesRequest request, CancellationToken cancellationToken = default)
    {
        var url = WithTrafficProfile(request.ToQueryString($"v2/regions/{request.RegionId}/activities", QueryStringBuilderFromJsonOptions));
        return await GetPaginatedApi<List<string>>(url, cancellationToken: cancellationToken);
    }

    public async Task<ExpediaPaginationResponse<List<string>>> GetRegionActivityGroups(GetRegionActivitiesRequest request, CancellationToken cancellationToken = default)
    {
        var url = WithTrafficProfile(request.ToQueryString($"v2/regions/{request.RegionId}/activity-groups", QueryStringBuilderFromJsonOptions));
        return await GetPaginatedApi<List<string>>(url, cancellationToken: cancellationToken);
    }

    public async Task<ExpediaPaginationResponse<List<string>>> GetRegionExperiences(GetRegionActivitiesRequest request, CancellationToken cancellationToken = default)
    {
        var url = WithTrafficProfile(request.ToQueryString($"v2/regions/{request.RegionId}/experiences", QueryStringBuilderFromJsonOptions));
        return await GetPaginatedApi<List<string>>(url, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// The link comes from ExpediaPaginationResponse.NextPageLink, already complete: traffic profile and page size are the ones of the first page.
    /// </summary>
    public async Task<ExpediaPaginationResponse<List<string>>> GetRegionIdsByLink(string link, CancellationToken cancellationToken = default)
    {
        return await GetPaginatedApi<List<string>>(link, cancellationToken: cancellationToken);
    }
}
