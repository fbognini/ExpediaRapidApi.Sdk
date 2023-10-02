namespace ExpediaRapidApi.Sdk.Models
{
    public class ExpediaPaginationResponse<T>
    {
        public string? NextPageLink { get; set; }

        public required T Response { get; set; }
    }
}
