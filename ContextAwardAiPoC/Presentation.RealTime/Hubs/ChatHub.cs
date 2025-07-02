using Microsoft.AspNetCore.SignalR;

namespace Presentation.RealTime.Hubs;

public class ChatHub : Hub
{
    private readonly ILogger<ChatHub> _logger;
    private readonly IHubContext<ChatHub> _chatHub;

    public ChatHub(ILogger<ChatHub> logger, IHubContext<ChatHub> chatHub)
    {
        _logger = logger;
        _chatHub = chatHub;
    }

    public Task SendMessage(string message)
    {
        _chatHub.Clients.All.SendAsync("ReceiveMessage", message);
        return Task.CompletedTask;
    }
}