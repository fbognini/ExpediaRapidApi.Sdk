using ExpediaRapidApi.Sdk.Pay.RegisterPayment;

namespace ExpediaRapidApi.Sdk.Pay;

internal partial class ExpediaPayApiClient
{

    public async Task<RegisterPaymentResponse> RegisterPaymentAsync(string token, RegisterPaymentRequest request, RegisterPaymentOptions options, CancellationToken cancellationToken = default)
    {
        var requestOptions = GetRequestOptions(options);
        return await PostApiAsync<RegisterPaymentResponse, RegisterPaymentRequest>($"v2/payments?token={token}", request, requestOptions, cancellationToken: cancellationToken);
    }
}