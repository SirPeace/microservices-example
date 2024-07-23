using Mapster;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Database.Repositories;
using PlatformService.DTO;
using PlatformService.Models;

namespace PlatformService.Controllers;

[ApiController]
[Route("api/platforms")]
public class PlatformsController(PlatformRepository platformRepository) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPlatforms()
    {
        var platforms = await platformRepository.GetAll();

        return Ok(platforms.Adapt<List<PlatformReadDTO>>());
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetPlatform(int id)
    {
        var platform = await platformRepository.FindById(id);
        if (platform is null)
            return NotFound();

        return Ok(platform.Adapt<PlatformReadDTO>());
    }

    [HttpPost]
    public async Task<IActionResult> CreatePlatform(PlatformCreateDTO requestBody)
    {
        var platform = requestBody.Adapt<Platform>();
        platformRepository.Add(platform);
        await platformRepository.SaveChanges();

        return CreatedAtRoute(
            nameof(GetPlatform),
            new { platform.Id },
            platform.Adapt<PlatformReadDTO>()
        );
    }
}
