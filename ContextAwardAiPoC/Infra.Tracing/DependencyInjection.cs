using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Infra.OpenTelemetry;

public static class DependencyInjection
{
    private const string ServiceName = "context-aware-ai-poc";
    private static TracerProvider TracerProvider;

    private static MeterProvider MeterProvider;
    
    public static ILoggingBuilder ConfigureTracingWithOpenTelemetryProvider(
        this ILoggingBuilder loggingBuilder)
    {
        BuildTraceProvider();
        BuildMeterProvider();
        
        loggingBuilder.AddOpenTelemetry(cfg =>
        {
            cfg.AddConsoleExporter()
                .AddOtlpExporter();
        });

        return loggingBuilder;
    }

    private static void BuildTraceProvider()
    {
        TracerProvider = Sdk.CreateTracerProviderBuilder()
                            .AddSource(ServiceName)
                            .AddConsoleExporter()
                            .AddOtlpExporter()
                            .Build();
    }
    private static void BuildMeterProvider()
    {
        MeterProvider = Sdk.CreateMeterProviderBuilder()
                           .AddMeter(ServiceName)
                           .AddConsoleExporter()
                           .AddOtlpExporter()
                           .Build();
    }

    private static void DisposeTraceProvider(this IHost host)
    {
        MeterProvider.Dispose();
        TracerProvider.Dispose();
    }
}