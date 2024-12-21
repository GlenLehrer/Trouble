using Microsoft.AspNetCore.SignalR;

namespace ZGT.Trouble.API.Hubs
{
    public class UIHub : Hub
    {
        public async Task SendMessage(string groupName, string user, string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", user, message);
        }
        public async Task UpdateGameBoard(string groupName)
        {
            await Clients.Group(groupName).SendAsync("UpdateGameBoard");
        }
        public async Task GroupJoined(string groupName)
        {
            await Clients.Group(groupName).SendAsync("GroupJoined");
        }
        public Task JoinGroup(string groupName)
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public Task LeaveGroup(string groupName)
        {
            return Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
    }
}

