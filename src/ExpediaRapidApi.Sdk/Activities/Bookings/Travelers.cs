namespace ExpediaRapidApi.Sdk.Activities.Bookings;

/// <summary>
/// The fields a traveller can be asked for. Which ones are actually mandatory is not fixed: it comes from the
/// required_booking_fields of the price check, activity by activity.
/// </summary>
public abstract class TravelerBase
{
    /// <summary>
    /// The ticket type this traveller is booked under: one of the ticket ids of the offer.
    /// </summary>
    public string TravelerType { get; set; } = default!;

    public DateOnly? DateOfBirth { get; set; }

    /// <summary>
    /// Nationality, in ISO 3166-1 alpha-2 format.
    /// </summary>
    public string? Nationality { get; set; }

    public ActivityAddress? Address { get; set; }

    public Measurement? Height { get; set; }

    public Measurement? Weight { get; set; }

    public PassportRequest? Passport { get; set; }

    public string? DietaryPreference { get; set; }
}

public class PrimaryTraveler : TravelerBase
{
    public TravelerName Name { get; set; } = default!;

    public Phone Phone { get; set; } = default!;

    public TransitRequest? Pickup { get; set; }

    public TransitRequest? Dropoff { get; set; }

    /// <summary>
    /// The language the activity is booked in: one of the languages the ticket supports.
    /// </summary>
    public string? Language { get; set; }
}

public class AdditionalTraveler : TravelerBase
{
    public TravelerName Name { get; set; } = default!;

    public Phone? Phone { get; set; }
}

public class TravelerName
{
    public string GivenName { get; set; } = default!;

    public string? MiddleName { get; set; }

    public string FamilyName { get; set; } = default!;
}

public class Measurement
{
    public string Value { get; set; } = default!;

    /// <summary>
    /// "cm" or "in" for a height, "kg" or "lb" for a weight.
    /// </summary>
    public string Unit { get; set; } = default!;
}

public class PassportRequest
{
    public string Number { get; set; } = default!;

    public DateOnly ExpirationDate { get; set; }

    /// <summary>
    /// Issuing country, in ISO 3166-1 alpha-2 format.
    /// </summary>
    public string IssuingCountry { get; set; } = default!;
}

/// <summary>
/// Where the traveller is picked up from, or dropped off to. Rapid models this as a oneOf: set exactly one of the
/// five properties, matching one of the permitted_options the price check returned for the pickup or dropoff field.
/// </summary>
public class TransitRequest
{
    public FlightTransit? Flight { get; set; }

    public RailTransit? Rail { get; set; }

    public BusTransit? Bus { get; set; }

    public ShipTransit? Ship { get; set; }

    public HotelTransit? Hotel { get; set; }
}

public abstract class TransitBase
{
    /// <summary>
    /// When the traveller is picked up or dropped off, in ISO 8601 format.
    /// </summary>
    public string DateTime { get; set; } = default!;

    public string Location { get; set; } = default!;
}

public class FlightTransit : TransitBase
{
    public string Airport { get; set; } = default!;

    public string Number { get; set; } = default!;
}

public class RailTransit : TransitBase
{
    public string Station { get; set; } = default!;

    public string Number { get; set; } = default!;
}

public class BusTransit : TransitBase
{
    public string Station { get; set; } = default!;

    public string Number { get; set; } = default!;
}

public class ShipTransit : TransitBase
{
    public string Port { get; set; } = default!;

    public string Number { get; set; } = default!;
}

public class HotelTransit
{
    public string Name { get; set; } = default!;
}
