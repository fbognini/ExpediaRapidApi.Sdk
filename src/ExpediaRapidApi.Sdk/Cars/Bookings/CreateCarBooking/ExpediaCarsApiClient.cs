using ExpediaRapidApi.Sdk.Cars.Bookings.CreateCarBooking;

namespace ExpediaRapidApi.Sdk.Cars;

internal partial class ExpediaCarsApiClient
{
    public async Task<CreateCarBookingResponse> CreateCarBooking(string token, CreateCarBookingRequest request, CancellationToken cancellationToken = default)
    {
        return await PostApiAsync<CreateCarBookingResponse, CreateCarBookingRequest>($"v2/itineraries/car?token={token}", request, cancellationToken: cancellationToken);
    }
}