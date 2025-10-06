namespace ExpediaRapidApi.Sdk.Lodging.Endpoints
{
    public static class RegionInclude
    {
        public const string STANDARD = "standard";
        public const string DETAILS = "details";
        public const string PROPERTY_IDS = "property_ids";
        public const string PROPERTY_IDS_EXPANDED = "property_ids_expanded";
    }

    public static class RegionEndpoints
    {
        public static string GetRegions(string language
            , string? ancestorId
            , string? iataLocationCode
            , List<string>? include)
        {
            string url = $"v3/regions?language={language}";

            if (!string.IsNullOrEmpty(ancestorId))
            {
                url += "&ancestor_id=" + ancestorId;
            }

            if (!string.IsNullOrEmpty(iataLocationCode))
            {
                url += "&iata_location_code=" + iataLocationCode;
            }

            if (include != null)
            {
                url += string.Join(string.Empty, include!.Select(x => "&include=" + x));
            }

            return url;
        }

        public static string GetRegionsByToken(string token)
        {
            return $"v3/regions?token={token}";
        }
    }
}
