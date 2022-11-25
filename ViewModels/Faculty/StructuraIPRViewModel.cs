using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace AvaloniaTerminal.ViewModels.Faculty
{
    public class StructuraIPRViewModel : ViewModelBase, IRoutableViewModel
    {
        public string? UrlPathSegment => nameof(StructuraIPRViewModel);

        public IScreen HostScreen { get; }

        #region Команды
        public ReactiveCommand<Unit, IRoutableViewModel> GetBack { get; }

        #endregion

        public StructuraIPRViewModel(IScreen screen)
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
}
