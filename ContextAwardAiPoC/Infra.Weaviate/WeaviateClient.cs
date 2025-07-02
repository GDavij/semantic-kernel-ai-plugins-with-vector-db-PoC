using System.Text.Json;
using Core.Ai.Contracts;
using Google.Protobuf;
using Infra.Auth;
using Microsoft.Extensions.Logging;
using Weaviate.Grpc.Protocol.V1;

namespace Infra.Weaviate;

public class WeaviateClient : IVectorStoreMemory
{
    private readonly global::Weaviate.Grpc.Protocol.V1.Weaviate.WeaviateClient _dbClient;
    private readonly ICurrentTenant _currentTenant;
    private readonly ILogger<WeaviateClient> _logger;
    
    public WeaviateClient(global::Weaviate.Grpc.Protocol.V1.Weaviate.WeaviateClient dbClient, ICurrentTenant currentTenant, ILogger<WeaviateClient> logger)
    {
        _dbClient = dbClient;
        _currentTenant = currentTenant;
        _logger = logger;
    }

    public async Task<T[]?> AddAsync<T>(params T[] itens)
        where T : class
    {
        var request = new BatchObjectsRequest()
        {
            Objects =
            {
                itens.Select(item => new BatchObject
                {
                    Collection = item.GetType().FullName,
                    VectorBytes = ByteString.CopyFrom(JsonSerializer.SerializeToUtf8Bytes(itens)),
                    Tenant = _currentTenant.TenantId,
                    Uuid = Guid.NewGuid().ToString("N"),
                })
            },
            ConsistencyLevel = ConsistencyLevel.All
        };

        _logger.LogWarning("Adding {Count} items to Weaviate: {items}", itens.Length, itens);
        try
        {
            var reply = await _dbClient.BatchObjectsAsync(request);
            if (reply.Errors.Any())
            {
                foreach (var error in reply.Errors)
                {
                    _logger.LogError("Weaviate Error: {Error}", error.Error);
                }

                return null;
            }

            return itens;
        }
        catch
        {
            return null;
        }
    }

    public Task<T> UpdateAsync<T>(T item)
        where T : class
    { 
        _dbClient.Bat
    }

    public Task<T> DeleteAsync<T>(T item)
        where T : class
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<(T[] entities, float score)>> Search<T>(byte[] embeddingsQuery, int topK)
        where T : class
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<(T[] entities, float score)>> Search<T>(string textQuery, int topK)
        where T : class
    {
        throw new NotImplementedException();
    }
}