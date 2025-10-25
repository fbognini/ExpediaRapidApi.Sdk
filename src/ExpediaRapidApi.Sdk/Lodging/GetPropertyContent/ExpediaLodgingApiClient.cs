using ExpediaRapidApi.Sdk.Cars.GetCarAvailability;
using ExpediaRapidApi.Sdk.Lodging.Endpoints;
using ExpediaRapidApi.Sdk.Lodging.GetPropertyContent;
using ExpediaRapidApi.Sdk.Models.Properties;
using fbognini.Sdk.Utils;

namespace ExpediaRapidApi.Sdk.Lodging;

internal partial class ExpediaLodgingApiClient
{
    public async Task<ExpediaPaginationResponse<GetPropertyContentResponse>> GetPropertyContent(GetPropertyContentRequest request, CancellationToken cancellationToken = default)
    {
        return await GetPaginatedApi<GetPropertyContentResponse>(request.ToQueryString("v3/properties/content", QueryStringBuilderFromJsonOptions), cancellationToken);
    }

    public async Task<ExpediaPaginationResponse<GetPropertyContentResponse>> GetPropertyContentByToken(string token, CancellationToken cancellationToken = default)
    {
        return await GetPaginatedApi<GetPropertyContentResponse>($"v3/properties/content?token={token}", cancellationToken);
    }
}