namespace DeribitApiClient.Models
{
    public class DeribitConfigOptions
    {
        public const string Name = "DeribitApiClientConfig";
        public string BaseUrl { get; set; } = "";
        public string ClientId { get; set; } = "";
        public string ClientSecret { get; set; } = "";
        public List<string> SubscribeTo { get; set; } = new List<string>();
    }
}
