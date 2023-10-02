//using ExpediaRapidApi.Sdk.Utils;
//using System.Text.Json.Serialization;

//namespace ExpediaRapidApi.Sdk.Models.Regions
//{
//    [JsonConverter(typeof(BoundingPolygonConverter))]
//    public class BoundingPolygon
//    {
//        [JsonPropertyName("type")]
//        public required string Type { get; set; }

//        [JsonPropertyName("coordinates")]
//        public List<GeoInfo> Coordinates { get; set; }
//    }

//    public class GeoInfo
//    {
//        public required string Lat { get; set; }
//        public required string Lng { get; set; }
//    }
//}
