namespace ExpediaRapidApi.Sdk.Lodging.GetPropertyContent;

public class GetPropertyContentRequest
{
    public string Language { get; set; } = default!;
    public string SupplySource { get; set; } = default!;
    public List<string>? AmenityId { get; set; }
    public List<string>? AttributeId { get; set; }
    public List<string>? BrandId { get; set; }
    public List<BusinessModelFilters>? BusinessModel { get; set; }
    public List<string>? CategoryId { get; set; }
    public List<string>? CategoryIdExclude { get; set; }
    public List<string>? ChainId { get; set; }
    public List<string>? CountryCode { get; set; }
    public DateOnly? DateAddedEnd { get; set; }
    public DateOnly? DateAddedStart { get; set; }
    public DateOnly? DateUpdatedEnd { get; set; }
    public DateOnly? DateUpdatedStart { get; set; }
    public List<string>? Include { get; set; }
    public bool MultiUnit { get; set; }
    public List<string>? PropertyId { get; set; }
    public List<string>? SpokenLanguageId { get; set; }
    public string? BillingTerms { get; set; }
    public string? PartnerPointOfSale { get; set; }
    public string? PaymentTerms { get; set; }
    public string? PlatformName { get; set; }
}

public enum BusinessModelFilters
{
    expedia_collect,
    property_collect
}