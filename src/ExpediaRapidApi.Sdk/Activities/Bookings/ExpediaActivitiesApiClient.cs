using ExpediaRapidApi.Sdk.Activities.Bookings.CancelActivityBooking;
using ExpediaRapidApi.Sdk.Activities.Bookings.CreateActivityBooking;
using ExpediaRapidApi.Sdk.Activities.Bookings.RetrieveActivityBooking;
using System.Net;

namespace ExpediaRapidApi.Sdk.Activities;

internal partial class ExpediaActivitiesApiClient
{
    public async Task<CreateActivityBookingResponse> CreateBooking(string token, CreateActivityBookingRequest request, CreateActivityBookingOptions options, CancellationToken cancellationToken = default)
    {
        var requestOptions = GetRequestOptions(options);
        return await PostApiAsync<CreateActivityBookingResponse, CreateActivityBookingRequest>($"v2/itineraries/activity?token={token}", request, requestOptions, cancellationToken: cancellationToken);
    }

    public async Task<RetrieveActivityBookingResponse> RetrieveBooking(string itineraryId, RetrieveActivityBookingOptions options, CancellationToken cancellationToken = default)
    {
        var requestOptions = GetRequestOptions(options);
        return await GetApiAsync<RetrieveActivityBookingResponse>($"v2/itineraries/{itineraryId}/activity", requestOptions, cancellationToken);
    }

    public async Task<List<RetrieveActivityBookingResponse>> RetrieveBookingByAffiliateReferenceId(string affiliateReferenceId, RetrieveActivityBookingOptions options, CancellationToken cancellationToken = default)
    {
        var requestOptions = GetRequestOptions(options);
        return await GetApiAsync<List<RetrieveActivityBookingResponse>>($"v2/itineraries/activity?affiliate_reference_id={Uri.EscapeDataString(affiliateReferenceId)}", requestOptions, cancellationToken);
    }

    public async Task<CancelActivityBookingResult> CancelBooking(string itineraryId, CancelActivityBookingOptions options, CancellationToken cancellationToken = default)
    {
        var requestOptions = GetRequestOptions(options);
        var response = await DeleteApiAsync($"v2/itineraries/{itineraryId}/activity", requestOptions, cancellationToken);

        // A 202 is not a success: Rapid is telling us it could not determine the state of the itinerary.
        return response.StatusCode == HttpStatusCode.Accepted
            ? CancelActivityBookingResult.Unknown
            : CancelActivityBookingResult.Cancelled;
    }

    public async Task<Link> GetVoucher(string itineraryId, RetrieveActivityBookingOptions options, CancellationToken cancellationToken = default)
    {
        var requestOptions = GetRequestOptions(options);
        return await GetApiAsync<Link>($"v2/itineraries/{itineraryId}/activity/voucher", requestOptions, cancellationToken);
    }
}
