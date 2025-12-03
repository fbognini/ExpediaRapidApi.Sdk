using ExpediaRapidApi.Sdk.Lodging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpediaRapidApi.Sdk.Cars.Bookings.CancelCarBooking;

public class CancelCarBookingOptions : IHasCustomerHeaderOptions
{
    public required CustomerHeaderOptions Customer { get; set; }
}
