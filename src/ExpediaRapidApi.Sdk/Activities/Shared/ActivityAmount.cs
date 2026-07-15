namespace ExpediaRapidApi.Sdk.Activities.Shared;

/// <summary>
/// A monetary amount. The value is a string so that the decimal precision Expedia sends is preserved verbatim.
/// </summary>
public class ActivityAmount
{
    /// <summary>
    /// The amount, with the correct decimal precision. E.g. "300.00".
    /// </summary>
    public string Value { get; set; } = default!;

    /// <summary>
    /// Currency of the amount, in ISO 4217 format.
    /// </summary>
    public string Currency { get; set; } = default!;

    /// <summary>
    /// Parses <see cref="Value"/> with invariant culture, which is how Expedia formats it.
    /// </summary>
    public decimal ToDecimal() => decimal.Parse(Value, System.Globalization.CultureInfo.InvariantCulture);
}

/// <summary>
/// A tax or fee, on top of the base price.
/// </summary>
public class FeeAmount : ActivityAmount
{
    /// <summary>
    /// What the fee or tax is. E.g. "Admission Fees", "booking_fee_vat".
    /// </summary>
    public string Type { get; set; } = default!;
}

/// <summary>
/// Taxes and fees, split by when the traveller pays them.
/// </summary>
public class BookingTaxesAndFees
{
    /// <summary>
    /// Collected at the time of booking: part of what we charge the customer.
    /// </summary>
    public List<FeeAmount> PayAtBooking { get; set; } = [];

    /// <summary>
    /// Paid on site, directly to the supplier: NOT part of what we charge the customer.
    /// </summary>
    public List<FeeAmount> PayAtLocation { get; set; } = [];
}
