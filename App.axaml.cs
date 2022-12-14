using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaTerminal.ViewModels;
using AvaloniaTerminal.ViewModels.Faculty;
using AvaloniaTerminal.Views;
using ReactiveUI;
using Splat;

namespace AvaloniaTerminal
{
    public class App : Application
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
               // Locator.CurrentMutable.Register<IViewFor<TimetableViewModel>>(() => new TimetableView());
                Locator.CurrentMutable.Register<IViewFor<CarouselViewModel>>(() => new CarouselView());
                Locator.CurrentMutable.Register<IViewFor<DirectorsIPRViewModel>>(() => new DirectorsIPRView());
                Locator.CurrentMutable.Register<IViewFor<StructuraIPRViewModel>>(() => new StructuraIPRView());

                Locator.CurrentMutable.Register<IViewFor<StructuralAllViewModel>>(() => new StructurAllView());
                
                Locator.CurrentMutable.Register<IViewFor<DirectorsAllViewModel>>(() => new DirectorsUniversityView());
                
                Locator.CurrentMutable.Register<IViewFor<NoticeViewModel>>(() => new NoticeView());
                
                Locator.CurrentMutable.Register<IViewFor<SturcurFizViewModel>>(() => new StructurFizView());
                Locator.CurrentMutable.Register<IViewFor<DirectorsFizViewModel>>(() => new DirectorsFizView());
                
                Locator.CurrentMutable.Register<IViewFor<NewTimeTableViewModel>>(() => new NewTimeTableView());
                
                Locator.CurrentMutable.Register<IViewFor<ContactVizViewModel>>(() => new ContactVIzView());
                Locator.CurrentMutable.Register<IViewFor<ContactIPRViewModel>>(() => new ContactIprView());
                Locator.CurrentMutable.Register<IViewFor<ContactsUniversityViewModel>>(() => new ContactsUniversityView());
                
                Locator.CurrentMutable.Register<IViewFor<IPRViewModel>>(() => new IprView());
                Locator.CurrentMutable.Register<IViewFor<FizVospViewModel>>(() => new FizVospView());
                
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