using Microsoft.AspNetCore.SignalR;

namespace ProjetoJan.Hubs;

public class ChatHub : Hub
{
    public override async Task OnConnectedAsync()
    { var user = Context.User?.Identity?.Name ?? "Anonymous";
        await base.OnConnectedAsync();
    }

    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
