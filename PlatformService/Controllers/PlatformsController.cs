using Mapster;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Communication.Http;
using PlatformService.Database.Repositories;
using PlatformService.DTO;
using PlatformService.Models;

namespace PlatformService.Controllers;

[ApiController]
[Route("api/platforms")]
public class PlatformsController(
    PlatformRepository platformRepository,
    ICommandDataClient commandClient
) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPlatforms()
    {
        var platforms = await platformRepository.GetAll();

        return Ok(platforms.Adapt<List<PlatformReadDto>>());
    }

    [HttpGet("{id:long}", Name = "GetPlatform")]
    public async Task<IActionResult> GetPlatform(long id)
    {
        var platform = await platformRepository.FindById(id);
        if (platform is null)
            return NotFound();

        return Ok(platform.Adapt<PlatformReadDto>());
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlatform(PlatformCreateDto requestBody)
    {
        var platform = requestBody.Adapt<Platform>();
        platformRepository.Add(platform);
        await platformRepository.SaveChanges();

        var platformRead = platform.Adapt<PlatformReadDto>();

        try
        {
            await commandClient.SendPlatformToCommand(platformRead);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"Error raised when sending platform to Command service: {ex.Message}\n{ex.StackTrace}"
            );
        }

        return CreatedAtRoute(
            routeName: "GetPlatform",
            routeValues: new { platform.Id },
            value: platformRead
        );
    }
}
