using System;
using System.Configuration;
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
using AvaloniaTerminal.ViewModels.Faculty;

namespace AvaloniaTerminal.ViewModels;

public sealed class MenuViewModel : ViewModelBase, IRoutableViewModel
{
    #region Свойства

    
    private static readonly string? NumberFaculty = ConfigurationManager.AppSettings["numberFaculty"];
    private readonly DispatcherTimer _disTimer = new();

    private readonly IMenuService? _menuService;
    private readonly IApplicationInfo? _applicationInfo;
    public string UrlPathSegment => nameof(MenuViewModel);
    public IScreen HostScreen { get; }

    private TimeSpan _span = TimeSpan.FromSeconds(30);

    public TimeSpan SpanTimeSpan
    {
        get => _span;
        set => this.RaiseAndSetIfChanged(ref _span, value);
    }

    #endregion

    #region Команды
    public ReactiveCommand<Unit, Unit> GetInfoView { get; }
    public ReactiveCommand<Unit, Unit> GetTimeTableView { get; }
    public ReactiveCommand<Unit, Unit> GetDirectorsView { get; }
    public ReactiveCommand<Unit, Unit> GetStructuralView { get; }
    
    public ReactiveCommand<Unit, Unit> GetContactView { get; }

    #endregion

    #region Логика

    private async void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        await HostScreen.Router.NavigateAndReset.Execute(new CarouselViewModel(HostScreen));
    }

    private async Task GetStructural()
    {
        // Может быть есть и намного лучше способ,
        // но у меня в данной структуре работает только такой

        // TODO: Когда пройдет еще время, найдите способ лучше передавать данные между Window и UseCotrol в (MVVM)

        var timeSpan = _disTimer.Interval.Add(TimeSpan.FromSeconds(40));

        CheckCodeViewModel viewModel = new();
        CheckCodeView view = new() { DataContext = viewModel };

        var mainWindow = Application.Current?.ApplicationLifetime
            is IClassicDesktopStyleApplicationLifetime desktop
            ? desktop.MainWindow
            : null;

        await view.ShowDialog(mainWindow);

        if (viewModel.Status)
        {
            switch (NumberFaculty)
            {
                case "1":
                    await HostScreen.Router.NavigateAndReset.Execute(new StructuraIPRViewModel(HostScreen));
                    break;
            }
            // _disTimer.Stop();
        }

    }

    private async Task GetInformationView()
    {
        // Может быть есть и намного лучше способ,
        // но у меня в данной структуре работает только такой

        // TODO: Когда пройдет еще время, найдите способ лучше передавать данные между Window и UseCotrol в (MVVM)

        var timeSpan = _disTimer.Interval.Add(TimeSpan.FromSeconds(40));

        CheckCodeViewModel viewModel = new();
        CheckCodeView view = new() { DataContext = viewModel };
        
        var mainWindow = Application.Current?.ApplicationLifetime 
            is IClassicDesktopStyleApplicationLifetime desktop 
            ? desktop.MainWindow 
            : null;

        await view.ShowDialog(mainWindow);

        if (viewModel.Status)
        {
            switch (NumberFaculty)
            {
                case "1" : await HostScreen.Router.NavigateAndReset.Execute(new IPRViewModel(HostScreen));
                    break;
            }
           // _disTimer.Stop();
        }
       
    }

    private async Task GetDirectors()
    {
        var timeSpan = _disTimer.Interval.Add(TimeSpan.FromSeconds(40));

        CheckCodeViewModel viewModel = new();
        CheckCodeView view = new() { DataContext = viewModel };

        var mainWindow = Application.Current?.ApplicationLifetime
            is IClassicDesktopStyleApplicationLifetime desktop
            ? desktop.MainWindow
            : null;

        await view.ShowDialog(mainWindow);

        if (viewModel.Status)
        {
            switch (NumberFaculty)
            {
                case "1":
                    await HostScreen.Router.NavigateAndReset.Execute(new DirectorsIPRViewModel(HostScreen));
                    break;
            }
        }
       
    }
    
    private async Task GetPhones()
    {
        var timeSpan = _disTimer.Interval.Add(TimeSpan.FromSeconds(40));

        CheckCodeViewModel viewModel = new();
        CheckCodeView view = new() { DataContext = viewModel };

        var mainWindow = Application.Current?.ApplicationLifetime
            is IClassicDesktopStyleApplicationLifetime desktop
            ? desktop.MainWindow
            : null;

        await view.ShowDialog(mainWindow);

        if (viewModel.Status)
        {
            switch (NumberFaculty)
            {
                case "1":
                    await HostScreen.Router.NavigateAndReset.Execute(new ContactIPRViewModel(HostScreen));
                    break;
            }
        }
       
    }

    private async Task GetTimeTable()
    {
        var timeSpan = _disTimer.Interval.Add(TimeSpan.FromSeconds(40));

        CheckCodeViewModel viewModel = new();
        CheckCodeView view = new() { DataContext = viewModel };
        
        var mainWindow = Application.Current?.ApplicationLifetime 
            is IClassicDesktopStyleApplicationLifetime desktop 
            ? desktop.MainWindow 
            : null;

        await view.ShowDialog(mainWindow);

        if(viewModel.Status)
        {
           // _disTimer.Stop();
            await HostScreen.Router.NavigateAndReset.Execute(new TimetableViewModel(HostScreen));
        }

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

        GetTimeTableView = ReactiveCommand.CreateFromTask(async _ => await GetTimeTable());

        GetTimeTableView.IsExecuting.ToProperty(this, x => x.IsBusy, out isBusy);

        GetDirectorsView = ReactiveCommand.CreateFromTask(async _ => await GetDirectors());

        GetDirectorsView.IsExecuting.ToProperty(this, x => x.IsBusy, out isBusy);

        GetStructuralView = ReactiveCommand.CreateFromTask(async _ => await GetStructural());

        GetStructuralView.IsExecuting.ToProperty(this, x => x.IsBusy, out isBusy);
        
        GetContactView = ReactiveCommand.CreateFromTask(async _ => await GetPhones());

        GetContactView.IsExecuting.ToProperty(this, x => x.IsBusy, out isBusy);

        this.WhenActivated((CompositeDisposable disposables) =>
        {
            // this.WhenAnyValue(e => e.SpanTimeSpan).Subscribe(span => _disTimer.Interval = span);
            _disTimer.Interval = TimeSpan.FromSeconds(30);
            _disTimer.Tick += DispatcherTimer_Tick;
            _disTimer.Start();

            Disposable.Create(() => { _disTimer.Stop(); }).DisposeWith(disposables);
            // Added here just for testing
            GC.Collect();
        });
    }


   
}