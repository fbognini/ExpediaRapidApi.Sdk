namespace ExpediaRapidApi.Sdk.Pay.RegisterPayment;

public class RegisterPaymentResponse
{
    public string PaymentToken { get; set; } = default!;
    public DateTime Expires { get; set; }
}