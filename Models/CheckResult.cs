using System.Text.Json.Serialization;
using ReactiveUI;

namespace AvaloniaTerminal.Models;

public class CheckResult : ReactiveObject
{
    [JsonPropertyName("subscribe")]
    public bool Subscribe { get; set; }
}