using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using AvaloniaTerminal.Models;

namespace AvaloniaTerminal.ViewModels;

public class NoticeViewModel :  ViewModelBase, IRoutableViewModel
{
    private readonly DispatcherTimer _disTimer = new();
    public string? UrlPathSegment => nameof(NoticeViewModel);

    private ObservableCollection<ListFiles>? _pdfFile = new();
    public ObservableCollection<ListFiles>? PdfFiles
    {
        get => _pdfFile;
        set =>  this.RaiseAndSetIfChanged(ref _pdfFile, value);
    }
    
    private ObservableCollection<Image>? _images = new();
    public ObservableCollection<Image>? Images
    {
        get => _images;
        set =>  this.RaiseAndSetIfChanged(ref _images, value);
    }
    public IScreen HostScreen { get; }

    #region Команды
    public ReactiveCommand<Unit, IRoutableViewModel> GetBack { get; }
    #endregion

    public NoticeViewModel(IScreen screen)
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
            
            var files = Directory.GetFiles("Z:\\data-avalonia\\afisha", "*.jpg");

            if (files.Length > 0)
            {
                foreach (var item in files)
                {
                    Images?.Add(new Image { Source = new Bitmap(item) });
                    /*var splitter = item.Split("\\");
                    PdfFiles?.Add(new ListFiles()
                    {
                        Name = splitter[^1],
                        OriginalName = item
                    });
                    */
                }     
            }
           
            GC.Collect();
        });
    }
    
    private async void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        await HostScreen.Router.NavigateAndReset.Execute(new CarouselViewModel(HostScreen));
    }
    
}