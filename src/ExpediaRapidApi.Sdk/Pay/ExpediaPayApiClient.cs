using ExpediaRapidApi.Sdk.Pay.RegisterPayment;
using Microsoft.Extensions.Options;

namespace ExpediaRapidApi.Sdk.Pay;

public interface IExpediaPayApiClient
{
    Task<RegisterPaymentResponse> RegisterPaymentAsync(string token, RegisterPaymentRequest request, RegisterPaymentOptions options, CancellationToken cancellationToken = default);
}

internal partial class ExpediaPayApiClient : ExpediaBaseApiClient, IExpediaPayApiClient
{
    public ExpediaPayApiClient(HttpClient httpClient, IOptions<ExpediaRapidApiSettings> settings, IExpediaCurrentUserService currentUserService) : base(httpClient, settings, currentUserService)
    {
        client.BaseAddress = new Uri(Settings.BasePayAddress);
    }
}