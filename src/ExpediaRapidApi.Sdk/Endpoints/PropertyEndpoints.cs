﻿using ExpediaRapidApi.Sdk.Utils;

namespace ExpediaRapidApi.Sdk.Endpoints
{
    public static class PropertyEndpoints
    {
        public static string GetContent(
            string language,
            List<string> categoryIdToExclude,
            DateTime? dateAddedStart,
            DateTime? dateUpdatedStart,
            string supplySource,
            string? partnerPointOfSale,
            string? paymentTerms,
            string? platformName)
        {
            var queryparameters = new List<KeyValuePair<string, string>>()
            {
                new ("language", language),
                new ("supply_source", supplySource),
            };

            if (dateAddedStart is not null)
                queryparameters.Add(new KeyValuePair<string, string>("date_added_start", dateAddedStart!.Value!.ToString("yyyy-MM-dd")));

            if (dateUpdatedStart is not null)
                queryparameters.Add(new KeyValuePair<string, string>("date_updated_start", dateUpdatedStart!.Value!.ToString("yyyy-MM-dd")));

            if (paymentTerms is not null)
                queryparameters.Add(new KeyValuePair<string, string>("payment_terms", paymentTerms!));

            if (partnerPointOfSale is not null)
                queryparameters.Add(new KeyValuePair<string, string>("partner_point_of_sale", partnerPointOfSale!));

            if (platformName is not null)
                queryparameters.Add(new KeyValuePair<string, string>("platform_name", platformName!));

            categoryIdToExclude?.ForEach(item => queryparameters.Add(new KeyValuePair<string, string>("category_id_exclude", item)));

            return $"v3/properties/content{UriHelpers.GetQueryParameters(queryparameters)}";
        }

        public static string GetContentByToken(string token)
        {
            return $"v3/properties/content?token={token}";
        }

        public static string GetAvailability(
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
            var queryparameters = new List<KeyValuePair<string, string>>()
            {
                new ("checkin", checkin.ToString("yyyy-MM-dd")),
                new ("checkout", checkout.ToString("yyyy-MM-dd")),
                new ("currency", currency),
                new ("language", language),
                new ("country_code", countryCode),
                new ("sort_type", sortType),
                new ("sales_channel", salesChannel),
                new ("sales_environment", salesEnvironment),
                new ("rate_plan_count", ratePlanCount.ToString()),
            };
            occupancy.ForEach(item => queryparameters.Add(new KeyValuePair<string, string>("occupancy", item)));
            propertyIds.ForEach(item => queryparameters.Add(new KeyValuePair<string, string>("property_id", item)));
            filter?.ForEach(item => queryparameters.Add(new KeyValuePair<string, string>("filter", item)));
            rateOption?.ForEach(item => queryparameters.Add(new KeyValuePair<string, string>("rate_option", item)));

            if (billingTerms is not null)
                queryparameters.Add(new KeyValuePair<string, string>("billing_terms", billingTerms!));

            if (paymentTerms is not null)
                queryparameters.Add(new KeyValuePair<string, string>("payment_terms", paymentTerms!));

            if (partnerPointOfSale is not null)
                queryparameters.Add(new KeyValuePair<string, string>("partner_point_of_sale", partnerPointOfSale!));

            if (platformName is not null)
                queryparameters.Add(new KeyValuePair<string, string>("platform_name", platformName!));

            exclusion?.ForEach(item => queryparameters.Add(new KeyValuePair<string, string>("exclusion", item)));

            return $"v3/properties/availability{UriHelpers.GetQueryParameters(queryparameters)}";
        }

        public static string PriceCheck(string propertyId, string roomId, string rateId, string token)
            => $"v3/properties/{propertyId}/rooms/{roomId}/rates/{rateId}?token={token}";

        public static string GuestReviews(long propertyId, string language)
            => $"v3/properties/{propertyId}/guest-reviews?language={language}";

        public static string PropertyContentFile(string language, string supplySource)
            => $"v3/files/properties/content?language={language}&supply_source={supplySource}";
    }
}
