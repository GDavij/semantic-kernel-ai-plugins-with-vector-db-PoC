using System.ComponentModel;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;

namespace Core.Ai.Plugins;

public class PostOfficePlugin
{
    private readonly ILogger<PostOfficePlugin> _logger;

    public PostOfficePlugin(ILogger<PostOfficePlugin> logger)
    {
        _logger = logger;
    }

    [KernelFunction("send_message")]
    [Description(
        "Send a message to a person using their name. If the person does not exist, it will throw an Exception a new one.")]
    public async Task SendMessageTo(
        [Description("The name of the person to send a message to")]
        string personName,
        [Description("The content of the message")]
        string messsage)
    {
        _logger.LogWarning($"Message sent to {personName}: {messsage}");
        await Task.Delay(5000);
    }
}