//using ExpediaRapidApi.Sdk.Models.Regions;
//using System;
//using System.Collections.Generic;
//using System.Formats.Asn1;
//using System.Linq;
//using System.Text;
//using System.Text.Json.Serialization;
//using System.Text.Json;
//using System.Threading.Tasks;
//using static System.Runtime.InteropServices.JavaScript.JSType;
//using System.Windows.Markup;
//using Polly;

//namespace ExpediaRapidApi.Sdk.Utils
//{
//    public class BoundingPolygonConverter : JsonConverter<BoundingPolygon>
//    {
//        public override bool CanConvert(Type objectType)
//        {
//            return objectType.Name.Equals("BoundingPolygon");
//        }

//        //public override BoundingPolygon Read(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
//        public override BoundingPolygon Read(ref Utf8JsonReader reader,
//            Type typeToConvert,
//            JsonSerializerOptions options)
//        {
//            if (reader.TokenType != JsonTokenType.StartObject)
//            {
//                throw new JsonException();
//            }

//            string type;
//            List<GeoInfo> coordinates;

//            var boundingPolygon = new BoundingPolygon()
//            {};

//            while (reader.Read())
//            {
//                if (reader.TokenType == JsonTokenType.EndObject)
//                {
//                    return boundingPolygon;
//                }

//                string propName = (reader.GetString() ?? string.Empty).ToLower();
//                reader.Read();

//                switch (propName)
//                {
//                    case var _ when propName.Equals(nameof(BoundingPolygon.Type).ToLower()):
//                        type = reader.GetString()!;
//                        break;
//                    case var _ when propName.Equals(nameof(BoundingPolygon.Coordinates).ToLower()):
//                        type = 
//                        break;
//                }
//            }
            
            
//            JObject obj = JObject.Load(reader);
//            BoundingPolygon boundingPolygon = new BoundingPolygon
//            {
//                Type = obj.Value<string>("type"),
//                Coordinates = new List<Borders>()
//            };
//            foreach (JToken coordinates in obj.Value<JArray>("coordinates").Children<JArray>())
//            {
//                foreach (JToken borders in coordinates.Children<JArray>())
//                {
//                    Borders brd = new Borders { geo_infos = new List<GeoInfo>() };
//                    if (borders.Children<JArray>().Count() > 0)
//                    {
//                        foreach (JToken geon_infos in borders.Children<JArray>())
//                        {
//                            var geoInfos = geon_infos.Values().ToList();
//                            brd.geo_infos.Add(new GeoInfo { lat = geoInfos[0].Value<string>(), lng = geoInfos[1].Value<string>() });
//                        }
//                    }
//                    else
//                    {
//                        var geoInfos = borders.Values().ToList();
//                        brd.geo_infos.Add(new GeoInfo { lat = geoInfos[0].Value<string>(), lng = geoInfos[1].Value<string>() });
//                    }
//                    boundingPolygon.Coordinates.Add(brd);
//                }
//            }
//            return boundingPolygon;
//        }

//        public override BoundingPolygon Write(JsonWriter writer, object value, JsonSerializer serializer)
//        {
//            //throw new NotImplementedException();
//            //var orderItem = value as OrderItem;
//            //JArray arra = new JArray();
//            //arra.Add(orderItem.itemId);
//            //arra.Add(orderItem.price);
//            //arra.Add(orderItem.quantity);
//            //arra.WriteTo(writer);
//        }
//    }
//}
