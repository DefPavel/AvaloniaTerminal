using System;
using System.Collections.ObjectModel;
using System.IO;
using AvaloniaTerminal.Services;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using AvaloniaTerminal.Models;
using Splat;

namespace AvaloniaTerminal.ViewModels;

public class CarouselViewModel : ViewModelBase, IRoutableViewModel
{
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
    
    public string? UrlPathSegment => nameof(CarouselViewModel);
    public IScreen HostScreen { get; }

    private readonly ICarouselService _carouselService;
    
    #region Команды
    
    public ReactiveCommand<Unit, IRoutableViewModel> GetMenuView { get; }
    
    public ReactiveCommand<Unit, Unit> GetFilesAfish { get; }
    
    
    #endregion

    #region Логика

    private void GetFiles()
    {
        var files = Directory.GetFiles("Z:\\data-avalonia\\afisha", "*.jpg");

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

        //GetFilesAfish = ReactiveCommand.Create(GetFiles);
        
        //GetFilesAfish.IsExecuting.ToProperty(this, x => x.IsBusy, out isBusy);
        
        this.WhenActivated((CompositeDisposable disposables) =>
        {
            // Added here just for testing
            GC.Collect();
        });
    }
}