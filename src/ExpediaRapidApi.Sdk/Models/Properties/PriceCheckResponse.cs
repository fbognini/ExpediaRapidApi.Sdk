using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models.Properties
{
    public class PriceCheckResponse
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("occupancy_pricing")]
        public OccupancyPricing? OccupancyPricing { get; set; }

        [JsonPropertyName("links")]
        public required PriceCheckLink Links { get; set; }

        [JsonPropertyName("card_on_file_limit")]
        public CardOnFileLimit? CardOnFileLimit { get; set; }

        [JsonPropertyName("refundable_damage_deposit")]
        public RefundableDamageDeposit? RefundableDamageDeposit { get; set; }

        [JsonPropertyName("deposits")]
        public List<Deposit>? Deposits { get; set; }
    }

    public class PriceCheckLink
    {
        [JsonPropertyName("book")]
        public required Book Book { get; set; }
    }

    public class Book
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }

        [JsonPropertyName("href")]
        public required string Href { get; set; }
    }

    public class CardOnFileLimit
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class RefundableDamageDeposit
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class Deposit
    {
        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("due")]
        public string Due { get; set; }

        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }
}
