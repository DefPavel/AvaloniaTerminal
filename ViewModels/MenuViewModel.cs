using System;
using System.Reactive.Disposables;
using AvaloniaTerminal.Services;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using AvaloniaTerminal.Views;
using Splat;
using Avalonia.Controls;

namespace AvaloniaTerminal.ViewModels;

public sealed class MenuViewModel : ViewModelBase, IRoutableViewModel
{
    #region Свойства

    private readonly IMenuService? _menuService;
    private readonly IApplicationInfo? _applicationInfo;
    public string UrlPathSegment => nameof(MenuViewModel);
    public IScreen HostScreen { get; } 

    #endregion

    #region Команды
    public ReactiveCommand<Unit, Unit> GetInfoView { get; }

    #endregion

    #region Логика

    private async Task GetInformationView()
    {
       CheckCodeViewModel viewModel = new();
       CheckCodeView view = new() { DataContext = viewModel };

       view.Show();

       await HostScreen.Router.Navigate.Execute(new CarouselViewModel(HostScreen));
    }

    #endregion

    public MenuViewModel(IScreen screen) :
        this(screen, 
            Locator.Current.GetService<IMenuService>(),
            Locator.Current.GetService<IApplicationInfo>()){ }
    public MenuViewModel(IScreen screen, IMenuService? menuService, IApplicationInfo? applicationInfo)
    {
        HostScreen = screen;
        _menuService = menuService;
        _applicationInfo = applicationInfo;
        
        GetInfoView = ReactiveCommand.CreateFromTask( async _ => await GetInformationView());
        
        GetInfoView.IsExecuting.ToProperty(this, x => x.IsBusy, out isBusy);
        
        
        this.WhenActivated((CompositeDisposable _) =>
        {
            // Added here just for testing
            GC.Collect();
        });
    }


   
}