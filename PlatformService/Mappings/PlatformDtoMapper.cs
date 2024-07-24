using Mapster;
using PlatformService.DTO;
using PlatformService.Models;

namespace PlatformService.Mappings;

public class PlatformDtoMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Platform, PlatformReadDto>();
        config.NewConfig<PlatformCreateDto, Platform>();
    }
}
