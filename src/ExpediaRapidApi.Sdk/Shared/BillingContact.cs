namespace ExpediaRapidApi.Sdk.Shared;

public class BillingContact
{
    public string GivenName { get; set; } = default!;

    public string FamilyName { get; set; } = default!;

    public BillingContactAddress Address { get; set; } = default!;
}

public class BillingContactAddress
{
    public string Line1 { get; set; } = default!;

    public string? Line2 { get; set; }

    public string? Line3 { get; set; }

    public string City { get; set; } = default!;

    public string? StateProvinceCode { get; set; }

    public string PostalCode { get; set; } = default!;

    public string CountryCode { get; set; } = default!;
}

