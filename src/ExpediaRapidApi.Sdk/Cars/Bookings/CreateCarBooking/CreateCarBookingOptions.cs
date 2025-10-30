using ExpediaRapidApi.Sdk.Lodging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpediaRapidApi.Sdk.Cars.Bookings.CreateCarBooking;

public class CreateCarBookingOptions : IHasCustomerHeaderOptions
{
    public CustomerHeaderOptions? Customer { get; set; }
}
