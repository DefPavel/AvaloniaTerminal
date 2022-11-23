using System.Reactive.Disposables;
using ReactiveUI;

namespace AvaloniaTerminal.ViewModels
{
    public sealed class MainWindowViewModel :  ReactiveObject, IActivatableViewModel, IScreen
    {
        public ViewModelActivator Activator { get; } = new();
        public RoutingState Router { get; } = new();

        public MainWindowViewModel()
        {
            var carousel = new CarouselViewModel(this);
            Router.Navigate.Execute(carousel);

            this.WhenActivated((CompositeDisposable disposables) =>
            {

            });
       
        }
    }
}