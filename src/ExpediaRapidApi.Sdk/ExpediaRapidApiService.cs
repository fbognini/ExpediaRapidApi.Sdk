using ExpediaRapidApi.Sdk.Endpoints;
using ExpediaRapidApi.Sdk.Models;
using ExpediaRapidApi.Sdk.Models.Bookings;
using ExpediaRapidApi.Sdk.Models.Properties;
using ExpediaRapidApi.Sdk.Requests;
using fbognini.Sdk;
using fbognini.Sdk.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

namespace ExpediaRapidApi.Sdk
{
    public interface IExpediaRapidApiService
    {
        Task CancelHeldBooking(string id, string token);
        Task<BookingResponse> CreateBooking(string token, CreateBookingRequest request, string clientId);
        Task<ItineraryDetailResponse> GetBookingFromToken(string id, string token, string clientIp);
        Task<ItineraryDetailResponse> GetBookingFromEmail(string id, string email, string clientIp);

        Task<ExpediaPaginationResponse<Dictionary<string, PropertyContent>>> GetPropertyContent(string language, List<string> categoryIdToExclude, DateTime? dateAddedStart, DateTime? dateUpdatedStart, string supplySource, string? partnerPointOfSale, string? paymentTerms, string? platformName);
        Task<ExpediaPaginationResponse<Dictionary<string, PropertyContent>>> GetPropertyContentByToken(string token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkin"></param>
        /// <param name="checkout"></param>
        /// <param name="currency"></param>
        /// <param name="language"></param>
        /// <param name="countryCode"></param>
        /// <param name="occupancy"></param>
        /// <param name="propertyIds"></param>
        /// <param name="salesChannel"></param>
        /// <param name="salesEnvironment"></param>
        /// <param name="filter"></param>
        /// <param name="ratePlanCount">
        /// The number of rates to return per property. The rates with the best value will be returned, e.g. a rate_plan_count=4 will return the best 4 rates, but the rates are not ordered from lowest to highest or vice versa in the response. Generally lowest rates will be prioritized.The value must be between 1 and 250.
        /// </param>
        /// <param name="rateOption"></param>
        /// <param name="billingTerms"></param>
        /// <param name="paymentTerms"></param>
        /// <param name="partnerPointOfSale"></param>
        /// <param name="platformName"></param>
        /// <param name="exclusion"></param>
        /// <returns></returns>
        Task<PropertyAvailabilityResponse> GetPropertiesAvailability(DateOnly checkin, DateOnly checkout, string currency, string language, string countryCode, string sortType, List<string> occupancy, List<string> propertyIds, string salesChannel, string salesEnvironment, List<string>? filter, int ratePlanCount, List<string>? rateOption, string? billingTerms, string? paymentTerms, string? partnerPointOfSale, string? platformName, List<string>? exclusion);
        Task<ExpediaPaginationResponse<List<Region>>> GetRegions(string language, string? anchestorId, string? iataLocationCode, List<string>? include);
        Task<PriceCheckResponse> PriceCheck(string propertyId, string roomId, string rateId, string token);
        Task<GuestReviewsResponse> GetPropertyGuestReviews(long propertyId, string language);
        Task ResumeBooking(string id, string token);
        Task CancelBookingRoom(string relativeUrl, string clientIp);
        Task<ExpediaPaginationResponse<List<Region>>> GetRegionsByToken(string token);
        Task<LinkHref> GetFilePropertyContent(string language, string supplySource);
        (string Signature, double UnixTime) GetSignature();
    }

    /// <summary>
    /// Documentation at https://developers.expediagroup.com/docs/rapid/resources/rapid-api
    /// </summary>
    internal class ExpediaRapidApiService : BaseApiService, IExpediaRapidApiService
    {
        private readonly ExpediaRapidApiSettings settings;

        public ExpediaRapidApiService(HttpClient client, ILogger<ExpediaRapidApiService> logger, IOptions<ExpediaRapidApiSettings> options)
            : base(client, logger, null)
        {
            this.settings = options.Value;

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.AcceptEncoding.Clear();
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));


            client.DefaultRequestHeaders.UserAgent.ParseAdd("GHC/2019");
            client.BaseAddress = new Uri($"{settings.Url}/");

            var header = GetAuthorizationHeader();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("EAN", header);
        }

        public (string Signature, double UnixTime) GetSignature()
        {

            TimeSpan epochTicks = new TimeSpan(new DateTime(1970, 1, 1).Ticks);
            TimeSpan unixTicks = new TimeSpan(DateTime.UtcNow.Ticks) - epochTicks;
            double unixTime = (int)unixTicks.TotalSeconds;
            var toBeHashed = settings.ApiKey + settings.ApiSecret + unixTime;
            var bytes = Encoding.UTF8.GetBytes(toBeHashed);

            var hashedInputBytes = System.Security.Cryptography.SHA512.HashData(bytes);
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
            {
                hashedInputStringBuilder.Append(b.ToString("X2"));
            }
            var signature = hashedInputStringBuilder.ToString();

            return (signature, unixTime);
        }
        
        protected async Task<ExpediaPaginationResponse<T>> GetPaginatedApi<T>(string url)
        {
            var response = await GetApi(url);

            response.Headers.TryGetValues("Link", out var nextPages);
            string? nextPageLink = nextPages?.FirstOrDefault();
            if (!string.IsNullOrEmpty(nextPageLink))
            {
                int from = nextPageLink.IndexOf("<");
                int to = nextPageLink.LastIndexOf(">");
                nextPageLink = nextPageLink.Substring(from + 1, to - from - 1);
            }

            return new ExpediaPaginationResponse<T>()
            {
                NextPageLink = nextPageLink,
                Response = (await response.Content.ReadFromJsonAsync<T>())!
            };
        }

        #region Regions

        public async Task<ExpediaPaginationResponse<List<Region>>> GetRegions(
            string language,
            string? ancestorId,
            string? iataLocationCode,
            List<string>? include)
        {
            string url = RegionEndpoints.GetRegions(
                language: language,
                ancestorId: ancestorId,
                iataLocationCode: iataLocationCode,
                include: include);

            return await GetPaginatedApi<List<Region>>(url);
        }

        public async Task<ExpediaPaginationResponse<List<Region>>> GetRegionsByToken(string token)
        {
            string url = RegionEndpoints.GetRegionsByToken(token);

            return await GetPaginatedApi<List<Region>>(url);
        }

        #endregion

        #region Properties

        public async Task<ExpediaPaginationResponse<Dictionary<string, PropertyContent>>> GetPropertyContent(
            string language,
            List<string> categoryIdToExclude,
            DateTime? dateAddedStart,
            DateTime? dateUpdatedStart,
            string supplySource,
            string? partnerPointOfSale,
            string? paymentTerms,
            string? platformName)
        {
            string url = PropertyEndpoints.GetContent(
                language: language,
                categoryIdToExclude: categoryIdToExclude,
                dateAddedStart: dateAddedStart,
                dateUpdatedStart: dateUpdatedStart,
                supplySource: supplySource,
                partnerPointOfSale: partnerPointOfSale,
                paymentTerms: paymentTerms,
                platformName: platformName
            );

            return await GetPaginatedApi<Dictionary<string, PropertyContent>>(url);
        }

        public async Task<ExpediaPaginationResponse<Dictionary<string, PropertyContent>>> GetPropertyContentByToken(string token)
        {
            string url = PropertyEndpoints.GetContentByToken(token);

            return await GetPaginatedApi<Dictionary<string, PropertyContent>>(url);
        }

        public async Task<PropertyAvailabilityResponse> GetPropertiesAvailability(
            DateOnly checkin,
            DateOnly checkout,
            string currency,
            string language,
            string countryCode,
            string sortType,
            List<string> occupancy,
            List<string> propertyIds,
            string salesChannel,
            string salesEnvironment,
            List<string>? filter,
            int ratePlanCount,
            List<string>? rateOption,
            string? billingTerms,
            string? paymentTerms,
            string? partnerPointOfSale,
            string? platformName,
            List<string>? exclusion)
        {
            return await GetApi<PropertyAvailabilityResponse>(PropertyEndpoints.GetAvailability(
                checkin: checkin,
                checkout: checkout,
                currency: currency,
                language: language,
                countryCode: countryCode,
                sortType: sortType,
                occupancy: occupancy,
                propertyIds: propertyIds,
                salesChannel: salesChannel,
                salesEnvironment: salesEnvironment,
                filter: filter,
                ratePlanCount: ratePlanCount,
                rateOption: rateOption,
                billingTerms: billingTerms,
                paymentTerms: paymentTerms,
                partnerPointOfSale: partnerPointOfSale,
                platformName: platformName,
                exclusion: exclusion));
        }

        public async Task<PriceCheckResponse> PriceCheck(
            string propertyId,
            string roomId,
            string rateId,
            string token)
        {
            return await GetApi<PriceCheckResponse>(PropertyEndpoints.PriceCheck(propertyId, roomId, rateId, token));
        }

        public async Task<GuestReviewsResponse> GetPropertyGuestReviews(long propertyId, string language)
        {
            return await GetApi<GuestReviewsResponse>(PropertyEndpoints.GuestReviews(propertyId, language));
        }

        public async Task<LinkHref> GetFilePropertyContent(string language, string supplySource)
        {
            return await GetApi<LinkHref>(PropertyEndpoints.PropertyContentFile(language, supplySource));
        }

        #endregion

        #region Booking

        public async Task<BookingResponse> CreateBooking(string token, CreateBookingRequest request, string clientIp)
        {
            AddClientIpHeader(clientIp);

            return await PostApi<BookingResponse, CreateBookingRequest>(BookingEndpoints.Create(token), request);
        }

        public async Task ResumeBooking(string id, string token)
        {
            await PutApi(BookingEndpoints.Resume(id, token));
        }

        public async Task CancelHeldBooking(string id, string token)
        {
            await DeleteApi(BookingEndpoints.Cancel(id, token));
        }

        public async Task CancelBookingRoom(string relativeUrl, string clientIp)
        {
            AddClientIpHeader(clientIp);
            await DeleteApi(relativeUrl);
        }

        public async Task<ItineraryDetailResponse> GetBookingFromToken(string id, string token, string clientIp)
        {
            return await GetBooking(id, token, null, clientIp);
        }

        public async Task<ItineraryDetailResponse> GetBookingFromEmail(string id, string email, string clientIp)
        {
            return await GetBooking(id, null, email, clientIp);
        }


        private async Task<ItineraryDetailResponse> GetBooking(string id, string? token, string? email, string clientIp)
        {
            AddClientIpHeader(clientIp);
            return await GetApi<ItineraryDetailResponse>(BookingEndpoints.Get(id, token, email));
        }

        private void AddClientIpHeader(string clientIp)
        {
            client.DefaultRequestHeaders.Remove("Customer-Ip");
            
            if (!string.IsNullOrEmpty(clientIp))
            {
                client.DefaultRequestHeaders.Add("Customer-Ip", clientIp);
            }
        }

        private string GetAuthorizationHeader()
        {
            var (signature, unixTime) = GetSignature();
            var authHeaderValue = "APIKey=" + settings.ApiKey + ",Signature=" + signature + ",timestamp=" + unixTime;

            return authHeaderValue;
        }

        #endregion
    }
}
