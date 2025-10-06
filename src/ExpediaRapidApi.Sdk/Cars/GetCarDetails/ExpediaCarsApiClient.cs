namespace ExpediaRapidApi.Sdk.Cars;

internal partial class ExpediaCarsApiClient
{
    public async Task<CarDetails> GetCarDetailsAsync(string carRentalId, string token, CancellationToken cancellationToken = default)
    {
        return await GetApiAsync<CarDetails>($"v2/cars/{carRentalId}/details?token={token}", cancellationToken: cancellationToken);
    }
}