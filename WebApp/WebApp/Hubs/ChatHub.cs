using Microsoft.AspNetCore.SignalR;

namespace WebApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task connect ( string username)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, username);
        }
    }
}
