using ExpediaRapidApi.Sdk.Activities.Bookings.CancelActivityBooking;
using ExpediaRapidApi.Sdk.Activities.Bookings.CreateActivityBooking;
using ExpediaRapidApi.Sdk.Activities.Bookings.RetrieveActivityBooking;
using ExpediaRapidApi.Sdk.Activities.Content;
using ExpediaRapidApi.Sdk.Activities.Geography;
using ExpediaRapidApi.Sdk.Activities.Shopping;
using Microsoft.Extensions.Options;

namespace ExpediaRapidApi.Sdk.Activities;

/// <summary>
/// Rapid Activities V2. Authenticated with a dedicated OAuth2 client, distinct from the one used by Cars and Pay.
/// </summary>
public interface IExpediaActivitiesApiClient
{
    #region Geography

    /// <summary>
    /// Activity IDs within the bounding polygon of a region. Paginated.
    /// </summary>
    Task<ExpediaPaginationResponse<List<string>>> GetRegionActivities(GetRegionActivitiesRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Activity group IDs within the bounding polygon of a region. Paginated.
    /// </summary>
    Task<ExpediaPaginationResponse<List<string>>> GetRegionActivityGroups(GetRegionActivitiesRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Experience IDs within the bounding polygon of a region. Paginated.
    /// </summary>
    Task<ExpediaPaginationResponse<List<string>>> GetRegionExperiences(GetRegionActivitiesRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Follows a next-page link returned by any of the region endpoints, verbatim.
    /// </summary>
    Task<ExpediaPaginationResponse<List<string>>> GetRegionIdsByLink(string link, CancellationToken cancellationToken = default);

    #endregion

    #region Content

    /// <summary>
    /// Content of up to 20 activities.
    /// </summary>
    Task<List<ActivityContent>> GetActivitiesContent(GetActivitiesContentRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Content of up to 20 activity groups.
    /// </summary>
    Task<List<ActivityGroupContent>> GetActivityGroupsContent(GetActivityGroupsContentRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Content of up to 20 experiences.
    /// </summary>
    Task<List<ExperienceContent>> GetExperiencesContent(GetExperiencesContentRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Opening hours of an activity, for a window of at most 90 days.
    /// </summary>
    Task<List<ActivityOperatingHours>> GetActivityOperatingHours(GetActivityOperatingHoursRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Guest reviews of an activity.
    /// </summary>
    Task<List<GuestReview>> GetGuestReviews(GetGuestReviewsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Every category that activities can be tagged with. Paginated.
    /// The <see cref="ActivityReference.Type"/> of a category is the <see cref="ActivityReference.Name"/> of its
    /// parent: that is what makes the taxonomy a tree.
    /// </summary>
    Task<ExpediaPaginationResponse<List<ActivityReference>>> GetActivitiesCategories(GetReferencesRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Every attribute that activities can be tagged with. Paginated.
    /// </summary>
    Task<ExpediaPaginationResponse<List<ActivityReference>>> GetActivitiesAttributes(GetReferencesRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Every category that experiences can be tagged with. Paginated.
    /// </summary>
    Task<ExpediaPaginationResponse<List<ActivityReference>>> GetExperiencesCategories(GetReferencesRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Every attribute that experiences can be tagged with. Paginated.
    /// </summary>
    Task<ExpediaPaginationResponse<List<ActivityReference>>> GetExperiencesAttributes(GetReferencesRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Follows a next-page link returned by any of the reference endpoints, verbatim.
    /// </summary>
    Task<ExpediaPaginationResponse<List<ActivityReference>>> GetReferencesByLink(string link, CancellationToken cancellationToken = default);

    #endregion

    #region Shopping

    /// <summary>
    /// Availability, time slots and ticket pricing of up to 20 activities, for a window of at most 14 days.
    /// </summary>
    Task<ActivityAvailability> GetAvailability(GetAvailabilityRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Day-by-day availability and starting price of up to 20 activities, for a window of at most 30 days.
    /// This is the cheap call: use it to price a list of results.
    /// </summary>
    Task<List<CalendarAvailability>> GetCalendarAvailability(GetCalendarAvailabilityRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Final price of a selection of tickets on a time slot, plus the questions the booking will have to answer and the tokens to pay and create it. The token comes from the price_check link of the availability response.
    /// </summary>
    Task<ActivityPriceCheck> PriceCheck(PriceCheckRequest request, PriceCheckOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// The catalogue of booking question data types, each with the JSON Schema its answers must follow.
    /// </summary>
    // A technical reference, not a step of the booking flow.
    // Its use is to check that the types we declare in supported_booking_data_types still cover everything Rapid can ask: a type in this catalogue that we do not declare is fine, one that we declare and cannot render is a booking that fails after the card is charged.
    Task<BookingQuestionDataTypeCatalog> GetBookingQuestionDataTypes(GetBookingQuestionDataTypesRequest request, CancellationToken cancellationToken = default);

    #endregion

    #region Bookings

    /// <summary>
    /// Creates the booking. The token comes from the create link of the price check response.
    /// </summary>
    Task<CreateActivityBookingResponse> CreateBooking(string token, CreateActivityBookingRequest request, CreateActivityBookingOptions options, CancellationToken cancellationToken = default);

    Task<RetrieveActivityBookingResponse> RetrieveBooking(string itineraryId, RetrieveActivityBookingOptions options, CancellationToken cancellationToken = default);

    Task<List<RetrieveActivityBookingResponse>> RetrieveBookingByAffiliateReferenceId(string affiliateReferenceId, RetrieveActivityBookingOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Cancels the booking. Beware: a 202 means Expedia could not determine the state of the itinerary, so the
    /// cancellation is neither confirmed nor rejected — see <see cref="CancelActivityBookingResult"/>.
    /// </summary>
    Task<CancelActivityBookingResult> CancelBooking(string itineraryId, CancelActivityBookingOptions options, CancellationToken cancellationToken = default);

    /// <summary>
    /// Link to the voucher document of a booking.
    /// </summary>
    Task<Link> GetVoucher(string itineraryId, RetrieveActivityBookingOptions options, CancellationToken cancellationToken = default);

    #endregion
}

internal partial class ExpediaActivitiesApiClient : ExpediaBaseApiClient, IExpediaActivitiesApiClient
{
    public ExpediaActivitiesApiClient(HttpClient httpClient, IOptions<ExpediaRapidApiSettings> settings, IExpediaActivitiesCurrentUserService currentUserService)
        : base(httpClient, settings, currentUserService)
    {
        httpClient.BaseAddress = new Uri(Settings.BaseAddress);
    }

    /// <summary>
    /// Appends traffic_profile to a url already built, when the setting carries one.
    /// </summary>
    // Rapid wants it on every Activities request, so every method of every area goes through here rather than carrying the parameter on its own request object.
    // Next-page links are the exception: they are rebuilt on the url of the page that produced them, which already went through here.
    private string WithTrafficProfile(string url)
    {
        if (string.IsNullOrWhiteSpace(Settings.TrafficProfile))
        {
            return url;
        }

        var separator = url.Contains('?') ? '&' : '?';
        return $"{url}{separator}traffic_profile={Uri.EscapeDataString(Settings.TrafficProfile)}";
    }
}
