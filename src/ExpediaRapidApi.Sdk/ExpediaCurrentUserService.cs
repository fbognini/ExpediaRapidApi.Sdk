using fbognini.Sdk.Interfaces;
using Microsoft.Extensions.Options;

namespace ExpediaRapidApi.Sdk;

/// <summary>
/// Access token provider for the Cars and Pay APIs.
/// </summary>
public interface IExpediaCurrentUserService : ISdkCurrentUserService
{
}

/// <summary>
/// Access token provider for the Activities API, which Expedia authorizes with a dedicated OAuth2 client.
/// It is a separate service (and therefore a separate token cache) from <see cref="IExpediaCurrentUserService"/>.
/// </summary>
public interface IExpediaActivitiesCurrentUserService : ISdkCurrentUserService
{
}

/// <summary>
/// Caches one access token per OAuth2 client, honouring its lifetime and letting a single caller renew it.
/// </summary>
public abstract class ExpediaCurrentUserServiceBase
{
    /// <summary>
    /// Renew this long before the token actually expires, so that a request in flight cannot outlive it.
    /// </summary>
    private static readonly TimeSpan ExpiryMargin = TimeSpan.FromMinutes(1);

    private readonly IExpediaAuthenticationApiService _authenticationApiService;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    private string? _accessToken;
    private DateTimeOffset _expiresAt = DateTimeOffset.MinValue;

    protected ExpediaCurrentUserServiceBase(IExpediaAuthenticationApiService authenticationApiService)
    {
        _authenticationApiService = authenticationApiService;
    }

    protected abstract OAuth OAuth { get; }

    public async Task<string> GetAccessToken()
    {
        if (TryGetValidToken(out var token))
        {
            return token;
        }

        await _semaphore.WaitAsync();
        try
        {
            // Another caller may have renewed the token while we were queuing on the semaphore.
            if (TryGetValidToken(out token))
            {
                return token;
            }

            var response = await _authenticationApiService.Login(OAuth);

            _accessToken = response.AccessToken;
            _expiresAt = DateTimeOffset.UtcNow.AddSeconds(response.ExpiresIn) - ExpiryMargin;

            return _accessToken;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    /// <summary>
    /// Always true: the token is fetched on demand. Returning false here would make the very first request of the
    /// process go out without an Authorization header, take a 401, and only then be retried with a token.
    /// </summary>
    public async Task<bool> IsAuthenticated()
    {
        await GetAccessToken();
        return true;
    }

    public async Task<string> ReloadAccessToken()
    {
        await _semaphore.WaitAsync();
        try
        {
            _accessToken = null;
            _expiresAt = DateTimeOffset.MinValue;
        }
        finally
        {
            _semaphore.Release();
        }

        return await GetAccessToken();
    }

    private bool TryGetValidToken(out string token)
    {
        token = _accessToken!;
        return !string.IsNullOrWhiteSpace(_accessToken) && DateTimeOffset.UtcNow < _expiresAt;
    }
}

public class ExpediaCurrentUserService : ExpediaCurrentUserServiceBase, IExpediaCurrentUserService
{
    private readonly ExpediaRapidApiSettings _settings;

    public ExpediaCurrentUserService(IExpediaAuthenticationApiService authenticationApiService, IOptions<ExpediaRapidApiSettings> settings)
        : base(authenticationApiService)
    {
        _settings = settings.Value;
    }

    protected override OAuth OAuth => _settings.OAuth;
}

public class ExpediaActivitiesCurrentUserService : ExpediaCurrentUserServiceBase, IExpediaActivitiesCurrentUserService
{
    private readonly ExpediaRapidApiSettings _settings;

    public ExpediaActivitiesCurrentUserService(IExpediaAuthenticationApiService authenticationApiService, IOptions<ExpediaRapidApiSettings> settings)
        : base(authenticationApiService)
    {
        _settings = settings.Value;
    }

    protected override OAuth OAuth => _settings.ActivitiesOAuthOrDefault;
}
