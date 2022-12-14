using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using AvaloniaTerminal.Services;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AvaloniaTerminal.Models;
using AvaloniaTerminal.Services.Api;
using Splat;

namespace AvaloniaTerminal.ViewModels;

public class CarouselViewModel : ViewModelBase, IRoutableViewModel
{
    
    private static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    private static readonly string? NumberFaculty = ConfigurationManager.AppSettings["numberFaculty"];
    
    private ObservableCollection<ListFiles>? _pdfFile = new();
    public ObservableCollection<ListFiles>? PdfFiles
    {
        get => _pdfFile;
        set =>  this.RaiseAndSetIfChanged(ref _pdfFile, value);
    }

    private ListFiles? _selectedPdf = new();

    public ListFiles? SelectedPdf
    {
        get => _selectedPdf;
        set =>  this.RaiseAndSetIfChanged(ref _selectedPdf, value);
    }
    private string? _titleHeader = string.Empty;
    public string? TitleHeader
    {
        get => _titleHeader;
        set =>  this.RaiseAndSetIfChanged(ref _titleHeader, value);
    }
    
    public string? UrlPathSegment => nameof(CarouselViewModel);
    public IScreen HostScreen { get; }

    private readonly ICarouselService _carouselService;
    
    #region Команды
    
    public ReactiveCommand<Unit, IRoutableViewModel> GetMenuView { get; }
    
    // public ReactiveCommand<Unit, Unit> GetIpChecked { get; }

   // public ReactiveCommand<Unit, Unit> GetFilesAfish { get; }
    
    
    #endregion

    #region Логика

    private static async Task DispatherApi()
    {
        try
        {
            await QueryService.JsonObjectWithToken(token: "secret", "http://jmu.api.lgpu.org/bot/telegram/ping-terminal", "POST");
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
    private void GetFiles()
    {
        var files = Directory.GetFiles("C:\\data-avalonia\\afisha", "*.jpg");

        if (files.Length <= 0) return;
        foreach (var item in files)
        {
            var splitter = item.Split("\\");
            PdfFiles?.Add(new ListFiles()
            {
                Name = splitter[^1],
                OriginalName = item
            });
        }

    }

    #endregion

    public CarouselViewModel(IScreen screen) :
        this(screen, 
            Locator.Current.GetService<ICarouselService>()){ }
    public CarouselViewModel(IScreen screen , ICarouselService carouselService)
    {
        HostScreen = screen;
        _carouselService = carouselService;
        
        GetMenuView = ReactiveCommand.CreateFromTask( async _ => await HostScreen.Router.NavigateAndReset.Execute(new MenuViewModel(HostScreen)));
        // GetIpChecked = ReactiveCommand.CreateFromTask(async _ => await DispatherApi());

        this.WhenActivated( async (CompositeDisposable disposables) =>
        {
            // GetIpChecked.Execute();
            // await QueryService.JsonObjectWithToken(token: "secret", 
            //     $"http://jmu.api.lgpu.org/bot/telegram/ping-terminal/{NumberFaculty}", "POST");
            TitleHeader = NumberFaculty switch
            {
                "1" => "Информационный стенд ИНСТИТУТА ПРОФЕССИОНАЛЬНОГО РАЗВИТИЯ",
                "2" => "Информационный стенд ИНСТИТУТА ФИЗИЧЕСКОГО ВОСПИТАНИЯ И СПОРТА",
                _ => TitleHeader
            };
           
            if (!File.Exists(Path.Combine(AppData, "settings.json"))) 
            {
                var settings = new Settings { Pincode = string.Empty };
                await using var createStream = File.Create(Path.Combine(AppData, "settings.json"));
                JsonSerializer.Serialize(createStream, settings);
            }
            else
            {
                var settings = new Settings { Pincode = string.Empty};
                var settingsText = JsonSerializer.Serialize(settings);
                
                await File.WriteAllTextAsync(Path.Combine(AppData, "settings.json"), settingsText);
            }
            
            // Added here just for testing
            GC.Collect();
        });
    }
}