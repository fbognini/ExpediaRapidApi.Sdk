using ExpediaRapidApi.Sdk.Cars.Bookings.CreateCarBooking;

namespace ExpediaRapidApi.Sdk.Cars;

internal partial class ExpediaCarsApiClient
{
    public async Task<CreateCarBookingResponse> CreateCarBooking(string token, CreateCarBookingRequest request, CreateCarBookingOptions options, CancellationToken cancellationToken = default)
    {
        var requestOptions = GetRequestOptions(options);
        return await PostApiAsync<CreateCarBookingResponse, CreateCarBookingRequest>($"v2/itineraries/car?token={token}", request, requestOptions, cancellationToken: cancellationToken);
    }
}