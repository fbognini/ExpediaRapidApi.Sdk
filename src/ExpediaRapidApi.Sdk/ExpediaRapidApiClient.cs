using ExpediaRapidApi.Sdk.Cars;
using ExpediaRapidApi.Sdk.Lodging;
using fbognini.Sdk;
using fbognini.Sdk.Utils;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ExpediaRapidApi.Sdk;


public class ExpediaRapidApiClient
{
    public IExpediaLodgingApiClient Lodging { get; }
    public IExpediaCarsApiClient Cars { get; }

    public ExpediaRapidApiClient(IExpediaLodgingApiClient lodging, IExpediaCarsApiClient cars)
    {
        Lodging = lodging;
        Cars = cars;
    }
}


public class ExpediaBaseApiClient : BaseApiService
{
    public static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
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
        client.BaseAddress = new Uri(Settings.BaseAddress);
    }
}