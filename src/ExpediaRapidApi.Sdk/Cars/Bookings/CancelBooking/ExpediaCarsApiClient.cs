using ExpediaRapidApi.Sdk.Cars.Bookings.CreateCarBooking;
using ExpediaRapidApi.Sdk.Cars.Bookings.RetrieveCarBooking;
using fbognini.Sdk.Utils;

namespace ExpediaRapidApi.Sdk.Cars;

internal partial class ExpediaCarsApiClient
{
    public async Task CancelBooking(string itineraryId, CancellationToken cancellationToken = default)
    {
        await DeleteApiAsync<RetrieveCarBookingResponse>($"v2/itineraries/{itineraryId}/car", cancellationToken: cancellationToken);
    }
}