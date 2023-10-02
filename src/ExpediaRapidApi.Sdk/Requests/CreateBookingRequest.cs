using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk.Requests
{
    public class CreateBookingRequest
    {
        [JsonPropertyName("affiliate_reference_id")]
        public string? AffiliateReferenceId { get; set; }

        [JsonPropertyName("hold")]
        public bool? Hold { get; set; }

        [JsonPropertyName("email")]
        public required string Email { get; set; }

        [JsonPropertyName("phone")]
        public required Phone Phone { get; set; }

        [JsonPropertyName("rooms")]
        public required List<CreateBookingRoomRequest> Rooms { get; set; }

        [JsonPropertyName("payments")]
        public List<CreateBookingPaymentRequest>? Payments { get; set; }

        [JsonPropertyName("affiliate_metadata")]
        public string? AffiliateMetadata { get; set; }

        [JsonPropertyName("tax_registration_number")]
        public string? TaxRegistrationNumber { get; set; }

        [JsonPropertyName("traveler_handling_instructions")]
        public string? TravelerHandlingInstructions { get; set; }
    }

    public class CreateBookingRoomRequest
    {
        [JsonPropertyName("given_name")]
        public required string GivenName { get; set; }

        [JsonPropertyName("family_name")]
        public required string FamilyName { get; set; }

        [JsonPropertyName("smoking")]
        public bool Smoking { get; set; }

        [JsonPropertyName("special_request")]
        public string? SpecialRequest { get; set; }

        [JsonPropertyName("loyalty_id")]
        public string? LoyaltyId { get; set; }
    }

    public class CreateBookingPaymentRequest
    {
        /// <summary>
        /// Allowed: corporate_card | customer_card | virtual_card | affiliate_collect
        /// </summary>
        [JsonPropertyName("type")]
        public required string Type { get; set; }

        [JsonPropertyName("number")]
        public string? Number { get; set; }

        [JsonPropertyName("security_code")]
        public string? SecurityCode { get; set; }

        [JsonPropertyName("expiration_month")]
        public string? ExpirationMonth { get; set; }

        [JsonPropertyName("expiration_year")]
        public string? ExpirationYear { get; set; }

        [JsonPropertyName("billing_contact")]
        public required BillingContact BillingContact { get; set; }

        [JsonPropertyName("third_party_authentication")]
        public ThirdPartyAuthRequest? ThirdPartyAuthRequest { get; set; }

        /// <summary>
        /// Date the payment account was enrolled in the cardholder's account with the merchant, in ISO 8601 format (YYYY-MM-DD)
        /// </summary>
        [JsonPropertyName("enrollment_date")]
        public string? EnrollmentDate { get; set; }
    }

    public class BillingContact
    {
        [JsonPropertyName("given_name")]
        public required string GivenName { get; set; }

        [JsonPropertyName("family_name")]
        public required string FamilyName { get; set; }

        [JsonPropertyName("address")]
        public required CreateBookingPaymentAddressRequest Address { get; set; }
    }
    public class ThirdPartyAuthRequest
    {
        [JsonPropertyName("cavv")]
        public required string Cavv { get; set; }

        [JsonPropertyName("eci")]
        public required string ECI { get; set; }

        [JsonPropertyName("three_ds_version")]
        public required string ThreeDsVersion { get; set; }

        [JsonPropertyName("ds_transaction_id")]
        public required string DsTransactionId { get; set; }

        [JsonPropertyName("pa_res_status")]
        public string? PaResStatus { get; set; }

        [JsonPropertyName("ve_res_status")]
        public string? VeResStatus { get; set; }

        [JsonPropertyName("xid")]
        public string? XId { get; set; }

        [JsonPropertyName("cavv_algorithm")]
        public string? CavvAlgorithm { get; set; }

        [JsonPropertyName("ucaf_indicator")]
        public string? UcafIndicator { get; set; }
    }

    public class CreateBookingPaymentAddressRequest
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
        public required string CountryCode { get; set; }
    }

    public class Phone
    {
        [JsonPropertyName("country_code")]
        public required string CountryCode { get; set; }

        [JsonPropertyName("area_code")]
        public string? AreaCode { get; set; }

        [JsonPropertyName("number")]
        public required string Number { get; set; }
    }
}
