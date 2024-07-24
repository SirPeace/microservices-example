using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Database.Repositories;

public class PlatformRepository(AppDbContext dbContext)
{
    public async Task<bool> SaveChanges() => await dbContext.SaveChangesAsync() > 0;

    public Task<List<Platform>> GetAll() => dbContext.Platforms.ToListAsync();

    public Task<Platform?> FindById(long id) =>
        dbContext.Platforms.FirstOrDefaultAsync(p => p.Id == id);

    public void Add(params Platform[] platforms)
    {
        foreach (var platform in platforms)
            dbContext.Platforms.Add(platform);
    }
}
