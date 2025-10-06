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
    public required string CustomerIp { get; set; }
    public required string UserAgent { get; set; }
    public string? CustomerSessionId { get; set; }
}
