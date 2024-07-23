using PlatformService;
using PlatformService.Database.Seeders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddProjectDependencies();

var app = builder.Build();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await PlatformsSeeder.Seed(app);

app.Run();
