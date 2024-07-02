using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExpediaRapidApi.Sdk.Models
{
    internal class References
    {
        [JsonPropertyName("categories")]
        public Dictionary<string, Reference> Categories { get; set; } = new();
    }
}
