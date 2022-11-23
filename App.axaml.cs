using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaTerminal.ViewModels;
using AvaloniaTerminal.Views;
using ReactiveUI;
using Splat;

namespace AvaloniaTerminal
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                Locator.CurrentMutable.Register<IViewFor<MenuViewModel>>(() => new MenuView());
                Locator.CurrentMutable.Register<IViewFor<InfoViewModel>>(() => new InfoView());
                Locator.CurrentMutable.Register<IViewFor<CarouselViewModel>>(() => new CarouselView());

                Bootstrapper.Register(Locator.CurrentMutable, Locator.Current);

                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}