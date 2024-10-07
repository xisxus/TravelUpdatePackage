namespace TravelUpdate.Models
{
    public class UrlService
    {
        public int UrlServiceId { get; set; }
        public string CurrentUrl { get; set; }
        public int RequestUrlId { get; set; }
        public string? Description { get; set; }
        public RequestUrl RequestUrl { get; set; }
    }
}
