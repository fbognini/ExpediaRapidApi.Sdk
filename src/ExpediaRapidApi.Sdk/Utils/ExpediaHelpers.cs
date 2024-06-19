using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpediaRapidApi.Sdk.Utils
{
    internal static class ExpediaHelpers
    {
        public static int GetUnixTimestamp(DateTime utc)
        {
            var unixTimestamp = (int)utc.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return unixTimestamp;
        }

        public static string GetSignature(string apiKey, string apiSecret, int unixTimestamp)
        {
            var toBeHashed = apiKey + apiSecret + unixTimestamp;
            var bytes = Encoding.UTF8.GetBytes(toBeHashed);

            var hashedInputBytes = System.Security.Cryptography.SHA512.HashData(bytes);
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
            {
                hashedInputStringBuilder.Append(b.ToString("X2"));
            }
            var signature = hashedInputStringBuilder.ToString();
            return signature;
        }

        public static string GetAuthorizationHeaderValue(string apiKey, string apiSecret, int unixTimestamp) => "EAN APIKey=" + apiKey + ",Signature=" + ExpediaHelpers.GetSignature(apiKey, apiSecret, unixTimestamp) + ",timestamp=" + unixTimestamp;

    }
}
