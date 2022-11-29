namespace AvaloniaTerminal.Models;

public class MenuResult
{
    public bool Status { get; set; }

    public string Pin { get; set; } = string.Empty;

    public string SecretPin { get; set; } = string.Empty;
}