
using Microsoft.AspNetCore.SignalR;
using SignalRWeb.API.Context;
using SignalRWeb.API.Models;

namespace ChatAppServer.WebAPI.Hubs;

public sealed class ChatHub(ApplicationDbContext _context) : Hub
{
    public static Dictionary<string, Guid> Users = new();
    public async Task Connect(Guid userId)
    {
        Users.Add(Context.ConnectionId, userId);
        User? user = await _context.Users.FindAsync(userId);
        if(user is not null)
        {
            user.Status = UserStatus.Online.ToString();
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("Users", user);
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        Guid userId;
        Users.TryGetValue(Context.ConnectionId, out userId);
        Users.Remove(Context.ConnectionId);
        User? user = await _context.Users.FindAsync(userId);
        if (user is not null)
        {
            user.Status = UserStatus.Offline.ToString();
            await _context.SaveChangesAsync();

            await Clients.All.SendAsync("Users", user);
        }
    }
}