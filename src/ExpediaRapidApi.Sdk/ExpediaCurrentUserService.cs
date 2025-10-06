using fbognini.Sdk.Interfaces;

namespace ExpediaRapidApi.Sdk;

public interface IExpediaCurrentUserService : ISdkCurrentUserService
{
}

public class ExpediaCurrentUserService : IExpediaCurrentUserService
{
    private readonly IExpediaAuthenticationApiService _expediaAuthenticationApiService;

    private string? _accessToken = null;

    public ExpediaCurrentUserService(IExpediaAuthenticationApiService ExpediaAuthenticationApiService)
    {
        _expediaAuthenticationApiService = ExpediaAuthenticationApiService;
    }

    public async Task<string> GetAccessToken()
    {
        if (string.IsNullOrWhiteSpace(_accessToken))
        {
            var response = await _expediaAuthenticationApiService.Login();
            _accessToken = response.AccessToken!;
        }

        return _accessToken;
    }

    public Task<bool> IsAuthenticated() => Task.FromResult(!string.IsNullOrEmpty(_accessToken));

    public async Task<string> ReloadAccessToken()
    {
        _accessToken = null;
        return await GetAccessToken();
    }
}
