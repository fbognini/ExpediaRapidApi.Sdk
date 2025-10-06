using ExpediaRapidApi.Sdk.Cars;
using ExpediaRapidApi.Sdk.Lodging;
using fbognini.Sdk;
using System.Net.Http.Headers;

namespace ExpediaRapidApi.Sdk;


public class ExpediaRapidApiClient
{
    public IExpediaLodgingApiClient Hotels { get; }
    public IExpediaCarsApiClient Cars { get; }

    public ExpediaRapidApiClient(IExpediaLodgingApiClient hotels, IExpediaCarsApiClient cars)
    {
        Hotels = hotels;
        Cars = cars;
    }
}


public class ExpediaBaseApiClient : BaseApiService
{
    public ExpediaRapidApiSettings Settings { get; }

    public ExpediaBaseApiClient(HttpClient client, ExpediaRapidApiSettings settings)
        : base(client)
    {
        Settings = settings;

        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        client.DefaultRequestHeaders.AcceptEncoding.Clear();
        client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        client.DefaultRequestHeaders.UserAgent.ParseAdd("GHC/2019");
        client.BaseAddress = new Uri(settings.BaseAddress);
    }
}