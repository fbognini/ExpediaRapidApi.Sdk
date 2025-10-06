using ExpediaRapidApi.Sdk.Lodging.Bookings.CreateLodgingBooking;

namespace ExpediaRapidApi.Sdk.Lodging;

internal partial class ExpediaLodgingApiClient
{
    public async Task<CreateLodgingBookingResponse> CreateLodgingBooking(string token, CreateLodgingBookingRequest request, CreateLodgingBookingOptions options, CancellationToken cancellationToken = default)
    {
        return await PostApiAsync<CreateLodgingBookingResponse, CreateLodgingBookingRequest>($"v3/itineraries?token={token}", request, GetRequestOptions(options), cancellationToken: cancellationToken);
    }
}