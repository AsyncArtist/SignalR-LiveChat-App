using ChatAppServer.WebAPI.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalRWeb.API.Context;
using SignalRWeb.API.Dtos;
using SignalRWeb.API.Models;

namespace SignalRWeb.API.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class ChatsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly IHubContext<ChatHub> _hubContext;
    public ChatsController(ApplicationDbContext context, IHubContext<ChatHub> hubContext)
    {
        _hubContext = hubContext;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        List<User> users = await _context.Users.ToListAsync(cancellationToken);
        if (users is null)
        {
            return NotFound("Users not found");
        }
        return Ok(users);
    }

    [HttpGet]
    public async Task<IActionResult> GetChats(Guid userId, Guid toUserId, CancellationToken cancellationToken)
    {
        List<Chat> chats = 
                    await _context.Chats
                            .Where(p => 
                                p.UserId == userId && p.ToUserId == toUserId || 
                                p.ToUserId == userId && p.UserId == toUserId)
                            .OrderBy(p=> p.Date)
                            .ToListAsync(cancellationToken);

        return Ok(chats);
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage(SendMessageDto request, CancellationToken cancellationToken)
    {
        var chat = new Chat
        {
            UserId = request.UserId,
            ToUserId = request.ToUserId,
            Message = request.Message,
            Date = DateTime.UtcNow
        };
        await _context.Chats.AddAsync(chat, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        string connectionId = ChatHub.Users.First(i=> i.Value == request.ToUserId).Key;
        
        await _hubContext.Clients.Client(connectionId).SendAsync("Messages", chat);

        return Ok(chat);
    }

}