using ExpediaRapidApi.Sdk.Lodging;

namespace ExpediaRapidApi.Sdk.Pay.RegisterPayment;

public class RegisterPaymentOptions : IHasCustomerHeaderOptions
{
    public CustomerHeaderOptions? Customer { get; set; }
}
