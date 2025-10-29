using ExpediaRapidApi.Sdk.Cars;
using ExpediaRapidApi.Sdk.Lodging;
using ExpediaRapidApi.Sdk.Pay;
using fbognini.Sdk;
using fbognini.Sdk.Models;
using fbognini.Sdk.Utils;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk;


public class ExpediaRapidApiClient(IExpediaLodgingApiClient lodging, IExpediaCarsApiClient cars, IExpediaPayApiClient pay)
{
    public IExpediaLodgingApiClient Lodging { get; } = lodging;
    public IExpediaCarsApiClient Cars { get; } = cars;
    public IExpediaPayApiClient Pay { get; } = pay;
}


public class ExpediaBaseApiClient : BaseApiService
{
    public static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        Converters = { new JsonStringEnumConverter() }
    };

    public QueryStringBuilderFromJsonOptions? QueryStringBuilderFromJsonOptions { get; }
    public ExpediaRapidApiSettings Settings { get; }

    public ExpediaBaseApiClient(HttpClient client, IOptions<ExpediaRapidApiSettings> settings, IExpediaCurrentUserService? currentUserService)
        : base(client, currentUserService, JsonSerializerOptions)
    {
        Settings = settings.Value;
        QueryStringBuilderFromJsonOptions = new QueryStringBuilderFromJsonOptions()
        {
            UseIndexForArrays = false,
            JsonSerializerOptions = JsonSerializerOptions
        };

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.AcceptEncoding.Clear();
        client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        client.DefaultRequestHeaders.UserAgent.ParseAdd("GHC/2019");
    }

    protected static RequestOptions GetRequestOptions(object? options)
    {
        var _options = new RequestOptions();
        if (options is not null && options is IHasCustomerHeaderOptions customerOptions && customerOptions.Customer is not null)
        {
            _options.Headers.Add("Customer-Ip", customerOptions.Customer.CustomerIp);
            _options.Headers.UserAgent.Clear();
            _options.Headers.UserAgent.ParseAdd(customerOptions.Customer.UserAgent);
            _options.Headers.Add("Customer-Session-Id", customerOptions.Customer.CustomerSessionId);
        }

        return _options;
    }
}