namespace ExpediaRapidApi.Sdk.Models.Shared
{
    public class ExpediaPaginationResponse<T>
    {
        public string? NextPageLink { get; set; }

        public required T Response { get; set; }
    }
}
