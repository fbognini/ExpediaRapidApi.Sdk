using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpediaRapidApi.Sdk.Lodging.Bookings.CreateLodgingBooking;

public class CreateLodgingBookingOptions: IHasCustomerHeaderOptions
{
    public CustomerHeaderOptions? Customer { get; set; }
}
