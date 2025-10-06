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
        internal ExpediaRapidEnvironment Environment => ExpediaRapidEnvironment.ParseEnvironment(EnvironmentName);
        public ExpediaRapidApiEnvironment EnvironmentName { get; set; }

        internal string BaseAddress => Environment.ApiUrl;

        public ApiKeyAuth ApiKey { get; set; } = default!;
        public OAuth OAuth { get; set; } = default!;
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
