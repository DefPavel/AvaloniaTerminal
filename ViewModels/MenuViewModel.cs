using System;
using System.Configuration;
using System.IO;
using System.Reactive.Disposables;
using AvaloniaTerminal.Services;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Splat;
using Avalonia.Threading;
using AvaloniaTerminal.Models;
using AvaloniaTerminal.ViewModels.Faculty;

namespace AvaloniaTerminal.ViewModels;

public sealed class MenuViewModel : ViewModelBase, IRoutableViewModel
{
    #region Свойства
    private static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    private static readonly string? NumberFaculty = ConfigurationManager.AppSettings["numberFaculty"];
    private readonly DispatcherTimer _disTimer = new();
    private readonly DispatcherTimer _saveTimer = new();
    private readonly IMenuService? _menuService;
    private readonly IApplicationInfo? _applicationInfo;
    public string UrlPathSegment => nameof(MenuViewModel);
    public IScreen HostScreen { get; }

    private string? _savePin = string.Empty;

    public string? SavePin
    {
        get => _savePin;
        set =>  this.RaiseAndSetIfChanged(ref _savePin, value);
    }

    #endregion

    #region Команды
    public ReactiveCommand<Unit, Unit> GetInfoView { get; }
    public ReactiveCommand<Unit, Unit> GetTimeTableView { get; }
    public ReactiveCommand<Unit, Unit> GetDirectorsView { get; }
    public ReactiveCommand<Unit, Unit> GetStructuralView { get; }
    
    public ReactiveCommand<Unit, Unit> GetNoticeView { get; }
    public ReactiveCommand<Unit, Unit> GetContactView { get; }

    #endregion

    #region Логика

    // Переход обратно на карусель каждые 40 сек
    private async void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        await HostScreen.Router.NavigateAndReset.Execute(new CarouselViewModel(HostScreen));
    }

    private static async Task ChangeDataJson(string pin)
    {
        var settings = new Settings { Pincode = pin};
        var settingsText = JsonSerializer.Serialize(settings);
                
        await File.WriteAllTextAsync(Path.Combine(AppData, "settings.json"), settingsText);
    }
    private static async void CheckSavePin(object? sender, EventArgs e)
    {
        var settings = new Settings { Pincode = string.Empty};
        var settingsText = JsonSerializer.Serialize(settings);
                
        await File.WriteAllTextAsync(Path.Combine(AppData, "settings.json"), settingsText);
    }

    #region Навигация по кнопкам

    private async Task GetStructural()
    {
        _disTimer.Interval = TimeSpan.FromSeconds(40);

        if (string.IsNullOrWhiteSpace(SavePin))
        {
            var check = await _menuService!.GetViewCheckCode(TimeSpan.FromSeconds(40));
            if (check.Status && check.Pin.Length > 0)
            {
                await ChangeDataJson(check.Pin);
                
                switch (NumberFaculty)
                {
                    case "1":
                        await HostScreen.Router.NavigateAndReset.Execute(new StructuraIPRViewModel(HostScreen));
                        break;
                    case "2":
                        await HostScreen.Router.NavigateAndReset.Execute(new SturcurFizViewModel(HostScreen));
                        break;
                }
            }     
        }
        else
        {
            switch (NumberFaculty)
            {
                case "1":
                    await HostScreen.Router.NavigateAndReset.Execute(new StructuraIPRViewModel(HostScreen));
                    break;
                case "2":
                    await HostScreen.Router.NavigateAndReset.Execute(new SturcurFizViewModel(HostScreen));
                    break;
            }
        }
        
    }
    private async Task GetInformationView()
    {
        // Может быть есть и намного лучше способ,
        // но у меня в данной структуре работает только такой
        _disTimer.Interval = TimeSpan.FromSeconds(40);
        // TODO: Когда пройдет еще время, найдите способ лучше передавать данные между Window и UseCotrol в (MVVM)
        if (string.IsNullOrWhiteSpace(SavePin))
        {
            var check = await _menuService!.GetViewCheckCode(TimeSpan.FromSeconds(40));
            if (check.Status && check.Pin.Length > 0)
            {
                await ChangeDataJson(check.Pin);
                switch (NumberFaculty)
                {
                    case "1" : await HostScreen.Router.NavigateAndReset.Execute(new IPRViewModel(HostScreen));
                        break;
                    case "2" : await HostScreen.Router.NavigateAndReset.Execute(new FizVospViewModel(HostScreen));
                        break;
                }
            }
        }
        else
        {
            switch (NumberFaculty)
            {
                case "1" : await HostScreen.Router.NavigateAndReset.Execute(new IPRViewModel(HostScreen));
                    break;
                case "2" : await HostScreen.Router.NavigateAndReset.Execute(new FizVospViewModel(HostScreen));
                    break;
            }
        }

      
       
    }
    private async Task GetDirectors()
    {
        _disTimer.Interval = TimeSpan.FromSeconds(40);

        if (string.IsNullOrWhiteSpace(SavePin))
        {
            var check = await _menuService!.GetViewCheckCode(TimeSpan.FromSeconds(40));
            if (check.Status && check.Pin.Length > 0)
            {
                await ChangeDataJson(check.Pin);
                switch (NumberFaculty)
                {
                    case "1":
                        await HostScreen.Router.NavigateAndReset.Execute(new DirectorsIPRViewModel(HostScreen));
                        break;
                    case "2":
                        await HostScreen.Router.NavigateAndReset.Execute(new DirectorsFizViewModel(HostScreen));
                        break;
                }
            }
        }
        else
        {
            switch (NumberFaculty)
            {
                case "1":
                    await HostScreen.Router.NavigateAndReset.Execute(new DirectorsIPRViewModel(HostScreen));
                    break;
                case "2":
                    await HostScreen.Router.NavigateAndReset.Execute(new DirectorsFizViewModel(HostScreen));
                    break;
            }
        }
       
       
    }
    private async Task GetPhones()
    {
        _disTimer.Interval = TimeSpan.FromSeconds(40);

        if (string.IsNullOrWhiteSpace(SavePin))
        {
            var check = await _menuService!.GetViewCheckCode(TimeSpan.FromSeconds(40));
            if (check.Status && check.Pin.Length > 0)
            {
                await ChangeDataJson(check.Pin);
                switch (NumberFaculty)
                {
                    case "1":
                        await HostScreen.Router.NavigateAndReset.Execute(new ContactIPRViewModel(HostScreen));
                        break;
                    case "2":
                        await HostScreen.Router.NavigateAndReset.Execute(new ContactVizViewModel(HostScreen));
                        break;
                }
            }     
        }
        else
        {
            switch (NumberFaculty)
            {
                case "1":
                    await HostScreen.Router.NavigateAndReset.Execute(new ContactIPRViewModel(HostScreen));
                    break;
                case "2":
                    await HostScreen.Router.NavigateAndReset.Execute(new ContactVizViewModel(HostScreen));
                    break;
            }
        }

       
       
    }
    private async Task GetTimeTable()
    {
        _disTimer.Interval = TimeSpan.FromSeconds(40);
        if (string.IsNullOrWhiteSpace(SavePin))
        {
            var check = await _menuService!.GetViewCheckCode(TimeSpan.FromSeconds(40));

            if (check.Status && check.Pin.Length > 0)
            {
                await ChangeDataJson(check.Pin);
                // _disTimer.Stop();
                await HostScreen.Router.NavigateAndReset.Execute(new NewTimeTableViewModel(HostScreen));
            }
        }
        else
        {
            await HostScreen.Router.NavigateAndReset.Execute(new NewTimeTableViewModel(HostScreen));
        }

       

    }
    private async Task GetNotice()
    {
        _disTimer.Interval = TimeSpan.FromSeconds(40);
        if (string.IsNullOrWhiteSpace(SavePin))
        {
            var check = await _menuService!.GetViewCheckCode(TimeSpan.FromSeconds(40));
        
            if (check.Status && check.Pin.Length > 0)
            {
                await ChangeDataJson(check.Pin);
                // _disTimer.Stop();
                await HostScreen.Router.NavigateAndReset.Execute(new NoticeViewModel(HostScreen));
            }
        }
        else
        {
            await HostScreen.Router.NavigateAndReset.Execute(new NoticeViewModel(HostScreen));
        }
       

    }   

    #endregion
    
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

        GetNoticeView = ReactiveCommand.CreateFromTask(async _ => await GetNotice());
        GetNoticeView.IsExecuting.ToProperty(this, x => x.IsBusy, out isBusy);

        this.WhenActivated(disposables =>
        {
            // this.WhenAnyValue(e => e.SpanTimeSpan).Subscribe(span => _disTimer.Interval = span);
            _disTimer.Interval = TimeSpan.FromSeconds(40);
            _disTimer.Tick += DispatcherTimer_Tick;
            _disTimer.Start();
            
            _saveTimer.Interval = TimeSpan.FromMinutes(2);
            _saveTimer.Tick += CheckSavePin;
            _saveTimer.Start();
            
           /* if (!File.Exists(Path.Combine(AppData, "settings.json"))) 
            {
                var settings = new Settings { Pincode = string.Empty };
                using var createStream = File.Create(Path.Combine(AppData, "settings.json"));
                JsonSerializer.Serialize(createStream, settings);
            }
            */
            SavePin = JsonSerializer.Deserialize<Settings>(File.ReadAllText($"{AppData}\\settings.json"))?.Pincode;

            Disposable.Create(() =>
            {
                _disTimer.Stop();
                _saveTimer.Stop();
            }).DisposeWith(disposables);
            GC.Collect();
        });
    }


   
}