using ExpediaRapidApi.Sdk.Utils;
using System.Globalization;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace ExpediaRapidApi.Sdk.Handlers
{
    internal class ExpediaAuthorizationHttpMessageHandler : DelegatingHandler
    {
        public const string ApiKeyOptionName = "__ExpediaApiKey__";
        public const string ApiSecretOptionName = "__ExpediaApiSecret__";

        public ExpediaAuthorizationHttpMessageHandler()
        {
        }

        /// <inheritdoc/>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Options.TryGetValue(new HttpRequestOptionsKey<string>(ApiKeyOptionName), out var apiKey) &&
                request.Options.TryGetValue(new HttpRequestOptionsKey<string>(ApiSecretOptionName), out var apiSecret))
            {
                string authorization = GenerateAuthorizationHeader(request, apiKey, apiSecret);
                request.Headers.Add("Authorization", authorization);
            }

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        private static string GenerateAuthorizationHeader(HttpRequestMessage _, string apiKey, string apiSecret)
        {
            var utc = DateTime.UtcNow;
            var unixTimestamp = ExpediaHelpers.GetUnixTimestamp(utc);
            var authHeaderValue = ExpediaHelpers.GetAuthorizationHeaderValue(apiKey, apiSecret, unixTimestamp);
            return authHeaderValue;
        }     
    }
}
