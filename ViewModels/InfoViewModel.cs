using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;

namespace AvaloniaTerminal.ViewModels;

public sealed class InfoViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment => nameof(InfoViewModel);
    public IScreen HostScreen { get; }

    #region Команды
    public ReactiveCommand<Unit, IRoutableViewModel> GetBack { get; }

    #endregion
    public InfoViewModel(IScreen screen)
    {
        HostScreen = screen;

        GetBack = ReactiveCommand.CreateFromTask(async _ =>
            await HostScreen.Router.NavigateAndReset.Execute(new MenuViewModel(HostScreen)));
        
        this.WhenActivated((CompositeDisposable disposables) =>
        {
            GC.Collect();
        });
    }
}