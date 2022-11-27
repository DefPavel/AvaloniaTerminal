using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;

namespace AvaloniaTerminal.ViewModels.Faculty;

public class ContactVizViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment => nameof(ContactIPRViewModel);

    public IScreen HostScreen { get; }

    #region Команды
    public ReactiveCommand<Unit, IRoutableViewModel> GetBack { get; }
    #endregion


    public ContactVizViewModel(IScreen screen)
    {
        HostScreen = screen;

        GetBack = ReactiveCommand.CreateFromTask(async _ =>
            await HostScreen.Router.Navigate.Execute(new MenuViewModel(HostScreen)));

        this.WhenActivated((CompositeDisposable disposables) =>
        {
            GC.Collect();
        });
    }
}