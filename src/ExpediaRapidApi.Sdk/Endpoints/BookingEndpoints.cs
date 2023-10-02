namespace ExpediaRapidApi.Sdk.Endpoints
{
    public static class BookingEndpoints
    {
        public static string Create(string token)
        {
            return $"v3/itineraries?token={token}";
        }

        public static string Resume(string itineraryId, string token)
        {
            return $"v3/itineraries/{itineraryId}?token={token}";
        }

        public static string Cancel(string itineraryId, string token)
        {
            return $"v3/itineraries/{itineraryId}?token={token}";
        }

        public static string Get(string itineraryId, string? token, string? email)
        {
            string path = $"v3/itineraries/{itineraryId}";
            
            if (!string.IsNullOrEmpty(token))
                return $"{path}?token={token}";

            if (!string.IsNullOrEmpty(email))
                return $"{path}?email={email}";

            return path;
        }
    }
}
