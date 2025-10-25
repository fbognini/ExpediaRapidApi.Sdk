using ExpediaRapidApi.Sdk.Models.Properties;
using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Lodging.GetPropertyContent;

public class PropertyContent
{
    [JsonPropertyName("property_id")]
    public long PropertyId { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public Address Address { get; set; } = new();

    [JsonPropertyName("ratings")]
    public Ratings Ratings { get; set; }

    [JsonPropertyName("location")]
    public PropertyLocation Location { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("fax")]
    public string Fax { get; set; }

    [JsonPropertyName("category")]
    public Category Category { get; set; }

    [JsonPropertyName("rank")]
    public int Rank { get; set; }

    [JsonPropertyName("business_model")]
    public BusinessModel BusinessModel { get; set; }

    [JsonPropertyName("checkin")]
    public Checkin Checkin { get; set; }

    [JsonPropertyName("checkout")]
    public Checkout Checkout { get; set; }

    [JsonPropertyName("fees")]
    public Fees Fees { get; set; }

    [JsonPropertyName("policies")]
    public Policies Policies { get; set; }

    [JsonPropertyName("attributes")]
    public Attributes Attributes { get; set; }

    [JsonPropertyName("amenities")]
    public Dictionary<string, Amenity>? Amenities { get; set; }

    [JsonPropertyName("images")]
    public List<Image> Images { get; set; }

    [JsonPropertyName("onsite_payments")]
    public OnsitePayments OnsitePayments { get; set; }

    [JsonPropertyName("rooms")]
    public Dictionary<string, RoomContent>? Rooms { get; set; }

    [JsonPropertyName("rates")]
    public Dictionary<string, Rates> Rates { get; set; }

    [JsonPropertyName("dates")]
    public Dates Dates { get; set; }

    [JsonPropertyName("descriptions")]
    public PropertiesContentResponseDescriptions Descriptions { get; set; }

    [JsonPropertyName("statistics")]
    public Dictionary<string, Statistic> Statistics { get; set; }

    [JsonPropertyName("themes")]
    public Dictionary<string, Theme>? Themes { get; set; }

    [JsonPropertyName("tax_id")]
    public string TaxId { get; set; }

    [JsonPropertyName("spoken_languages")]
    public Dictionary<string, SpokenLanguages>? SpokenLanguages { get; set; }

    [JsonPropertyName("multi_unit")]
    public bool MultiUnit { get; set; }

    [JsonPropertyName("vacation_rental_details")]
    public VacationRentalDetails VacationRentalDetails { get; set; }

    [JsonPropertyName("brand")]
    public Brand Brand { get; set; }

    [JsonPropertyName("chain")]
    public Chain Chain { get; set; }

}

public class Address
{
    [JsonPropertyName("line_1")]
    public string Line1 { get; set; }

    [JsonPropertyName("line_2")]
    public string Line2 { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("state_province_code")]
    public string StateProvinceCode { get; set; }

    [JsonPropertyName("state_province_name")]
    public string StateProvinceName { get; set; }

    [JsonPropertyName("postal_code")]
    public string PostalCode { get; set; }

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; }

    [JsonPropertyName("obfuscated")]
    public bool Obfuscated { get; set; }

    [JsonPropertyName("localized")]
    public Localized Localized { get; set; }
}

public class Localized
{
    [JsonPropertyName("links")]
    public Dictionary<string, Link> Links { get; set; }
}

public class Preferred
{
    [JsonPropertyName("iata_airport_code")]
    public string IataAirportCode { get; set; }
}

public class RoomAmenity
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    //[JsonPropertyName("value", NullValueHandling = NullValueHandling.Ignore)]
    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class Attributes
{
    [JsonPropertyName("pets")]
    public Dictionary<string, Attribute>? Pets { get; set; }
    
    [JsonPropertyName("general")]
    public Dictionary<string, Attribute>? General { get; set; }
}

public class Attribute
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    //[JsonPropertyName("value", NullValueHandling = NullValueHandling.Ignore)]
    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class Category
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class Theme
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class View
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class Checkin
{
    [JsonPropertyName("24_hour")]
    public string The24_Hour { get; set; }

    [JsonPropertyName("begin_time")]
    public string BeginTime { get; set; }

    [JsonPropertyName("end_time")]
    public string EndTime { get; set; }

    [JsonPropertyName("instructions")]
    public string Instructions { get; set; }

    [JsonPropertyName("special_instructions")]
    public string SpecialInstructions { get; set; }

    [JsonPropertyName("min_age")]
    public int MinAge { get; set; }
}

public class Checkout
{
    [JsonPropertyName("time")]
    public string Time { get; set; }
}

public class Dates
{
    [JsonPropertyName("added")]
    public DateTime Added { get; set; }

    [JsonPropertyName("updated")]
    public DateTime Updated { get; set; }
}


public class PropertiesContentResponseDescriptions
{
    [JsonPropertyName("amenities")]
    public string Amenities { get; set; }

    [JsonPropertyName("dining")]
    public string Dining { get; set; }

    [JsonPropertyName("renovations")]
    public string Renovations { get; set; }

    [JsonPropertyName("national_ratings")]
    public string NationalRatings { get; set; }

    [JsonPropertyName("business_amenities")]
    public string BusinessAmenities { get; set; }

    [JsonPropertyName("rooms")]
    public string Rooms { get; set; }

    [JsonPropertyName("attractions")]
    public string Attractions { get; set; }

    [JsonPropertyName("location")]
    public string Location { get; set; }

    [JsonPropertyName("headline")]
    public string Headline { get; set; }
}

public class Fees
{
    [JsonPropertyName("mandatory")]
    public string Mandatory { get; set; }

    [JsonPropertyName("optional")]
    public string Optional { get; set; }
}

public class Image
{
    [JsonPropertyName("caption")]
    public string Caption { get; set; } = string.Empty;

    [JsonPropertyName("hero_image")]
    public bool HeroImage { get; set; }

    [JsonPropertyName("category")]
    public int Category { get; set; }

    [JsonPropertyName("links")]
    public Dictionary<string, Link> Links { get; set; } = new();
}

public class PropertyLocation
{
    [JsonPropertyName("coordinates")]
    public Coordinates Coordinates { get; set; }
}

public class OnsitePayments
{
    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("types")]
    public Dictionary<string, PaymentsTypes> PaymentsTypes { get; set; }
}

public class PaymentsTypes
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class Policies
{
    [JsonPropertyName("know_before_you_go")]
    public string KnowBeforeYouGo { get; set; }
}

public class Rates
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("amenities")]
    public Dictionary<string, RoomAmenity> Amenities { get; set; }
}
public class BusinessModel
{
    [JsonPropertyName("expedia_collect")]
    public bool ExpediaCollect { get; set; }

    [JsonPropertyName("property_collect")]
    public bool PropertyCollect { get; set; }
}

public class Ratings
{
    [JsonPropertyName("property")]
    public Property Property { get; set; }

    [JsonPropertyName("guest")]
    public Guest Guest { get; set; }
}

public class Guest
{
    [JsonPropertyName("count")]
    public int Count { get; set; }

    [JsonPropertyName("average")]
    public double Average { get; set; }

    [JsonPropertyName("cleanliness")]
    public string Cleanliness { get; set; }

    [JsonPropertyName("service")]
    public string Service { get; set; }

    [JsonPropertyName("comfort")]
    public string Comfort { get; set; }

    [JsonPropertyName("condition")]
    public string Condition { get; set; }

    [JsonPropertyName("location")]
    public string Location { get; set; }

    [JsonPropertyName("neighborhood")]
    public string Neighborhood { get; set; }

    [JsonPropertyName("quality")]
    public string quality { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }

    [JsonPropertyName("amenities")]
    public string Amenities { get; set; }

    [JsonPropertyName("recommendation_percent")]
    public string RecommendationPercent { get; set; }
}

public class Property
{
    [JsonPropertyName("rating")]
    public double Rating { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; }
}

public class RoomContent
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("descriptions")]
    public RoomDescriptions Descriptions { get; set; }

    [JsonPropertyName("amenities")]
    public Dictionary<string, RoomAmenity> Amenities { get; set; }

    [JsonPropertyName("images")]
    public List<Image> Images { get; set; }

    [JsonPropertyName("bed_groups")]
    public Dictionary<string, BedGroup> BedGroups { get; set; }

    [JsonPropertyName("area")]
    public Area Area { get; set; }

    [JsonPropertyName("views")]
    public Dictionary<string, View> Views { get; set; }

    [JsonPropertyName("occupancy")]
    public Occupancy Occupancy { get; set; }
}

public class Area
{
    [JsonPropertyName("square_meters")]
    public double SquareMeters { get; set; }

    //[JsonPropertyName("square_feet", NullValueHandling = NullValueHandling.Ignore)]
    [JsonPropertyName("square_feet")]
    public double SquareFeet { get; set; }
}

public class BedGroup
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("configuration")]
    public List<Configuration> Configuration { get; set; }
}

public class RoomDescriptions
{
    [JsonPropertyName("overview")]
    public string Overview { get; set; }
}

public class Occupancy
{
    [JsonPropertyName("max_allowed")]
    public MaxAllowed MaxAllowed { get; set; }

    [JsonPropertyName("age_categories")]
    public AgeCategories AgeCategories { get; set; }
}

public class AgeCategories
{
    //[JsonPropertyName("Adult", NullValueHandling = NullValueHandling.Ignore)]
    [JsonPropertyName("Adult")]
    public Adult Adult { get; set; }

    //[JsonPropertyName("ChildAgeA", NullValueHandling = NullValueHandling.Ignore)]
    [JsonPropertyName("ChildAgeA")]
    public Adult ChildAgeA { get; set; }
}

public class Adult
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("minimum_age")]
    public long MinimumAge { get; set; }
}

public class MaxAllowed
{
    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("children")]
    public int Children { get; set; }

    [JsonPropertyName("adults")]
    public int Adults { get; set; }
}

public class SpokenLanguages
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class Statistic
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class VacationRentalDetails
{
    [JsonPropertyName("registry_number")]
    public string RegistryNumber { get; set; }

    [JsonPropertyName("private_host")]
    public bool PrivateHost { get; set; }

    [JsonPropertyName("property_manager")]
    public PropertyManager PropertyManager { get; set; }

    [JsonPropertyName("rental_agreement")]
    public RentalAgreement RentalAgreement { get; set; }

    [JsonPropertyName("house_rules")]
    public List<string> HouseRules { get; set; }

    [JsonPropertyName("amenities")]
    public Dictionary<string, RoomAmenity> Amenities { get; set; }

    [JsonPropertyName("vrbo_srp_id")]
    public string VrboSrp { get; set; }

    [JsonPropertyName("listing_id")]
    public string ListingId { get; set; }

    [JsonPropertyName("listing_number")]
    public string ListingNumber { get; set; }

    [JsonPropertyName("listing_source")]
    public string ListingSource { get; set; }

    [JsonPropertyName("listing_unit")]
    public string ListingUnit { get; set; }
}

public class PropertyManager
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("links")]
    public PropertyManagerLinks Links { get; set; }
}

public class PropertyManagerLinks
{
    [JsonPropertyName("image")]
    public Link Image { get; set; }
}

public class RentalAgreement
{
    [JsonPropertyName("links")]
    public RentalAgreementLinks Links { get; set; }
}

public class RentalAgreementLinks
{
    [JsonPropertyName("rental_agreement")]
    public Link Link { get; set; }
}
public class Brand
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}

public class Chain
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}
