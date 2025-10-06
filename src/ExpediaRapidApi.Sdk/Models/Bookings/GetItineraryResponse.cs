using ExpediaRapidApi.Sdk.Models.Properties;
using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Models.Bookings
{
    public class ItineraryDetailResponse
    {
        [JsonPropertyName("itinerary_id")]
        public string ItineraryId { get; set; }

        [JsonPropertyName("property_id")]
        public string PropertyId { get; set; }

        [JsonPropertyName("links")]
        public ItineraryLinks Links { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public Phone Phone { get; set; }

        [JsonPropertyName("rooms")]
        public List<ItineraryRoom> Rooms { get; set; }

        [JsonPropertyName("billing_contact")]
        public BillingContact BillingContact { get; set; }

        [JsonPropertyName("adjustment")]
        public PriceItem Adjustment { get; set; }

        [JsonPropertyName("creation_date_time")]
        public string CreationDateTime { get; set; }

        [JsonPropertyName("affiliate_reference_id")]
        public string AffiliateReferenceId { get; set; }

        [JsonPropertyName("affiliate_metadata")]
        public string AffiliateMetadata { get; set; }

        [JsonPropertyName("conversations")]
        public Conversation Conversations { get; set; }


        public class Conversation
        {
            [JsonPropertyName("links")]
            public ConversationsLinks Links { get; set; }
        }

        public class ConversationsLinks
        {
            [JsonPropertyName("property")]
            public ItineraryLinks Property { get; set; }
        }

        public class ItineraryLinks
        {
            [JsonPropertyName("resume")]
            public Link? Resume { get; set; }

            [JsonPropertyName("cancel")]
            public Link? Cancel { get; set; }

            [JsonPropertyName("non_vat_expedia_invoice")]
            public Link? NotVatExpediaInvoice { get; set; }
        }


        public class ItineraryRoom
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("confirmation_id")]
            public ConfirmationId ConfirmationId { get; set; }

            [JsonPropertyName("bed_group_id")]
            public string BedGroupId { get; set; }

            [JsonPropertyName("checkin")]
            public string Checkin { get; set; }

            [JsonPropertyName("checkout")]
            public string Checkout { get; set; }

            [JsonPropertyName("number_of_adults")]
            public int NumberOfAdults { get; set; }

            [JsonPropertyName("child_ages")]
            public List<int> ChildAges { get; set; }

            [JsonPropertyName("given_name")]
            public string GivenName { get; set; }

            [JsonPropertyName("family_name")]
            public string FamilyName { get; set; }

            /// <summary>
            /// The booking status of the room.
            /// Allowed: pending┃booked┃canceled
            /// </summary>
            [JsonPropertyName("status")]
            public string Status { get; set; }

            [JsonPropertyName("special_request")]
            public string SpecialRequest { get; set; }

            [JsonPropertyName("smoking")]
            public bool Smoking { get; set; }

            [JsonPropertyName("rate")]
            public ItineraryRate Rate { get; set; }

            [JsonPropertyName("links")]
            public RoomLinks Links { get; set; }
        }

        public class ConfirmationId
        {
            [JsonPropertyName("expedia")]
            public string Expedia { get; set; }

            [JsonPropertyName("property")]
            public string Property { get; set; }
        }

        public class RoomLinks
        {
            [JsonPropertyName("cancel")]
            public Link Cancel { get; set; }

            [JsonPropertyName("change")]
            public Link Change { get; set; }
        }

        public class ItineraryRate
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("merchant_of_record")]
            public string MerchantOfRecord { get; set; }

            [JsonPropertyName("refundable")]
            public bool Refundable { get; set; }

            [JsonPropertyName("cancel_refund")]
            public CancelRefund CancelRefund { get; set; }

            [JsonPropertyName("amenities")]
            public List<string> Amenities { get; set; }

            [JsonPropertyName("promotions")]
            public Promotions Promotions { get; set; }

            [JsonPropertyName("cancel_penalties")]
            public List<CancelPenalty> CancelPenalties { get; set; }

            [JsonPropertyName("nonrefundable_date_ranges")]
            public List<NonRefundableDateRange>? NonRefundableDateRanges { get; set; }

            [JsonPropertyName("deposits")]
            public List<Deposit> Deposits { get; set; }


            [JsonPropertyName("pricing")]
            public Pricing? Pricing { get; set; }

            [JsonPropertyName("fees")]
            public List<Fee> Fees { get; set; }
        }

        public class Pricing
        {
            [JsonPropertyName("nightly")]
            public List<List<PriceItem>>? Nightly { get; set; }
            [JsonPropertyName("stay")]
            public List<PriceItem>? Stays { get; set; }
            [JsonPropertyName("totals")]
            public Totals? Totals { get; set; }
            [JsonPropertyName("fees")]
            public Fee? Fee { get; set; }
        }

        public class CancelPenalty
        {
            [JsonPropertyName("percent")]
            public string Percent { get; set; }

            [JsonPropertyName("start")]
            public string Start { get; set; }

            [JsonPropertyName("end")]
            public string End { get; set; }

            [JsonPropertyName("amount")]
            public string Amount { get; set; }

            [JsonPropertyName("currency")]
            public string Currency { get; set; }

            [JsonPropertyName("nights")]
            public string Nights { get; set; }
        }

        public class NonRefundableDateRange
        {
            [JsonPropertyName("start")]
            public string Start { get; set; }

            [JsonPropertyName("end")]
            public string End { get; set; }
        }

        public class CancelRefund
        {
            [JsonPropertyName("amount")]
            public string Amount { get; set; }

            [JsonPropertyName("currency")]
            public string Currency { get; set; }
        }

        public class Deposit
        {
            [JsonPropertyName("amount")]
            public string Amount { get; set; }

            [JsonPropertyName("value")]
            public string Value { get; set; }

            [JsonPropertyName("due")]
            public string Due { get; set; }
        }

        public class Promotions
        {
            [JsonPropertyName("value_adds")]
            public Dictionary<string, ValueAdd> ValueAdds { get; set; }
        }

        public class ValueAdd
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; }
        }

    }
}


public class BillingContact
{
    [JsonPropertyName("given_name")]
    public string GivenName { get; set; }

    [JsonPropertyName("family_name")]
    public string FamilyName { get; set; }

    [JsonPropertyName("address")]
    public Address Address { get; set; }
}

public class Address
{
    [JsonPropertyName("line_1")]
    public string? Line1 { get; set; }

    [JsonPropertyName("line_2")]
    public string? Line2 { get; set; }

    [JsonPropertyName("line_3")]
    public string? Line3 { get; set; }

    [JsonPropertyName("city")]
    public string? City { get; set; }

    [JsonPropertyName("state_province_code")]
    public string? StateProvinceCode { get; set; }

    [JsonPropertyName("postal_code")]
    public string? PostalCode { get; set; }

    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; } = string.Empty;
}

public class Phone
{
    [JsonPropertyName("country_code")]
    public string CountryCode { get; set; } = string.Empty;

    [JsonPropertyName("area_code")]
    public string? AreaCode { get; set; }

    [JsonPropertyName("number")]
    public string Number { get; set; } = string.Empty;
}