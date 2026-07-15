using ExpediaRapidApi.Sdk.Activities.Shopping;
using fbognini.Sdk.Utils;

namespace ExpediaRapidApi.Sdk.Activities;

internal partial class ExpediaActivitiesApiClient
{
    public async Task<ActivityAvailability> GetAvailability(GetAvailabilityRequest request, CancellationToken cancellationToken = default)
    {
        var url = request.ToQueryString("v2/experiences/activities/availability", QueryStringBuilderFromJsonOptions);
        return await GetApiAsync<ActivityAvailability>(url, cancellationToken: cancellationToken);
    }

    public async Task<List<CalendarAvailability>> GetCalendarAvailability(GetCalendarAvailabilityRequest request, CancellationToken cancellationToken = default)
    {
        var url = request.ToQueryString("v2/experiences/activities/calendars/availability", QueryStringBuilderFromJsonOptions);
        return await GetApiAsync<List<CalendarAvailability>>(url, cancellationToken: cancellationToken);
    }

    public async Task<ActivityPriceCheck> PriceCheck(PriceCheckRequest request, PriceCheckOptions options, CancellationToken cancellationToken = default)
    {
        var requestOptions = GetRequestOptions(options);
        var url = request.ToQueryString($"v2/experiences/activities/{request.ActivityId}/price-check", QueryStringBuilderFromJsonOptions);
        return await GetApiAsync<ActivityPriceCheck>(url, requestOptions, cancellationToken);
    }
}
