namespace ExpediaRapidApi.Sdk.Models
{
    public interface IExpediaErrorModel
    {
        public string? Type { get; set; }
        public string? Message { get; set; }
        public List<ExpediaErrorModel>? Errors { get; set; }
        public List<ErrorField>? Fields { get; set; }
    }
}
