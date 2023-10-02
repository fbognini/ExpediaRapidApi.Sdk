using ExpediaRapidApi.Sdk.Models;
using fbognini.Sdk.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExpediaRapidApi.Sdk.Utils
{
    public static class ExpediaErrors
    {
        public static ExpediaErrorModel ConvertApiExceptionToErrorModel(ApiException exception)
        {
            ArgumentException.ThrowIfNullOrEmpty(exception.Response, nameof(exception));

            return JsonSerializer.Deserialize<ExpediaErrorModel>(exception.Response)!;
        }
    }
}
