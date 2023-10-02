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

        [JsonPropertyName("checkin_instructions")]
        public string CheckinInstructions { get; set; }
        [JsonPropertyName("special_checkin_instructions")]
        public string SpecialCheckInInstructions { get; set; }
        [JsonPropertyName("is_european")]
        public bool IsEuropean { get; set; }
        [JsonPropertyName("totel_rate")]
        public double TotelRate { get; set; }
        [JsonPropertyName("discount_amount")]
        public double DiscountAmount { get; set; }
        [JsonPropertyName("booking_amount")]
        public double BookingAmount { get; set; }
        [JsonPropertyName("hotel_address")]
        public string HotelAddress { get; set; }
        [JsonPropertyName("hotel_city")]
        public string HotelCity { get; set; }
        [JsonPropertyName("hotel_country")]
        public string HotelCountry { get; set; }
        [JsonPropertyName("hotel_name")]
        public string HotelName { get; set; }
        [JsonPropertyName("hotel_image")]
        public string HotelImage { get; set; }
        [JsonPropertyName("room_description")]
        public string RoomDescription { get; set; }
        [JsonPropertyName("number_of_nights")]
        public int NumberOfNights { get; set; }
        [JsonPropertyName("book_details")]
        public BookDetails BookDetails { get; set; }
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("refundable")]
        public bool Refundable { get; set; }
        [JsonPropertyName("cancellationPolicy")]
        public List<CancelPenalty> CancellationPolicy { get; set; }
        [JsonPropertyName("cancellationPolicyNotRefundable")]
        public string CancellationPolicyNotRefundable { get; set; }


        [JsonPropertyName("links")]
        public ItineraryLinks Links { get; set; }

        [JsonPropertyName("rooms")]
        public List<ItineraryRoom> Rooms { get; set; }

        [JsonPropertyName("billing_contact")]
        public CustomerDetail CustomerDetails { get; set; }

        [JsonPropertyName("adjustment")]
        public Nightly Adjustment { get; set; }

        [JsonPropertyName("creation_date_time")]
        public string CreationDateTime { get; set; }

        [JsonPropertyName("affiliate_reference_id")]
        public string AffiliateReferenceId { get; set; }

        [JsonPropertyName("affiliate_metadata")]
        public string AffiliateMetadata { get; set; }

        [JsonPropertyName("conversations")]
        public Conversation Conversations { get; set; }



        public class CustomerDetail
        {
            [JsonPropertyName("given_name")]
            public string GivenName { get; set; }

            [JsonPropertyName("family_name")]
            public string FamilyName { get; set; }

            [JsonPropertyName("email")]
            public string Email { get; set; }

            [JsonPropertyName("phone")]
            public Phone Phone { get; set; }

            [JsonPropertyName("address")]
            public Address Address { get; set; }
        }

        public class Address
        {
            [JsonPropertyName("line_1")]
            public string Line1 { get; set; }

            [JsonPropertyName("line_2")]
            public string Line2 { get; set; }

            [JsonPropertyName("line_3")]
            public string Line3 { get; set; }

            [JsonPropertyName("city")]
            public string City { get; set; }

            [JsonPropertyName("state_province_code")]
            public string StateProvinceCode { get; set; }

            [JsonPropertyName("postal_code")]
            public string PostalCode { get; set; }

            [JsonPropertyName("country_code")]
            public string CountryCode { get; set; }
        }

        public class Phone
        {
            [JsonPropertyName("country_code")]
            public string CountryCode { get; set; }

            [JsonPropertyName("area_code")]
            public string AreaCode { get; set; }

            [JsonPropertyName("number")]
            public string Number { get; set; }
        }

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
            public Link Resume { get; set; }

            [JsonPropertyName("cancel")]
            public Link Cancel { get; set; }


            [JsonPropertyName("retrieve")]
            public Link Retrieve { get; set; }
            //[JsonPropertyName("expires")]
            //public string Expires { get; set; }
        }

        public class Link
        {
            [JsonPropertyName("method")]
            public string Method { get; set; }

            [JsonPropertyName("href")]
            public string Href { get; set; }
        }


        public class ItineraryRoom
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }

            [JsonPropertyName("bed_group_id")]
            public string BedGroupId { get; set; }

            [JsonPropertyName("confirmation_id")]
            public ConfirmationId ConfirmationId { get; set; }

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

            [JsonPropertyName("pricing")]
            public Pricing? Pricing { get; set; }

            [JsonPropertyName("cancel_penalties")]
            public List<CancelPenalty> CancelPenalties { get; set; }
            [JsonPropertyName("nonrefundable_date_ranges")]
            public List<NonRefundableDateRange>? NonRefundableDateRanges { get; set; }

            [JsonPropertyName("deposit_policies")]
            public List<DepositPolicy> DepositPolicies { get; set; }

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

        public class DepositPolicy
        {
            [JsonPropertyName("amount")]
            public string Amount { get; set; }

            [JsonPropertyName("due")]
            public string Due { get; set; }

            [JsonPropertyName("percent")]
            public string Percent { get; set; }

            [JsonPropertyName("remainder")]
            public bool Remainder { get; set; }

            [JsonPropertyName("currency")]
            public string Currency { get; set; }

            [JsonPropertyName("nights")]
            public string Nights { get; set; }
        }

        public class Fee
        {
            [JsonPropertyName("type")]
            public string Type { get; set; }

            [JsonPropertyName("value")]
            public string Value { get; set; }

            [JsonPropertyName("currency")]
            public string Currency { get; set; }

            [JsonPropertyName("scope")]
            public string Scope { get; set; }

            [JsonPropertyName("frequency")]
            public string Frequency { get; set; }
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

    public class BookDetails
    {


        public BookDetails()
        {
            CancellationPolicy = new List<CancelPenalty>();
            BookRooms = new List<BookRoom>();
            cancelPenalities = new List<string>();
        }

        [JsonPropertyName("cancellationPolicy")]
        public List<CancelPenalty> CancellationPolicy { get; set; }
        [JsonPropertyName("cancelPenalities")]
        public List<string> cancelPenalities { get; set; }
        [JsonPropertyName("cancellationPolicyNotRefundable")]
        public string CancellationPolicyNotRefundable { get; set; }
        [JsonPropertyName("taxAndServiceFee")]
        public double TaxAndServiceFee { get; set; }

        [JsonPropertyName("compensation")]
        public double Compensation { get; set; }
        [JsonPropertyName("propertyFee")]
        public double PropertyFee { get; set; }
        [JsonPropertyName("salesTax")]
        public double SalesTax { get; set; }
        [JsonPropertyName("recoveryChargesAndFees")]
        public double RecoveryChargesAndFees { get; set; }
        [JsonPropertyName("feeChargeCost")]
        public double FeeChargeCost { get; set; }

        [JsonPropertyName("totalRate")]
        public double TotalRate { get; set; }

        [JsonPropertyName("extraPersonFee")]
        public double ExtraPersonFee { get; set; }
        [JsonPropertyName("totalBookingCost")]
        public double TotalBookingCost { get; set; }

        [JsonPropertyName("bookRooms")]
        public List<BookRoom> BookRooms { get; set; }
        [JsonPropertyName("resortFeeCost")]
        public double ResortFeeCost { get; set; }
        [JsonPropertyName("mandatoryTaxCost")]
        public double MandatoryTaxCost { get; set; }
        [JsonPropertyName("mandatoryFeeCost")]
        public double MandatoryFeeCost { get; set; }
    }

    public class BookRoom
    {
        public BookRoom()
        {
            BookRoomNights = new List<BookRoomNight>();
        }

        [JsonPropertyName("bookRoomNights")]
        public List<BookRoomNight> BookRoomNights { get; set; }
    }

    public class BookRoomNight
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }
        [JsonPropertyName("baseRate")]
        public double BaseRate { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class Nightly
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("value")]
        public string Value { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
    }

    public class CancelPenalty
    {
        [JsonPropertyName("start")]
        public DateTimeOffset Start { get; set; }
        [JsonPropertyName("end")]
        public DateTimeOffset End { get; set; }
        [JsonPropertyName("nights")]
        public int Nights { get; set; }
        [JsonPropertyName("amount")]
        public double Amount { get; set; }
        [JsonPropertyName("currency")]
        public string Currency { get; set; }
        [JsonPropertyName("percent")]
        public string Percent { get; set; }
    }
}
