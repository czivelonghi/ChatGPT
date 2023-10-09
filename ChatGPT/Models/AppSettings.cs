namespace ChatGPT.Models
{
    public class AppSettings
    {
        public required Application Application { get; set; }
    }

    public class Application
    {
        public required string ApiKey { get; set; }
        public required string Uri { get; set; }
        public required string Model { get; set; }
    }
}
