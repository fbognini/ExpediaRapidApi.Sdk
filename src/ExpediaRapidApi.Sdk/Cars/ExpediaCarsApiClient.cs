using ExpediaRapidApi.Sdk.Cars.Bookings.CreateCarBooking;
using ExpediaRapidApi.Sdk.Cars.Bookings.RetrieveCarBooking;
using ExpediaRapidApi.Sdk.Cars.GetCarAvailability;
using Microsoft.Extensions.Options;

namespace ExpediaRapidApi.Sdk.Cars;

public interface IExpediaCarsApiClient
{
    Task<GetCarAvailabilityResponse> GetCarAvailabilityAsync(GetCarAvailabilityRequest request, CancellationToken cancellationToken = default);
    Task<CarDetails> GetCarDetailsAsync(string carRentalId, string token, CancellationToken cancellationToken = default);
    Task<CreateCarBookingResponse> CreateCarBooking(string token, CreateCarBookingRequest request, CancellationToken cancellationToken = default);
    Task<RetrieveCarBookingResponse> RetrieveBooking(string itineraryId, CancellationToken cancellationToken = default);
    Task<RetrieveCarBookingResponse> RetrieveBookingByAffiliateReferenceId(string affiliateReferenceId, CancellationToken cancellationToken = default);
    Task CancelBooking(string itineraryId, CancellationToken cancellationToken = default);
}

internal partial class ExpediaCarsApiClient : ExpediaBaseApiClient, IExpediaCarsApiClient
{
    public ExpediaCarsApiClient(HttpClient httpClient, IOptions<ExpediaRapidApiSettings> settings, IExpediaCurrentUserService? currentUserService) : base(httpClient, settings, currentUserService)
    {
        httpClient.BaseAddress = new Uri(Settings.BaseAddress);
    }
}