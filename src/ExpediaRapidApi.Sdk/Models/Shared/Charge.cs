using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models.Shared;

public class Charge
{
    [JsonPropertyName("billable_currency")]
    public CurrencyAmount BillableCurrency { get; set; }

    [JsonPropertyName("request_currency")]
    public CurrencyAmount RequestCurrency { get; set; }
}


public class CurrencyAmount
{
    [JsonPropertyName("value")]
    public double Value { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }
}