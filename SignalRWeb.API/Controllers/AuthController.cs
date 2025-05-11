using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalRWeb.API.Context;
using SignalRWeb.API.Dtos;
using SignalRWeb.API.Models;

namespace SignalRWeb.API.Controllers;
[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public AuthController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm]RegisterDto request, CancellationToken cancellationToken)
    {
        bool isExist = await _context.Users.AnyAsync(x => x.Name == request.Name, cancellationToken);
        if (isExist)
        {
            return BadRequest("User already exists");
        }

        string avatar = Path.Combine("wwwroot", "avatar", request.File.FileName);

        var user = new User
        {
            Name = request.Name,
            Avatar = request.File.FileName,
        };
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

       return Ok(user);
    }

    [HttpGet]
    public async Task<IActionResult> Login(string name, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
        if (user is null)
        {
            return NotFound("User not found");
        }
        
        user.Status = UserStatus.Online.ToString();

        await _context.SaveChangesAsync(cancellationToken);
        
        return Ok(user);
    }
}