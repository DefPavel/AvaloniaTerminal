using System;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace AvaloniaTerminal.ViewModels.Faculty;

public class IPRViewModel :  ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment => nameof(IPRViewModel);
    public IScreen HostScreen { get; }

    #region Команды
    public ReactiveCommand<Unit, IRoutableViewModel> GetBack { get; }

    #endregion
    
    public IPRViewModel(IScreen screen)
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