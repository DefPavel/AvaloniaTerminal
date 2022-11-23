using System.Threading.Tasks;
using AvaloniaTerminal.Models;

namespace AvaloniaTerminal.Services;

public interface IChekingService
{
    Task<CheckResult> GetPin(string pin);
}