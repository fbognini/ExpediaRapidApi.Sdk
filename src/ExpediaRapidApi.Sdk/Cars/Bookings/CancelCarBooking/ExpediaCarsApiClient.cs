using ExpediaRapidApi.Sdk.Cars.Bookings.CancelCarBooking;

namespace ExpediaRapidApi.Sdk.Cars;

internal partial class ExpediaCarsApiClient
{
    public async Task CancelBooking(string itineraryId, CancelCarBookingOptions options, CancellationToken cancellationToken = default)
    {
        var requestOptions = GetRequestOptions(options);
        await DeleteApiAsync($"v2/itineraries/{itineraryId}/car", requestOptions, cancellationToken: cancellationToken);
    }
}