namespace ExpediaRapidApi.Sdk.Activities.Bookings;

/// <summary>
/// What every traveller on a booking carries.
/// </summary>
// ⚠️ There is deliberately almost nothing here.
// Rapid used to take typed traveller fields — passport, date of birth, height, weight, nationality, address, dietary preference, language, pickup and dropoff — and declare which of them an activity needed through required_booking_fields.
// All of that is gone.
// A traveller is now a ticket and a name; everything a supplier wants to know is asked as a BookingQuestion by the price check and answered here, generically, in BookingQuestionAnswers.
// So do not add typed fields back to this class, however tempting: Rapid would ignore them at best and reject the booking at worst.
public abstract class TravelerBase
{
    /// <summary>
    /// The ticket type this traveller is booked under: one of the ticket ids of the offer.
    /// </summary>
    public string TicketId { get; set; } = default!;

    /// <summary>
    /// The answers to the questions the price check declared <see cref="BookingQuestionAppliesTo.PerTraveler"/>, filtered to those whose <see cref="BookingQuestion.TicketIds"/> cover <see cref="TicketId"/>.
    /// Questions that apply <see cref="BookingQuestionAppliesTo.PerBooking"/> do not belong here — they go on
    /// <see cref="CreateActivityBooking.CreateActivityBookingRequest.BookingQuestionAnswers"/>.
    /// </summary>
    public List<BookingQuestionAnswer> BookingQuestionAnswers { get; set; } = [];
}

public class PrimaryTraveler : TravelerBase
{
    public TravelerName Name { get; set; } = default!;

    /// <summary>
    /// Required. The primary traveller is the only one Rapid takes a phone number for.
    /// </summary>
    public Phone Phone { get; set; } = default!;
}

public class AdditionalTraveler : TravelerBase
{
    public TravelerName Name { get; set; } = default!;
}

public class TravelerName
{
    public string GivenName { get; set; } = default!;

    public string? MiddleName { get; set; }

    public string FamilyName { get; set; } = default!;
}
