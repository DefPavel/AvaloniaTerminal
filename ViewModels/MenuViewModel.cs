using System;
using System.Reactive.Disposables;
using AvaloniaTerminal.Services;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia;
using AvaloniaTerminal.Views;
using Splat;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;

namespace AvaloniaTerminal.ViewModels;

public sealed class MenuViewModel : ViewModelBase, IRoutableViewModel
{
    #region Свойства

    private readonly DispatcherTimer _disTimer = new();

    private readonly IMenuService? _menuService;
    private readonly IApplicationInfo? _applicationInfo;
    public string UrlPathSegment => nameof(MenuViewModel);
    public IScreen HostScreen { get; } 

    #endregion

    #region Команды
    public ReactiveCommand<Unit, Unit> GetInfoView { get; }

    #endregion

    #region Логика

    private async void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        await HostScreen.Router.Navigate.Execute(new CarouselViewModel(HostScreen));
    }

    private async Task GetInformationView()
    {
        // Может быть есть и намного лучше способ,
        // но у меня в данной структуре работает только такой
        
        // TODO: Когда пройдет еще время, найдите способ лучше передавать данные между Window и UseCotrol в (MVVM)
        
        CheckCodeViewModel viewModel = new();
        CheckCodeView view = new() { DataContext = viewModel };
        
        var mainWindow = Application.Current?.ApplicationLifetime 
            is IClassicDesktopStyleApplicationLifetime desktop 
            ? desktop.MainWindow 
            : null;

        await view.ShowDialog(mainWindow);

        if(viewModel.Status)
            await HostScreen.Router.Navigate.Execute(new InfoViewModel(HostScreen));
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

        _disTimer.Interval = TimeSpan.FromMinutes(1);
        _disTimer.Tick += DispatcherTimer_Tick;
        _disTimer.Start();

        this.WhenActivated((CompositeDisposable _) =>
        {
            // Added here just for testing
            GC.Collect();
        });
    }


   
}