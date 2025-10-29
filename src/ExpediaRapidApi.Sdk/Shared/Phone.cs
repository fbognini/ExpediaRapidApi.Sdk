using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpediaRapidApi.Sdk.Shared;

public class Phone
{
    public string CountryCode { get; set; } = string.Empty;
    public string? AreaCode { get; set; }
    public string Number { get; set; } = string.Empty;
}
