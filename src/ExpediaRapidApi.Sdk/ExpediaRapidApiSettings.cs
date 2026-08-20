namespace ExpediaRapidApi.Sdk
{
    public enum ExpediaRapidApiEnvironment
    {
        STG, PRD
    }

    internal class ExpediaRapidEnvironment
    {
        public static ExpediaRapidEnvironment STAGING = new(ExpediaRapidApiEnvironment.STG, "https://test.ean.com/");
        public static ExpediaRapidEnvironment PRODUCTION = new(ExpediaRapidApiEnvironment.PRD, "https://api.ean.com/");

        public ExpediaRapidApiEnvironment EnvironmentName { get; private set; }
        public string ApiUrl { get; private set; }

        private ExpediaRapidEnvironment(
            ExpediaRapidApiEnvironment environmentName,
            string apiUrl)
        {
            EnvironmentName = environmentName;
            ApiUrl = apiUrl;
        }

        /// <summary>
        /// Generates a configured Environment for use in a ExpediaRapid Gateway; targeted by Environment name.
        /// </summary>
        /// <param name="environment">The name of the target Environment (not case-sensitive)</param>
        /// <returns>A new configured instance of a ExpediaRapid Environment</returns>
        public static ExpediaRapidEnvironment ParseEnvironment(ExpediaRapidApiEnvironment environment)
        {
            return environment switch
            {
                ExpediaRapidApiEnvironment.STG => STAGING,
                ExpediaRapidApiEnvironment.PRD => PRODUCTION,
                _ => throw new ArgumentException("Unsupported environment: " + environment),
            };
        }
    }

    public class ExpediaRapidApiSettings
    {
        private const string DefaultAuthAddress = "https://api.ean.com/";

        private readonly Dictionary<ExpediaRapidApiEnvironment, string> RapidUrls = new()
        {
            [ExpediaRapidApiEnvironment.STG] = "https://test.ean.com/",
            [ExpediaRapidApiEnvironment.PRD] = "https://api.ean.com/",
        };

        private readonly Dictionary<ExpediaRapidApiEnvironment, string> RapidPayUrls = new()
        {
            [ExpediaRapidApiEnvironment.STG] = "https://pay-test.ean.com/",
            [ExpediaRapidApiEnvironment.PRD] = "https://pay.ean.com/",
        };

        public ExpediaRapidApiEnvironment EnvironmentName { get; set; }

        internal string BaseAddress => RapidUrls[EnvironmentName];
        internal string BasePayAddress => RapidPayUrls[EnvironmentName];

        /// <summary>
        /// Host of the OAuth2 token endpoint. Expedia issues a single token that carries both the test and the
        /// production scopes, so this does not follow <see cref="EnvironmentName"/> and defaults to the production
        /// host for every environment. Override it only if Expedia splits the identity service per environment.
        /// </summary>
        public string? AuthAddress { get; set; }

        internal string BaseAuthAddress => string.IsNullOrWhiteSpace(AuthAddress) ? DefaultAuthAddress : AuthAddress;

        /// <summary>
        /// Value applied to the Rapid <c>Test</c> header when a request does not set one of its own.
        /// </summary>
        // ⚠️ Leave it unset unless you are exercising an error path.
        // The header is what makes the test server answer with canned data: with standard the Activities price check returns the example printed in the manual — the manual's tickets, in the manual's currency, with the manual's payment token, which the Pay API then rejects.
        // Without it, staging prices the real activity and hands back a token the Pay API accepts.
        // It once had to be set to standard in staging, because the price check answered 500 without it.
        // That stopped being true: measured 2026-08-05.
        // Use it deliberately, per call, to reach sold_out, price_mismatch and the other scripted responses — not as an ambient setting.
        public string? TestHeader { get; set; }

        /// <summary>
        /// Value sent as the traffic_profile query parameter on every Activities call, for example Activities_GHC.
        /// It identifies the traffic against the commercial agreement and is what makes Rapid return the marketing fee.
        /// Rapid is making it mandatory: left unset, the parameter is simply not sent.
        /// </summary>
        public string? TrafficProfile { get; set; }

        public ApiKeyAuth ApiKey { get; set; } = default!;

        /// <summary>
        /// OAuth2 credentials used by the Cars and Pay APIs.
        /// </summary>
        public OAuth OAuth { get; set; } = default!;

        /// <summary>
        /// OAuth2 credentials used by the Activities API. Expedia issues a dedicated client for activities;
        /// when left unset, <see cref="OAuth"/> is used instead.
        /// </summary>
        public OAuth? ActivitiesOAuth { get; set; }

        internal OAuth ActivitiesOAuthOrDefault => ActivitiesOAuth ?? OAuth;
    }

    public class ApiKeyAuth
    {
        public required string ApiKey { get; set; }
        public required string ApiSecret { get; set; }
    }
    public class OAuth
    {
        public required string ClientId { get; set; }
        public required string ClientSecret { get; set; }
    }
}
