using ChatGPT.Models;
using ChatGPT.Common;
using Microsoft.Extensions.Options;
using ChatGPT.Models.Chat;

namespace ChatGPT.Services
{
    public class CompletionService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _model;
        private const int _timeout = 5;
        private const string _url = "/v1/chat/completions";

        public CompletionService(IOptions<Application> options, IHttpClientFactory clientFactory)
        {
            _model = options.Value.Model;
            _clientFactory = clientFactory;
        }

        public async Task<string> GenerateText(string prompt, string assistantContent)
        {
            try
            {
                HttpClient httpClient = _clientFactory.CreateClient(nameof(CompletionService));

                httpClient.Timeout = TimeSpan.FromMinutes(_timeout);

                StringContent request = Request
                    .Create(prompt, assistantContent, false, _model)
                    .Serialize()
                    .ToJsonStringContent();

                HttpResponseMessage httpResponse = await httpClient.PostAsync(_url, request);

                httpResponse.EnsureSuccessStatusCode();

                return await httpResponse.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex) {
                throw new Exception($"Error: {ex.StatusCode}");
            }
        }

        public async Task<Stream> StreamText(string prompt, string assistantContent)
        {
            try
            {
                HttpClient httpClient = _clientFactory.CreateClient(nameof(CompletionService));

                httpClient.Timeout = TimeSpan.FromMinutes(_timeout);

                StringContent stringContent = Request
                    .Create(prompt, assistantContent, true, _model)
                    .Serialize()
                    .ToJsonStringContent();

                HttpResponseMessage httpResponse = await httpClient.PostAsync(_url, stringContent);

                httpResponse.EnsureSuccessStatusCode();

                return await httpResponse.Content.ReadAsStreamAsync();
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Error: {ex.StatusCode}");
            }
        }
    }
}
