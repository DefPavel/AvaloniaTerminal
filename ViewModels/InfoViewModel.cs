using ReactiveUI;

namespace AvaloniaTerminal.ViewModels;

public sealed class InfoViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }
}