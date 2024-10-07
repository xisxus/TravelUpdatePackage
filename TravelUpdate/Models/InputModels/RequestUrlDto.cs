namespace TravelUpdate.Models.InputModels
{
    public class RequestUrlDto
    {
        public int RequestUrlId { get; set; }
        public string Url { get; set; }
        public string? UrlName { get; set; }
    }

    public class UrlServiceDto
    {
        public int UrlServiceId { get; set; }
        public string CurrentUrl { get; set; }
        public string? Description { get; set; }
        public RequestUrlDto RequestUrl { get; set; }
    }

    public class CreateUrlServiceDto
    {
        public string CurrentUrl { get; set; }
        public string? Description { get; set; }
        public int RequestUrlId { get; set; }
    }

    public class CreateRequestUrlDto
    {
        public string Url { get; set; }
        public string? UrlName { get; set; }
    }

}
