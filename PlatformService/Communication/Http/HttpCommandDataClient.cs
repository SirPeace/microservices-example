using System.Text;
using System.Text.Json;
using PlatformService.DTO;

namespace PlatformService.Communication.Http;

public class HttpCommandDataClient(HttpClient http, IConfiguration config) : ICommandDataClient
{
    /// <exception cref="NullReferenceException">Service is missing required configuration</exception>
    /// <exception cref="HttpRequestException">Request failed for any reason</exception>
    public async Task SendPlatformToCommand(PlatformReadDto dto)
    {
        var content = new StringContent(
            content: JsonSerializer.Serialize(dto),
            encoding: Encoding.UTF8,
            mediaType: "application/json"
        );

        string? commandServiceEndpoint = config["CommandServiceEndpoint"];
        if (commandServiceEndpoint is null)
            throw new NullReferenceException(
                "'CommandServiceEndpoint' option is missing in configuration!"
            );

        var response = await http.PostAsync($"{commandServiceEndpoint}/api/c/platforms", content);
        if (response.IsSuccessStatusCode)
            Console.WriteLine("--> Successfully sent platform to Command service");
        else
            Console.WriteLine("--> Could not send platform to Command service");
    }
}
