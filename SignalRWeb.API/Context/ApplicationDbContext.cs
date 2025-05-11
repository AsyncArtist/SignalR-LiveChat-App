using Microsoft.EntityFrameworkCore;
using SignalRWeb.API.Models;

namespace SignalRWeb.API.Context;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users => Set<User>();
    public DbSet<Chat> Chats => Set<Chat>();
    
}

