using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaTerminal.ViewModels.Faculty
{
    public class ContactIPRViewModel : ViewModelBase, IRoutableViewModel
    {
        public string? UrlPathSegment => nameof(ContactIPRViewModel);

        public IScreen HostScreen { get; }

        #region Команды
        public ReactiveCommand<Unit, IRoutableViewModel> GetBack { get; }
        #endregion


        public ContactIPRViewModel(IScreen screen)
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
}
