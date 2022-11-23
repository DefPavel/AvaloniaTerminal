using System;

namespace AvaloniaTerminal.Services;

public interface IApplicationInfo
{
    string Name { get; }
    string Organization { get; }
    string Version { get; }
    string FileVersion { get; }
    DateTime Created { get; }
}