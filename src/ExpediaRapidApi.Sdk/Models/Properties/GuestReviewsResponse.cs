using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models.Properties
{
    public class GuestReviewsResponse
    {
        [JsonPropertyName("verified")]
        public GuestVerifiedReviews Verified { get; set; }
    }

    public class GuestVerifiedReviews
    {
        [JsonPropertyName("recent")]
        public List<GuestReview> Recent { get; set; }
    }

    public class GuestReview
    {
        [JsonPropertyName("verification_source")]
        public string Source { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("date_submitted")]
        public DateTime DateSubmitted { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; }

        [JsonPropertyName("reviewer_name")]
        public string ReviewerName { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("trip_reason")]
        public TripReason TripReason { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [JsonPropertyName("travel_companion")]
        public TravelCompanion TravelCompanion { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }

    public enum TripReason
    {
        business,
        leisure,
        friends_and_family,
        business_and_leisure
    }
    public enum TravelCompanion
    {
        family,
        family_with_children,
        partner,
        self,
        friends,
        pet
    }
}
