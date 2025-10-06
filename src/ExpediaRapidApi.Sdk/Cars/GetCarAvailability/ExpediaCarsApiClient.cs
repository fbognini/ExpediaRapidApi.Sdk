using ExpediaRapidApi.Sdk.Cars.GetCarAvailability;
using fbognini.Sdk.Utils;

namespace ExpediaRapidApi.Sdk.Cars;

internal partial class ExpediaCarsApiClient
{
    public async Task<GetCarAvailabilityResponse> GetCarAvailabilityAsync(GetCarAvailabilityRequest request, CancellationToken cancellationToken = default)
    {
        return await GetApiAsync<GetCarAvailabilityResponse>(request.ToQueryString("v2/cars/availability", QueryStringBuilderFromJsonOptions), cancellationToken: cancellationToken);
    }
}