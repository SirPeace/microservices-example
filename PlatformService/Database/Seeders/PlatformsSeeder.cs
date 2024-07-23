using Microsoft.EntityFrameworkCore;

namespace PlatformService.Database.Seeders;

public static class PlatformsSeeder
{
    public static async Task Seed(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
        if (dbContext is null)
            throw new NullReferenceException();

        await CreatePlatforms(dbContext);
    }

    private static async Task CreatePlatforms(AppDbContext dbContext)
    {
        if (await dbContext.Platforms.AnyAsync())
        {
            Console.WriteLine("--> Data already exists.");
            return;
        }

        dbContext.Platforms.AddRange(
            [
                new()
                {
                    Name = "PostgreSQL",
                    Cost = 0.00,
                    Publisher = "The PostgreSQL Global Development Group"
                },
                new()
                {
                    Name = ".NET",
                    Cost = 0.00,
                    Publisher = "Microsoft"
                },
                new()
                {
                    Name = "Kubernetes",
                    Cost = 0.00,
                    Publisher = "Cloud Native Computing Foundation"
                },
            ]
        );

        await dbContext.SaveChangesAsync();
    }
}
