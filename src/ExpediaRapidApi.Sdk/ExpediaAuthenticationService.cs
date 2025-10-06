using fbognini.Sdk;
using fbognini.Sdk.Models;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;

namespace ExpediaRapidApi.Sdk
{
    public class OAuth2Token
    {
        public string Scope { get; set; } = default!;
        public string AccessToken { get; set; } = default!;
        public string TokenType { get; set; } = default!;
        public long ExpiresIn { get; set; } = default!;
    }

    public interface IExpediaAuthenticationApiService
    {
        Task<OAuth2Token> Login();
    }

    public class ExpediaAuthenticationService : BaseApiService, IExpediaAuthenticationApiService
    {
        private readonly ExpediaRapidApiSettings _settings;

        public ExpediaAuthenticationService(HttpClient client, IOptions<ExpediaRapidApiSettings> settings)
            : base(client, options: ExpediaBaseApiClient.JsonSerializerOptions)
        {
            _settings = settings.Value;
        }

        public async Task<OAuth2Token> Login()
        {
            var content = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", _settings.OAuth.ClientId),
                new KeyValuePair<string, string>("client_secret", _settings.OAuth.ClientSecret),
            ]);

            return await PostApiAsync<OAuth2Token>("https://api.ean.com/identity/oauth2/v3/token", content);
        }
    }
}
