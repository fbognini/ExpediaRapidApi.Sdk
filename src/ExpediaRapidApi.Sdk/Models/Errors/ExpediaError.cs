using fbognini.Sdk.Exceptions;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models
{
    public class ExpediaError
    {
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("errors")]
        public List<ExpediaError> Errors { get; set; } = new();
        [JsonPropertyName("fields")]
        public List<ErrorField> Fields { get; set; } = new();

        public static bool TryParse(ApiException exception, out ExpediaError expediaError)
        {
            var _error = ConvertApiExceptionToErrorModel(exception);
            if (_error == null)
            {
                expediaError = new();
                return false;
            }

            expediaError = _error!;
            return true;
        }

        public static ExpediaError Parse(ApiException exception)
        {
            if (TryParse(exception, out ExpediaError error))
            {
                return error;
            }

            throw new FormatException();
        }

        private static ExpediaError? ConvertApiExceptionToErrorModel(ApiException exception)
        {
            ArgumentException.ThrowIfNullOrEmpty(exception.Response, nameof(exception));

            return JsonSerializer.Deserialize<ExpediaError>(exception.Response);
        }


    }
}
