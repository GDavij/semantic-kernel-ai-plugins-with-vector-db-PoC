using Microsoft.Extensions.Configuration;

namespace Core.Ai.Extensions;

public static class ConfigurationExtensions
{
    public static TValue GetOrThrowValueForKey<TValue>(this IConfiguration configuration, string key)
    {
        return configuration.GetValue<TValue>(key) ??
               throw new KeyNotFoundException($"The configuration key '{key}' was not found.");
    }
}