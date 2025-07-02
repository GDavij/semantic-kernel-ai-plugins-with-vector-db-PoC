using Microsoft.Extensions.VectorData;

namespace TaskManager.Helper.Models;

public class Task
{
    [VectorStoreKey]
    public Guid Id { get; set; }
    
    [VectorStoreData(IsIndexed = true, IsFullTextIndexed = true)]
    public string Title { get; set; }
    
    [VectorStoreData(IsIndexed = true, IsFullTextIndexed = true)]
    public string Description { get; set; }
    
    [VectorStoreData(IsIndexed = true)]
    public bool Status { get; set; }
}