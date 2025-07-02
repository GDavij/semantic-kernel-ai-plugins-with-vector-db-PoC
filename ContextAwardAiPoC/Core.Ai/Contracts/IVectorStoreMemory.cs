namespace Core.Ai.Contracts;

public interface IVectorStoreMemory
{
    Task<T[]?> AddAsync<T>(params T[] item)
        where T : class;
    
    Task<T> UpdateAsync<T>(T item)
        where T : class;

    Task<T> DeleteAsync<T>(T item)
        where T : class;

    // TODO: Add Algorithm for similarity search in future
    Task<IEnumerable<(T[] entities, float score)>> Search<T>(byte[] embeddingsQuery, int topK)
        where T : class;
    
    Task<IEnumerable<(T[] entities, float score)>> Search<T>(string textQuery, int topK)
        where T : class;
    
}