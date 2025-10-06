namespace ExpediaRapidApi.Sdk.Cars;

public interface IExpediaCarsApiClient
{
    Task<int> GetCarsAsync(string pickupLocation, DateTime pickupDate);
}

internal class ExpediaCarsApiClient : ExpediaBaseApiClient, IExpediaCarsApiClient
{
    public ExpediaCarsApiClient(HttpClient httpClient, ExpediaRapidApiSettings settings) : base(httpClient, settings) { }

    public async Task<int> GetCarsAsync(string pickupLocation, DateTime pickupDate)
    {
        var url = $"cars/search?pickupLocation={pickupLocation}&pickupDate={pickupDate:yyyy-MM-dd}";
        return 1;
    }
}
