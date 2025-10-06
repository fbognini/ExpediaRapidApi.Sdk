using ExpediaRapidApi.Sdk.Cars.Bookings.CreateCarBooking;
using ExpediaRapidApi.Sdk.Cars.Bookings.RetrieveCarBooking;
using fbognini.Sdk.Utils;

namespace ExpediaRapidApi.Sdk.Cars;

internal partial class ExpediaCarsApiClient
{
    public async Task<RetrieveCarBookingResponse> RetrieveBooking(string itineraryId, CancellationToken cancellationToken = default)
    {
        return await GetApiAsync<RetrieveCarBookingResponse>($"v2/itineraries/{itineraryId}/car", cancellationToken: cancellationToken);
    }

    public async Task<RetrieveCarBookingResponse> RetrieveBookingByAffiliateReferenceId(string affiliateReferenceId, CancellationToken cancellationToken = default)
    {
        return await GetApiAsync<RetrieveCarBookingResponse>($"v2/itineraries/car?affiliate_reference_id={affiliateReferenceId}", cancellationToken: cancellationToken);
    }
}