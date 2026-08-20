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
        var url = WithTrafficProfile($"v2/itineraries/activity?token={token}");
        return await PostApiAsync<CreateActivityBookingResponse, CreateActivityBookingRequest>(url, request, requestOptions, cancellationToken: cancellationToken);
    }

    public async Task<RetrieveActivityBookingResponse> RetrieveBooking(string itineraryId, RetrieveActivityBookingOptions options, CancellationToken cancellationToken = default)
    {
        var requestOptions = GetRequestOptions(options);
        var url = WithTrafficProfile($"v2/itineraries/{itineraryId}/activity");
        return await GetApiAsync<RetrieveActivityBookingResponse>(url, requestOptions, cancellationToken);
    }

    public async Task<List<RetrieveActivityBookingResponse>> RetrieveBookingByAffiliateReferenceId(string affiliateReferenceId, RetrieveActivityBookingOptions options, CancellationToken cancellationToken = default)
    {
        var requestOptions = GetRequestOptions(options);
        var url = WithTrafficProfile($"v2/itineraries/activity?affiliate_reference_id={Uri.EscapeDataString(affiliateReferenceId)}");
        return await GetApiAsync<List<RetrieveActivityBookingResponse>>(url, requestOptions, cancellationToken);
    }

    public async Task<CancelActivityBookingResult> CancelBooking(string itineraryId, CancelActivityBookingOptions options, CancellationToken cancellationToken = default)
    {
        var requestOptions = GetRequestOptions(options);
        var url = WithTrafficProfile($"v2/itineraries/{itineraryId}/activity");
        var response = await DeleteApiAsync(url, requestOptions, cancellationToken);

        // A 202 is not a success: Rapid is telling us it could not determine the state of the itinerary.
        return response.StatusCode == HttpStatusCode.Accepted
            ? CancelActivityBookingResult.Unknown
            : CancelActivityBookingResult.Cancelled;
    }

    public async Task<Link> GetVoucher(string itineraryId, RetrieveActivityBookingOptions options, CancellationToken cancellationToken = default)
    {
        var requestOptions = GetRequestOptions(options);
        var url = WithTrafficProfile($"v2/itineraries/{itineraryId}/activity/voucher");
        return await GetApiAsync<Link>(url, requestOptions, cancellationToken);
    }
}
