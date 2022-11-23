using System;
using System.Reactive;
using System.Reactive.Disposables;
using ReactiveUI;

namespace AvaloniaTerminal.ViewModels;

public sealed class InfoViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment => nameof(InfoViewModel);
    public IScreen HostScreen { get; }

    #region Команды
    public ReactiveCommand<Unit, Unit> GetBack { get; }

    #endregion
    public InfoViewModel(IScreen screen)
    {
        HostScreen = screen;

        GetBack = HostScreen.Router.NavigateBack;
        this.WhenActivated((CompositeDisposable disposables) =>
        {
            GC.Collect();
        });
    }
}