using System.Text.Json.Serialization;

namespace ChatGPT.Models.Chat
{
    public class Choice
    {
        [JsonPropertyName("index")]
        public int Index { get; set; }
        [JsonPropertyName("message")]
        public required Message Message { get; set; }
        [JsonPropertyName("finish_reason")]
        public required string FinishReason { get; set; }
    }

    public class Message
    {
        [JsonPropertyName("role")]
        public required string Role { get; set; }
        [JsonPropertyName("content")]
        public required string Content { get; set; }
    }

    public class Request
    {
        [JsonPropertyName("model")]
        public required string Model { get; set; }
        [JsonPropertyName("stream")]
        public bool Stream { get; set; }
        [JsonPropertyName("messages")]
        public required List<Message> Messages
        {
            get; set;
        }

        public static Request Create(string prompt, string assistantContent, bool stream, string model)
        {
            return new Request
            {
                Model = model,
                Stream = stream,
                Messages = new List<Message>
                {
                    new Message
                    {
                        Role = "system",
                        Content = assistantContent
                    },
                    new Message
                    {
                        Role = "user",
                        Content = prompt
                    }
                }
            };
        }
    }

    public class Response
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }
        [JsonPropertyName("object")]
        public required string Object { get; set; }
        [JsonPropertyName("created")]
        public double Created { get; set; }
        [JsonPropertyName("model")]
        public required string Model { get; set; }
        [JsonPropertyName("choices")]
        public required List<Choice> Choices { get; set; }
        [JsonPropertyName("usage")]
        public required Usage Usage { get; set; }
    }

    public class Usage
    {
        [JsonPropertyName("prompt_tokens")]
        public int PromptTokens { get; set; }
        [JsonPropertyName("completion_tokens")]
        public int CompletionTokens { get; set; }
        [JsonPropertyName("total_tokens")]
        public int TotalTokens { get; set; }
    }
}
