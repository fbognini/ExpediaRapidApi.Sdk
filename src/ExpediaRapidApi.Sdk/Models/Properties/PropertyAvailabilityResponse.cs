﻿using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models.Properties
{
    public class PropertyAvailabilityResponse : List<PropertyAvailability>, IExpediaErrorModel
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("errors")]
        public List<ExpediaErrorModel>? Errors { get; set; }
        [JsonPropertyName("fields")]
        public List<ErrorField>? Fields { get; set; }
    }
}