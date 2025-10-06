using System.Web;

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

        public static string EncodeEmailLocalPart(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
            {
                return email;
            }

            var atIndex = email.IndexOf('@');

            var localPart = email[..atIndex];
            if (HttpUtility.UrlDecode(localPart) == localPart)
            {
                // It's been already encoded
                return email;
            }

            var encodedLocalPart = HttpUtility.UrlEncode(localPart);

            return string.Concat(encodedLocalPart, email.Substring(atIndex));
        }
    }
}
