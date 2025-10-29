using ExpediaRapidApi.Sdk.Shared;

namespace ExpediaRapidApi.Sdk.Pay.RegisterPayment;

public class RegisterPaymentRequest
{
    public string Token { get; set; }
    public List<PaymentRegistrationRequest> Payments { get; set; } = [];
}


public enum PaymentType
{
    corporate_card,
    customer_card,
    virtual_card,
}


public class PaymentRegistrationRequest
{
    public PaymentType Type { get; set; }
    public string Number { get; set; } = default!;
    public string SecurityCode { get; set; } = default!;
    public string ExpirationMonth { get; set; } = default!;
    public string ExpirationYear { get; set; } = default!;
    public PaymentRegistrationRequestBillingContact BillingContact { get; set; } = default!;
}

public class PaymentRegistrationRequestBillingContact : BillingContact
{
    public string Email { get; set; } = default!;
    public Phone Phone { get; set; } = default!;
}
