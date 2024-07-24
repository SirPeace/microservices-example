using PlatformService.DTO;

namespace PlatformService.Communication.Http;

public interface ICommandDataClient
{
    public Task SendPlatformToCommand(PlatformReadDto dto);
}
