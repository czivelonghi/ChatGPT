using ChatGPT.Common;
using ChatGPT.Interfaces;
using ChatGPT.Models.Chat;
using Microsoft.Extensions.DependencyInjection;

namespace ChatGPT.Services
{
    public class ChatService : IChatService
    {
        private readonly IServiceProvider _services;

        public ChatService(IServiceProvider services)
        {
            _services = services;
        }

        public async Task StartAsync()
        {
            using var scope = _services.CreateScope();

            CompletionService completionService = scope.ServiceProvider.GetRequiredService<CompletionService>();

            ChatConsole.WriteWelcome();

            while (true)
            {
                string assistantContent = ChatConsole.ReadAssistantContent();

                ChatConsole.WritePrompt();

                if (ChatConsole.TryReadInput(out string input) == true)
                {
                    ChatConsole.WriteBusy();

                    try
                    {
                        string json = await completionService.GenerateText(input, assistantContent);

                        Response? response = json.Deserialize<Response>();

                        string text = response?.Choices[0].Message.Content ?? "";

                        ChatConsole.Write(text);
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
