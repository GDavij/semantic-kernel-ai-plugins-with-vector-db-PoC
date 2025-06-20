
using System.Threading;
using System.Threading.Tasks;
using Core.Ai;
using Infra.OpenTelemetry;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

var hostBuilder = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddEnvironmentVariables();
        config.AddUserSecrets<Program>();
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
        config.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: false);
        
    });

hostBuilder.ConfigureLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    loggingBuilder.ConfigureTracingWithOpenTelemetryProvider();
});

hostBuilder.ConfigureServices((context, services) =>
{
    var logger = services.BuildServiceProvider().GetRequiredService<ILogger<Program>>();
#pragma warning disable SKEXP0070
    services.AddSelfHostedCoreAi(context.Configuration, logger);
#pragma warning restore SKEXP0070
});

var host = hostBuilder.Build();
    
var cancellationTokenSource = new CancellationTokenSource();

var logger = host.Services.GetRequiredService<ILogger<Program>>();
var kernel = host.Services.GetRequiredService<Kernel>();
var chat = kernel.GetRequiredService<IChatCompletionService>();
ChatHistory chatHistory = new ChatHistory();
while (!cancellationTokenSource.IsCancellationRequested)
{
    try
    {

        chatHistory.AddMessage(AuthorRole.System,
            "You are a helpful assistant. you should answer the questions as best as you can. If you don't know the answer, just say that you don't know. Do not try to make up an answer.");

        Console.WriteLine("Enter your question (or type 'exit' to quit):");
        var userInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(userInput) || userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
        {
            break;
        }

        chatHistory.AddMessage(AuthorRole.User, userInput);

        var result = await chat.GetChatMessageContentsAsync(chatHistory, kernel: kernel,
            cancellationToken: cancellationTokenSource.Token);
        Console.Write($"AI Response: {result}");
 
    }
    catch (Exception ex)
    {
        logger.LogError(ex, ex.Message);
        var result = await chat.GetChatMessageContentsAsync(chatHistory, kernel: kernel,
            cancellationToken: cancellationTokenSource.Token);
        Console.Write($"AI Response: {result}");
    }
}
