using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Cars.Shared;

public class Rate
{
    [JsonPropertyName("merchant_of_record")]
    public string MerchantOfRecord { get; set; }

    [JsonPropertyName("sale_scenario")]
    public SaleScenario SaleScenario { get; set; }

    [JsonPropertyName("pricing")]
    public Pricing Pricing { get; set; }
}


public class SaleScenario
{
    [JsonPropertyName("package")]
    public bool Package { get; set; }

    [JsonPropertyName("member")]
    public bool Member { get; set; }

    [JsonPropertyName("mobile_promotion")]
    public bool MobilePromotion { get; set; }
}

public class Pricing
{
    [JsonPropertyName("daily_rate_strikethrough")]
    public Charge DailyRateStrikethrough { get; set; }

    /// <summary>
    /// Provides the daily rate of the car, excluding taxes and fees.
    /// </summary>
    [JsonPropertyName("daily_rate")]
    public Charge DailyRate { get; set; }

    /// <summary>
    /// Provides a list of mandatory taxes and fees that apply to the entire reservation (not divided per day).
    /// </summary>
    [JsonPropertyName("fees")]
    public List<Fee> Fees { get; set; }

    [JsonPropertyName("totals")]
    public PricingTotals Totals { get; set; }
}

public class PricingTotals
{
    /// <summary>
    /// Provides the non-discounted total price including taxes and fees. If present, this value should be displayed with a strikethrough to indicate it represents the original price.
    /// </summary>
    [JsonPropertyName("inclusive_strikethrough")]
    public Charge InclusiveStrikethrough { get; set; }

    /// <summary>
    /// Provides the total price including taxes and fees
    /// </summary>
    [JsonPropertyName("inclusive")]
    public Charge Inclusive { get; set; }

    /// <summary>
    /// Provides the total price excluding taxes and fees.
    /// </summary>
    [JsonPropertyName("exclusive")]
    public Charge Exclusive { get; set; }

    /// <summary>
    /// Provides the total price of taxes and fees.
    /// </summary>
    [JsonPropertyName("fees")]
    public Charge Fees { get; set; }

    /// <summary>
    /// Provides a total price of the requested extras (if applicable).
    /// </summary>
    [JsonPropertyName("extras")]
    public Charge Extras { get; set; }

    /// <summary>
    /// Provides the total price due at booking.
    /// </summary>
    [JsonPropertyName("due_at_booking")]
    public Charge DueAtBooking { get; set; }
}


public class Fee
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }
}