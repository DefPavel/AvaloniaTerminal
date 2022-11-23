using System.Threading.Tasks;
using AvaloniaTerminal.Models;
using AvaloniaTerminal.Services.Api;

namespace AvaloniaTerminal.Services;

public class ChekingService : IChekingService
{
    public async Task<CheckResult> GetPin(string pin)
    {
        return await QueryService.JsonDeserializeWithToken<CheckResult>(token: "secret", $"/check-subscribe?pin={pin.Trim()}", "GET");
    }

}