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
        /// <summary>
        /// Requests an access token for the Cars and Pay APIs.
        /// </summary>
        Task<OAuth2Token> Login();

        /// <summary>
        /// Requests an access token for the given OAuth2 client.
        /// </summary>
        Task<OAuth2Token> Login(OAuth oauth);
    }

    public class ExpediaAuthenticationService : BaseApiService, IExpediaAuthenticationApiService
    {
        private const string TokenEndpoint = "identity/oauth2/v3/token";

        private readonly ExpediaRapidApiSettings _settings;

        public ExpediaAuthenticationService(HttpClient client, IOptions<ExpediaRapidApiSettings> settings)
            : base(client, options: ExpediaBaseApiClient.JsonSerializerOptions)
        {
            _settings = settings.Value;
        }

        public Task<OAuth2Token> Login() => Login(_settings.OAuth);

        public async Task<OAuth2Token> Login(OAuth oauth)
        {
            var content = new FormUrlEncodedContent(
            [
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", oauth.ClientId),
                new KeyValuePair<string, string>("client_secret", oauth.ClientSecret),
            ]);

            return await PostApiAsync<OAuth2Token>(new Uri(new Uri(_settings.BaseAuthAddress), TokenEndpoint).ToString(), content);
        }
    }
}
