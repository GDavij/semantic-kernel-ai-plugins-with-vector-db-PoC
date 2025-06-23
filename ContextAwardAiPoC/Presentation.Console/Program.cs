
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
using Microsoft.SemanticKernel.Connectors.Ollama;

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
    services.AddLogging(cfg => cfg.AddConsole().AddDebug().AddOpenTelemetry());
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
            "You are a helpful assistant. for each message you should send a letter to the post office depending on the message content and the person name.");

        Console.WriteLine("Enter your question (or type 'exit' to quit):");
        var userInput = "Send a Letter To MarkTwain and ask him to write a book about the Current American culture and the conflicts within democrats and republicans.";

        if (string.IsNullOrWhiteSpace(userInput) || userInput.Equals("exit", StringComparison.OrdinalIgnoreCase))
        {
            break;
        }

        chatHistory.AddMessage(AuthorRole.User, userInput);

#pragma warning disable SKEXP0070
        var result = await chat.GetChatMessageContentAsync(chatHistory, executionSettings: new OllamaPromptExecutionSettings()
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Required()
            }, kernel: kernel,
#pragma warning restore SKEXP0070
            cancellationToken: cancellationTokenSource.Token);

        Console.WriteLine($"AI Response: {result.Content}");
 
    }
    catch (Exception ex)
    {
        logger.LogError(ex, ex.Message);
        var result = await chat.GetChatMessageContentsAsync(chatHistory, kernel: kernel,
            cancellationToken: cancellationTokenSource.Token);
        Console.Write($"AI Response: {result}");
    }
}
