using ExpediaRapidApi.Sdk.Pay.RegisterPayment;

namespace ExpediaRapidApi.Sdk.Pay;

internal partial class ExpediaPayApiClient
{

    public async Task<List<RegisterPaymentResponse>> RegisterPaymentAsync(RegisterPaymentRequest request, RegisterPaymentOptions options, CancellationToken cancellationToken = default)
    {
        var requestOptions = GetRequestOptions(options);
        requestOptions.Headers.Add("token", request.Token);
        return await PostApiAsync<List<RegisterPaymentResponse>, List<PaymentRegistrationRequest>>("v1/payments", request.Payments, requestOptions, cancellationToken: cancellationToken);
    }
}