using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpediaRapidApi.Sdk.Lodging;

internal interface IHasCustomerHeaderOptions
{
    CustomerHeaderOptions? Customer { get; }
}

public class CustomerHeaderOptions
{
    public CustomerHeaderOptions(string customerIp)
    {
        CustomerIp = customerIp;
    }

    public string CustomerIp { get; set; }
    public string? UserAgent { get; set; }
    public string? CustomerSessionId { get; set; }
}
