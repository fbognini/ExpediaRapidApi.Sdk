namespace ExpediaRapidApi.Sdk.Shared
{
    public class ExpediaPaginationResponse<T>
    {
        public string? NextPageLink { get; set; }

        /// <summary>
        /// Total number of results across every page, from the Pagination-Total-Results header.
        /// Not every paginated endpoint returns it.
        /// </summary>
        public int? TotalResults { get; set; }

        public required T Response { get; set; }
    }
}
