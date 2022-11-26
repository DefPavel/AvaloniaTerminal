using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using AvaloniaTerminal.Models;

namespace AvaloniaTerminal.ViewModels;

public class NoticeViewModel :  ViewModelBase, IRoutableViewModel
{
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
            var files = Directory.GetFiles("C:\\data-avalonia\\afisha", "*.jpg");

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
            GC.Collect();
        });
    }
    
}