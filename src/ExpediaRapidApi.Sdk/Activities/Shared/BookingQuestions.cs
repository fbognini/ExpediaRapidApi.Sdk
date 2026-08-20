namespace ExpediaRapidApi.Sdk.Activities.Shared;

/// <summary>
/// A question this offer requires an answer to before it can be booked.
/// The price check declares them, the booking answers them.
/// </summary>
// This replaced the old required_booking_fields, and it is not a rename: the traveller no longer has typed fields at all.
// Passport, date of birth, height, pickup and the rest are gone from the API — whatever a supplier needs is now asked as a question, and answered as a free-form object whose shape is dictated by Type.
// The booking form has to be built from these at runtime; there is nothing left to hardcode.
public class BookingQuestion
{
    /// <summary>
    /// What an answer must quote to be tied back to this question.
    /// E.g. "height_1".
    /// </summary>
    public string Id { get; set; } = default!;

    /// <summary>
    /// The data type of the answer, which is what says how to render the input and how to shape Answer.
    /// E.g. "measurement", "selection", "text".
    /// The catalogue of types, with a JSON Schema for each, comes from the data types endpoint — see BookingQuestionDataType.
    /// </summary>
    public string Type { get; set; } = default!;

    /// <summary>
    /// The question as it is put to the traveller, already localized by Expedia in the language of the price check.
    /// Show it as it comes: there is nothing to translate on our side.
    /// </summary>
    public string Question { get; set; } = default!;

    /// <summary>
    /// Why the question is being asked.
    /// E.g. "Required for safety equipment fitting".
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Who has to answer: PerTraveler or PerBooking.
    /// It decides which of the two booking_question_answers arrays the answer goes into, so getting it wrong is a rejected booking.
    /// </summary>
    public string? AppliesTo { get; set; }

    /// <summary>
    /// Whether more than one of AllowedOptions may be picked.
    /// </summary>
    public bool AllowMultiple { get; set; }

    /// <summary>
    /// The tickets this question applies to.
    /// Empty means every ticket in the booking — so a question is not necessarily asked of every traveller even when it is PerTraveler.
    /// </summary>
    public List<string> TicketIds { get; set; } = [];

    /// <summary>
    /// The ids of the questions that hang off this one's answer.
    /// The dependency is declared from both ends: the parent lists its children here, and each child carries a Conditional of its own.
    /// </summary>
    public List<string> Children { get; set; } = [];

    /// <summary>
    /// When set, this question is only asked if the parent's answer satisfies the condition.
    /// Null for a question that is always asked.
    /// </summary>
    public BookingQuestionConditional? Conditional { get; set; }

    /// <summary>
    /// The permitted answers, when the question has a fixed set.
    /// For a "selection" these are the choices; for a "measurement" they are the units the value may be given in.
    /// </summary>
    public List<BookingQuestionOption> AllowedOptions { get; set; } = [];
}

/// <summary>
/// The values AppliesTo takes.
/// </summary>
// Constants and not an enum: Rapid declares the field as a bare string with no enum, so a value we have never seen must degrade to "unknown question, cannot answer it" rather than blow up deserialization of the whole price check.
public static class BookingQuestionAppliesTo
{
    /// <summary>
    /// Answered once per traveller, in the traveller's own booking_question_answers.
    /// </summary>
    public const string PerTraveler = "per_traveler";

    /// <summary>
    /// Answered once for the whole booking, in the request's top-level booking_question_answers.
    /// </summary>
    public const string PerBooking = "per_booking";
}

public class BookingQuestionOption
{
    /// <summary>
    /// What the answer must carry.
    /// E.g. "cm", "whatsapp".
    /// </summary>
    public string Id { get; set; } = default!;

    /// <summary>
    /// What the traveller is shown, localized.
    /// </summary>
    public string? Label { get; set; }
}

public class BookingQuestionConditional
{
    public string ParentQuestionId { get; set; } = default!;

    public BookingQuestionCondition? ShowWhen { get; set; }
}

/// <summary>
/// The test applied to the parent question's answer to decide whether the child is asked.
/// </summary>
public class BookingQuestionCondition
{
    /// <summary>
    /// How to compare.
    /// See BookingQuestionOperators.
    /// </summary>
    public string? Operator { get; set; }

    /// <summary>
    /// Which property of the parent's answer object to compare.
    /// Typically "selected".
    /// </summary>
    public string? Field { get; set; }

    /// <summary>
    /// The values that satisfy the condition.
    /// </summary>
    public List<string> Values { get; set; } = [];
}

/// <summary>
/// The comparisons Operator takes.
/// Strings, for the same reason as BookingQuestionAppliesTo.
/// </summary>
public static class BookingQuestionOperators
{
    // Named "Equal" and not "Equals": a const called Equals would hide object.Equals. The wire values are unaffected.
    public const string Equal = "equals";
    public const string NotEqual = "not_equals";
    public const string Contains = "contains";
    public const string NotContains = "not_contains";
}

/// <summary>
/// One answer to one booking question.
/// </summary>
public class BookingQuestionAnswer
{
    public string QuestionId { get; set; } = default!;

    /// <summary>
    /// The answer itself.
    /// Deliberately untyped: its shape is whatever the JSON Schema of the question's Type says it is — {"value":"72","unit":"in"} for a measurement, {"selected":["email"]} for a selection, and so on.
    /// Build it with BookingQuestionAnswers rather than by hand.
    /// </summary>
    public Dictionary<string, object?> Answer { get; set; } = [];
}

/// <summary>
/// The answer shapes that Rapid's own data type catalogue documents.
/// They are conveniences, not a closed set: a type we have never seen is still answerable by filling Answer directly from its SchemaDefinition.
/// </summary>
public static class BookingQuestionAnswers
{
    /// <summary>
    /// The property a single-valued answer carries.
    /// </summary>
    public const string ValueField = "value";

    /// <summary>
    /// The property a measurement's unit carries.
    /// </summary>
    public const string UnitField = "unit";

    /// <summary>
    /// The property a selection carries, and the one a child question's condition is usually tested against.
    /// </summary>
    public const string SelectedField = "selected";

    /// <summary>
    /// The property a phone's calling code carries, and an address's or a travel document's issuing country.
    /// </summary>
    public const string CountryCodeField = "country_code";

    /// <summary>
    /// The property a phone number carries, and a travel document's number.
    /// </summary>
    public const string NumberField = "number";

    public static BookingQuestionAnswer Value(string questionId, string value) => new()
    {
        QuestionId = questionId,
        Answer = new Dictionary<string, object?>() { [ValueField] = value },
    };

    /// <summary>
    /// A measurement.
    /// The value is a string on purpose: Rapid keeps it that way to preserve decimal precision.
    /// </summary>
    public static BookingQuestionAnswer Measurement(string questionId, string value, string unit) => new()
    {
        QuestionId = questionId,
        Answer = new Dictionary<string, object?>() { [ValueField] = value, [UnitField] = unit },
    };

    /// <summary>
    /// A selection.
    /// The single option still travels as a one-element array: the catalogue's schema declares "selected" an array of option ids even when only one may be picked.
    /// </summary>
    public static BookingQuestionAnswer Selected(string questionId, string optionId) => new()
    {
        QuestionId = questionId,
        Answer = new Dictionary<string, object?>() { [SelectedField] = new List<string>() { optionId } },
    };

    /// <summary>
    /// A selection that permitted more than one option.
    /// </summary>
    public static BookingQuestionAnswer Selected(string questionId, IEnumerable<string> optionIds) => new()
    {
        QuestionId = questionId,
        Answer = new Dictionary<string, object?>() { [SelectedField] = optionIds.ToList() },
    };

    /// <summary>
    /// A "boolean" answer.
    /// The value is a real JSON boolean and not the string "true", so it cannot go through Value.
    /// </summary>
    public static BookingQuestionAnswer Boolean(string questionId, bool value) => new()
    {
        QuestionId = questionId,
        Answer = new Dictionary<string, object?>() { [ValueField] = value },
    };

    /// <summary>
    /// An "integer" answer — an age or a headcount.
    /// A JSON number, unlike the string a measurement carries.
    /// </summary>
    public static BookingQuestionAnswer Integer(string questionId, int value) => new()
    {
        QuestionId = questionId,
        Answer = new Dictionary<string, object?>() { [ValueField] = value },
    };

    /// <summary>
    /// A "phone" answer: the number is split from its calling code, so a single "+39333..." string is not an answer.
    /// </summary>
    public static BookingQuestionAnswer Phone(string questionId, string countryCode, string number) => new()
    {
        QuestionId = questionId,
        Answer = new Dictionary<string, object?>() { [CountryCodeField] = countryCode, [NumberField] = number },
    };

    /// <summary>
    /// An "address" answer.
    /// Only line 1, city and the ISO 3166-1 alpha-2 country code are required.
    /// </summary>
    public static BookingQuestionAnswer Address(
        string questionId,
        string line1,
        string city,
        string countryCode,
        string? postalCode = null,
        string? stateProvince = null,
        string? line2 = null,
        string? line3 = null)
    {
        var answer = new Dictionary<string, object?>()
        {
            ["line_1"] = line1,
            ["city"] = city,
            [CountryCodeField] = countryCode,
        };

        AddIfPresent(answer, "line_2", line2);
        AddIfPresent(answer, "line_3", line3);
        AddIfPresent(answer, "postal_code", postalCode);
        AddIfPresent(answer, "state_province", stateProvince);

        return new BookingQuestionAnswer() { QuestionId = questionId, Answer = answer };
    }

    /// <summary>
    /// A "travel_document" answer.
    /// Number, issuing country and expiration date are required; the names and the issue date are only sent when the supplier asked for them.
    /// </summary>
    public static BookingQuestionAnswer TravelDocument(
        string questionId,
        string number,
        string countryCode,
        DateOnly expirationDate,
        string? givenName = null,
        string? familyName = null,
        DateOnly? issuedDate = null)
    {
        var answer = new Dictionary<string, object?>()
        {
            [NumberField] = number,
            [CountryCodeField] = countryCode,
            ["expiration_date"] = expirationDate.ToString("yyyy-MM-dd"),
        };

        AddIfPresent(answer, "given_name", givenName);
        AddIfPresent(answer, "family_name", familyName);
        AddIfPresent(answer, "issued_date", issuedDate?.ToString("yyyy-MM-dd"));

        return new BookingQuestionAnswer() { QuestionId = questionId, Answer = answer };
    }

    private static void AddIfPresent(Dictionary<string, object?> answer, string field, string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            answer[field] = value;
        }
    }
}

/// <summary>
/// The data type catalogue: every Type that exists, and the validation rules those types may carry.
/// </summary>
// The catalogue is a technical reference, not something the booking flow needs at runtime.
// Its use is to keep the set of types we claim to support — the supported_booking_data_types we send on content and availability — honest: a type that appears here and is not in that list is an activity we would show and then fail to book.
public class BookingQuestionDataTypeCatalog
{
    public List<BookingQuestionDataType> DataTypes { get; set; } = [];

    /// <summary>
    /// Every validation rule the catalogue defines, keyed by the name the types quote in ValidationRulesOptions.
    /// E.g. "max_length", "min_value".
    /// </summary>
    public Dictionary<string, BookingQuestionValidationRule> ValidationRules { get; set; } = [];
}

/// <summary>
/// One entry of the data type catalogue: what a given Type means and how an answer to it has to be structured.
/// </summary>
public class BookingQuestionDataType
{
    /// <summary>
    /// The identifier, as it appears in Type and in supported_booking_data_types.
    /// E.g. "measurement".
    /// </summary>
    public string Type { get; set; } = default!;

    /// <summary>
    /// The display name.
    /// E.g. "Measurement".
    /// </summary>
    public string? Name { get; set; }

    public string? Description { get; set; }

    /// <summary>
    /// When the type entered the catalogue.
    /// It is what date_updated_start filters on, so a delta call returns the types we have never seen and therefore never declared as supported.
    /// </summary>
    public DateOnly? DateAdded { get; set; }

    /// <summary>
    /// A JSON Schema fragment describing what Answer must look like for this type.
    /// Kept raw: it is a schema, not a model.
    /// </summary>
    public Dictionary<string, object?> SchemaDefinition { get; set; } = [];

    /// <summary>
    /// Which validation rules a question of this type may impose, keyed by the answer property they apply to — e.g. "value" → ["min_length", "max_length", "pattern"].
    /// The rules themselves are described in ValidationRules.
    /// A type with nothing to validate omits it.
    /// </summary>
    public Dictionary<string, List<string>> ValidationRulesOptions { get; set; } = [];
}

/// <summary>
/// One validation rule of the catalogue: what it constrains and what a value for it looks like.
/// </summary>
public class BookingQuestionValidationRule
{
    public string? Description { get; set; }

    /// <summary>
    /// The JSON type the rule's own value takes.
    /// E.g. "integer" for max_length, "string" for max_date.
    /// </summary>
    public string? Type { get; set; }
}
