using System.Text.Json.Serialization;

namespace ChatGPT.Models.ChatStream 
{ 
    public class ChatStream
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }
        [JsonPropertyName("object")]
        public required string Object { get; set; }
        [JsonPropertyName("created")]
        public int created { get; set; }
        [JsonPropertyName("model")]
        public required string Model { get; set; }
        [JsonPropertyName("choices")]
        public required List<Choice> Choices { get; set; }
    }

    public class Choice
    {
        [JsonPropertyName("index")]
        public int Index { get; set; }
        [JsonPropertyName("delta")]
        public required Delta Delta { get; set; }
        [JsonPropertyName("finish_reason")]
        public string? FinishReason { get; set; }
    }

    public class Delta
    {
        [JsonPropertyName("role")]
        public string? Role { get; set; }
        [JsonPropertyName("content")]
        public string? Content { get; set; }
    }
}
