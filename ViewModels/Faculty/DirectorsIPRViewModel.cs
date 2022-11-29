using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Threading;

namespace AvaloniaTerminal.ViewModels.Faculty
{
    public class DirectorsIPRViewModel : ViewModelBase, IRoutableViewModel
    {
        private readonly DispatcherTimer _disTimer = new();
        public string? UrlPathSegment => nameof(DirectorsIPRViewModel);
        public IScreen HostScreen { get; }

        #region Команды
        public ReactiveCommand<Unit, IRoutableViewModel> GetBack { get; }

        #endregion

        public DirectorsIPRViewModel(IScreen screen)
        {
            HostScreen = screen;

            GetBack = ReactiveCommand.CreateFromTask(async _ =>
                await HostScreen.Router.Navigate.Execute(new MenuViewModel(HostScreen)));

            this.WhenActivated((CompositeDisposable disposables) =>
            {
                _disTimer.Interval = TimeSpan.FromMinutes(2);
                _disTimer.Tick += DispatcherTimer_Tick;
                _disTimer.Start();
            
                Disposable.Create(() => { _disTimer.Stop(); }).DisposeWith(disposables);
                GC.Collect();
            });
        }
        
        private async void DispatcherTimer_Tick(object? sender, EventArgs e)
        {
            await HostScreen.Router.NavigateAndReset.Execute(new CarouselViewModel(HostScreen));
        }
    }

}
