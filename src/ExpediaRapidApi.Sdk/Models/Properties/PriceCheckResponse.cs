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
        public PriceCheckLink Links { get; set; } = new();

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
        public Link Book { get; set; } = new();
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
