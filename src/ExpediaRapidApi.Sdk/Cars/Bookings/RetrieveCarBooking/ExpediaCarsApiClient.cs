using ExpediaRapidApi.Sdk.Cars.Bookings.RetrieveCarBooking;
namespace ExpediaRapidApi.Sdk.Cars;

internal partial class ExpediaCarsApiClient
{
    public async Task<RetrieveCarBookingResponse> RetrieveBooking(string itineraryId, RetrieveCarBookingOptions options, CancellationToken cancellationToken = default)
    {
        var requestOptions = GetRequestOptions(options);
        return await GetApiAsync<RetrieveCarBookingResponse>($"v2/itineraries/{itineraryId}/car", requestOptions, cancellationToken: cancellationToken);
    }

    public async Task<RetrieveCarBookingResponse> RetrieveBookingByAffiliateReferenceId(string affiliateReferenceId, RetrieveCarBookingOptions options, CancellationToken cancellationToken = default)
    {
        var requestOptions = GetRequestOptions(options);
        return await GetApiAsync<RetrieveCarBookingResponse>($"v2/itineraries/car?affiliate_reference_id={affiliateReferenceId}", requestOptions, cancellationToken: cancellationToken);
    }
}