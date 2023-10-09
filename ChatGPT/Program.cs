using ChatGPT.Interfaces;
using ChatGPT.Models;
using ChatGPT.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net.Http.Headers;

AppSettings? settings = new ConfigurationBuilder()
    .AddCommandLine(args)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build()
    .Get<AppSettings>();

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostContext, config) => config.AddJsonFile("appsettings.json", optional: false))
    .ConfigureServices((hostContext, services) =>
    {
        services.AddOptions();
        services.Configure<Application>(options => hostContext.Configuration.GetSection("Application").Bind(options));
        services.AddTransient<CompletionService>();
        services.AddTransient<ChatService>();
        services.AddHttpClient(nameof(CompletionService), c =>
        {
            c.BaseAddress = new Uri(settings.Application.Uri);
            c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            c.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", settings.Application.ApiKey);
        });
    })
    .Build();

host
    .Services
    .GetService<ChatService>()
    ?.StartAsync();