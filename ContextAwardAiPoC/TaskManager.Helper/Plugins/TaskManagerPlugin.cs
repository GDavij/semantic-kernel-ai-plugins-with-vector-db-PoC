using System.ComponentModel;
using Core.Ai.Contracts;
using Microsoft.SemanticKernel;

namespace TaskManager.Helper.Plugins;

public class TaskManagerPlugin
{
    private readonly IVectorStoreMemory _vectorStore;

    public TaskManagerPlugin(IVectorStoreMemory vectorStore)
    {
        _vectorStore = vectorStore;
    }
    
    [KernelFunction("Add Task")]
    [Description("This function adds a new task to the memory.")]
    public async Task<Models.Task?> AddTaskAsync([Description("This is a task with an Id, Name, Description and Done Properties")] Models.Task task)
    {
        try
        {
            await _vectorStore.AddAsync(task);
            return task;
        }
        catch (Exception e)
        {
            return null;
        }
    }
}