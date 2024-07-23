using Mapster;
using PlatformService.DTO;
using PlatformService.Models;

namespace PlatformService.Mappings;

public class PlatformDtoMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Platform, PlatformReadDTO>();
        config.NewConfig<PlatformCreateDTO, Platform>();
    }
}
