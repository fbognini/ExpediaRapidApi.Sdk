using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models.Properties
{
    public class Room
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("room_name")]
        public string RoomName { get; set; } = string.Empty;
        [JsonPropertyName("rates")]
        public List<Rate> Rates { get; set; } = new();
    }

    public class Rate
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;
        [JsonPropertyName("available_rooms")]
        public int AvailableRooms { get; set; }
        [JsonPropertyName("refundable")]
        public bool Refundable { get; set; }
        [JsonPropertyName("member_deal_available")]
        public bool MemberDealAvailable { get; set; }
        [JsonPropertyName("sale_scenario")]
        public SaleScenario SaleScenario { get; set; }
        [JsonPropertyName("merchant_of_record")]
        public string MerchantOfRecord { get; set; }
        [JsonPropertyName("fenced_deal")]
        public bool FencedDeal { get; set; }
        [JsonPropertyName("fenced_deal_available")]
        public bool FencedDealAvailable { get; set; }
        [JsonPropertyName("deposit_required")]
        public bool DepositRequired { get; set; }
        [JsonPropertyName("amenities")]
        public Dictionary<string, RoomAmenity>? Amenities { get; set; }
        [JsonPropertyName("links")]
        public RateLinks Links { get; set; }
        [JsonPropertyName("bed_groups")]
        public Dictionary<string, BedGroups> BedGroups { get; set; }
        [JsonPropertyName("cancel_penalties")]
        public List<CancelPenalty> CancelPenalties { get; set; }
        [JsonPropertyName("occupancy_pricing")]
        public Dictionary<string, OccupancyPricing> OccupancyPricing { get; set; }
        [JsonPropertyName("promotions")]
        public Promotions Promotions { get; set; }
    }

    public class SaleScenario
    {
        [JsonPropertyName("package")]
        public bool Package { get; set; }
        [JsonPropertyName("member")]
        public bool Member { get; set; }
        [JsonPropertyName("corporate")]
        public bool Corporate { get; set; }
        [JsonPropertyName("distribution")]
        public bool Distribution { get; set; }
    }

    public class RateLinks
    {
        [JsonPropertyName("payment_options")]
        public PaymentOptions PaymentOptions { get; set; }
    }

    public class BedGroupsLinks
    {
        [JsonPropertyName("price_check")]
        public Link PriceCheck { get; set; }
    }

    public class PaymentOptions
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }
        [JsonPropertyName("href")]
        public string href { get; set; }
    }

    public class Amenity
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class BedGroups
    {
        [JsonPropertyName("links")]
        public BedGroupsLinks Link { get; set; }
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("configuration")]
        public List<Configuration> Configuration { get; set; }
    }

    public class Configuration
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("size")]
        public string Size { get; set; }
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }

    public class CancelPenalty
    {
        [JsonPropertyName("start")]
        public DateTimeOffset Start { get; set; }
        [JsonPropertyName("end")]
        public DateTimeOffset End { get; set; }
        [JsonPropertyName("nights")]
        public string? Nights { get; set; }
        [JsonPropertyName("amount")]
        public double? Amount { get; set; }
        [JsonPropertyName("currency")]
        public string? Currency { get; set; }
        /// <summary>
        /// Percentage of total booking charged for as penalty.A thirty percent penalty would be returned as 30%
        /// </summary>
        [JsonPropertyName("percent")]
        public string? Percent { get; set; }
    }

    public class OccupancyPricing
    {
        [JsonPropertyName("nightly")]
        public List<List<PriceItem>> Nightly { get; set; }
        [JsonPropertyName("stay")]
        public List<PriceItem> Stays { get; set; }
        [JsonPropertyName("totals")]
        public Totals Totals { get; set; }
        [JsonPropertyName("fees")]
        public Fee Fee { get; set; }

    }

    public class PriceItem
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("value")]
        public double Value { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class Fee
    {
        [JsonPropertyName("mandatory_fee")]
        public Tax MandatoryFee { get; set; }
        [JsonPropertyName("resort_fee")]
        public Tax ResortFee { get; set; }
        [JsonPropertyName("mandatory_tax")]
        public Tax MandatoryTax { get; set; }
    }

    public class Tax
    {
        [JsonPropertyName("billable_currency")]
        public Currency BillableCurrency { get; set; }
        [JsonPropertyName("request_currency")]
        public Currency RequestCurrency { get; set; }
    }

    public class Currency
    {
        [JsonPropertyName("value")]
        public double Value { get; set; }
        [JsonPropertyName("currency")]
        public string CurrencyCurrency { get; set; }
    }

    //public class Nightly
    //{
    //    [JsonPropertyName("type")]
    //    public string Type { get; set; }
    //    [JsonPropertyName("value")]
    //    public string Value { get; set; }
    //    [JsonPropertyName("currency")]
    //    public string Currency { get; set; }
    //}

    public class Totals
    {
        [JsonPropertyName("inclusive")]
        public Exclusive Inclusive { get; set; }
        [JsonPropertyName("strikethrough")]
        public Exclusive Strikethrough { get; set; }
        [JsonPropertyName("exclusive")]
        public Exclusive Exclusive { get; set; }
        [JsonPropertyName("marketing_fee")]
        public Exclusive MarketingFee { get; set; }
        [JsonPropertyName("gross_profit")]
        public Exclusive GrossProfit { get; set; }
        [JsonPropertyName("minimum_selling_price")]
        public Exclusive MinimumSellingPrice { get; set; }
    }

    public class Exclusive
    {
        [JsonPropertyName("billable_currency")]
        public Currency BillableCurrency { get; set; }
        [JsonPropertyName("request_currency")]
        public Currency RequestCurrency { get; set; }
    }

    public class Promotions
    {
        [JsonPropertyName("value_adds")]
        public Dictionary<string, ValueAdd> ValueAdd { get; set; }
        [JsonPropertyName("deal")]
        public Deal Deal { get; set; }
    }

    public class Deal
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
    public class ValueAdd
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }
}
