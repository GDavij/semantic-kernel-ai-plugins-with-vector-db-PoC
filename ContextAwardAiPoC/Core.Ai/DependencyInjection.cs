using Microsoft.Extensions.DependencyInjection;
using Microsoft.SemanticKernel;

namespace Core.Ai;

public static class DependencyInjection
{
    public static IServiceCollection AddCoreAi(this IServiceCollection services)
    {
        return services;
    }
}