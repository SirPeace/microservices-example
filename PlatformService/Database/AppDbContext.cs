using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Database;

public class AppDbContext : DbContext
{
    public DbSet<Platform> Platforms { get; init; }

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.UseInMemoryDatabase("db");
    }
}
