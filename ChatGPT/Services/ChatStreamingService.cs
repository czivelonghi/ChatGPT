using ChatGPT.Common;
using ChatGPT.Interfaces;
using ChatGPT.Models.ChatStream;
using Microsoft.Extensions.DependencyInjection;

namespace ChatGPT.Services
{
    public class ChatStreamingService : IChatService
    {
        private readonly IServiceProvider _services;
        private const string _eos = "[DONE]";

        public ChatStreamingService(IServiceProvider services)
        {
            _services = services;
        }

        public async Task StartAsync()
        {
            using var scope = _services.CreateScope();

            CompletionService completionService = scope.ServiceProvider.GetRequiredService<CompletionService>();

            ChatConsole.WriteStreamWelcome();

            while (true)
            {
                string assistantContent = ChatConsole.ReadAssistantContent();

                ChatConsole.WritePrompt();

                if (ChatConsole.TryReadInput(out string input) == true)
                {
                    ChatConsole.WriteBusy();

                    Stream stream = await completionService.StreamText(input, assistantContent);

                    using StreamReader reader = new(stream);

                    while (!reader.EndOfStream)
                    {
                        string line = await reader.ReadLineAsync() ?? "";

                        if (string.IsNullOrEmpty(line) | line.Equals(_eos, StringComparison.OrdinalIgnoreCase)) continue;

                        try
                        {
                            line = line.Replace("data: ", "");

                            ChatStream? response = line.Deserialize<ChatStream>();

                            string content = response?.Choices[0].Delta.Content ?? "";

                            ChatConsole.Write(content);
                        }
                        catch (Exception ex)
                        {
                            ChatConsole.WriteLine(ex.Message);
                        }
                    }
                }
            }
        }
    }
}
