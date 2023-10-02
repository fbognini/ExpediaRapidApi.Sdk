namespace ExpediaRapidApi.Sdk.Utils
{
    internal class UriHelpers
    {
        public static string GetQueryParameters(List<KeyValuePair<string, string>> parameters)
        {
            if (!parameters.Any())
                return string.Empty;

            var parameterStrings = parameters.Select(param => param.Key + "=" + param.Value);
            
            return $"?{string.Join("&", parameterStrings)}";
        }
    }
}
