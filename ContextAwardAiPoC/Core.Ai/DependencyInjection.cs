using System.Diagnostics.CodeAnalysis;
using Core.Ai.Constants;
using Core.Ai.Extensions;
using Core.Ai.Plugins;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using OllamaSharp;

namespace Core.Ai;

public static class DependencyInjection
{
    [Experimental("SKEXP0070")]
    public static IServiceCollection AddSelfHostedCoreAi<T>(this IServiceCollection services, IConfiguration configuration, ILogger<T> logger)
    {
        if (configuration.GetOrThrowValueForKey<bool>(OllamaRequirements.IsSelfHosted) is false)
        {
            logger.LogError("Self-hosted Ollama is not enabled in the configuration. Please set 'IsSelfHosted' to true if self hosted gonna be used.");
            throw new NotImplementedException("Only self-hosted Ollama is supported at this time.");
        }

        logger.LogInformation("Verifying Ollama configuration...");
        var modelId = configuration.GetOrThrowValueForKey<string>(OllamaRequirements.OllamaModelId);
        
        logger.LogInformation("Using Ollama model: {ModelId}", modelId);
        var endpoint = configuration.GetOrThrowValueForKey<string>(OllamaRequirements.ServiceEndpoint);
        
        logger.LogInformation("Using Ollama service endpoint: {Endpoint}", endpoint);

        services.AddOllamaChatCompletion(modelId, new Uri(endpoint));

        services.AddTransient(sp => new Kernel(sp));
        logger.LogInformation("Ollama kernel has been successfully configured and added to the service collection.");

        return services;
    }
}