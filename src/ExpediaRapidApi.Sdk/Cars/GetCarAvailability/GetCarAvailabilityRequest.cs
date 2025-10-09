namespace ExpediaRapidApi.Sdk.Cars.GetCarAvailability;

public class GetCarAvailabilityRequest
{
    public string PickupTime { get; set; } = default!;

    public string DropoffTime { get; set; } = default!;

    public string? PickupArea { get; set; }

    public string? DropoffArea { get; set; }

    public long? PickupAirport { get; set; }

    public long? DropoffAirport { get; set; }

    public int DriverAge { get; set; }

    public string Currency { get; set; } = default!;

    public string Language { get; set; } = default!;

    public string CountryCode { get; set; } = default!;

    public int Limit { get; set; }

    public CarsSort Sort { get; set; }

    public CarsSalesChannel SalesChannel { get; set; }

    public CarsSalesEnvironment SalesEnvironment { get; set; }

    /// <summary>
    /// When shopping for package or cross-sell rates, you must specify the type of product(s) being sold with the car rental.
    /// </summary>
    public List<CarsPackagedProduct>? PackagedProduct { get; set; }

    public List<CarsFilter>? Filter { get; set; }

    public List<CarsRateOption>? RateOption { get; set; }
}


public enum CarsSort
{
    recommended,
    total_price,
    customer_rating,
    distance
}

public enum CarsSalesChannel
{
    website,
    agent_tool,
    mobile_app,
    mobile_web,
    meta,
    cache
}

public enum CarsSalesEnvironment
{
    car_package,
    car_only
}

public enum CarsRateOption
{
    /// <summary>
    /// Request member rates. This feature must be enabled and requires a user to be logged in to request these rates.
    /// </summary>
    member,
    /// <summary>
    /// Request cross sell rates when the traffic is coming from a cross sell booking. The traveler must have booked another service (hotel, flight, activities...) before the car rental.
    /// </summary>
    cross_sell
}

public enum CarsPackagedProduct
{
    hotel
}

public enum CarsFilter
{
    /// <summary>
    /// Only returns fully refundable rates. "Fully refundable rates" are rates that can be refunded for the full amount. This may only apply to certain time windows. Check cancel_penalties for detailed policy
    /// </summary>
    refundable,
    /// <summary>
    /// Only returns rates with payment collected by Expedia at the time of booking.
    /// </summary>
    expedia_collect,
    /// <summary>
    /// Only returns rates with payment collected by the vendor at the time of pickup.
    /// </summary>
    vendor_collect
}

