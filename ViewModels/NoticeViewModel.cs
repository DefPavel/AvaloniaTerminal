using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using AvaloniaTerminal.Models;
using DynamicData;

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

    private IEnumerable<Image>? _images;
    public IEnumerable<Image>? Images
    {
        get => _images;
        set =>  this.RaiseAndSetIfChanged(ref _images, value);
    }
    public IScreen HostScreen { get; }

    #region Команды
    public ReactiveCommand<Unit, IRoutableViewModel> GetBack { get; }
    #endregion

    private static IEnumerable<Image> GetFiles()
    {
        var images = new ObservableCollection<Image>();
        try
        {
            var files =  Directory.EnumerateFiles("C:\\data-avalonia\\afisha", "*.*", SearchOption.AllDirectories)
                .Where(s => s.EndsWith(".png") || s.EndsWith(".jpg"));

            var enumerable = files as string[] ?? files.ToArray();
            if (enumerable.Any())
            {
                foreach (var item in enumerable)
                {
                    images.Add(new Image { Source = new Bitmap(item) });
                }     
            }
        }
        catch (Exception exception)
        {
            var messageBoxStandardWindow = MessageBox.Avalonia.MessageBoxManager
                .GetMessageBoxStandardWindow("message", exception.Message);
            messageBoxStandardWindow.Show();
        }
        return images;
    }
    
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

            Images = GetFiles();
            
            Disposable.Create(() => { _disTimer.Stop(); }).DisposeWith(disposables);
            
            GC.Collect();
        });
    }
    
    private async void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        await HostScreen.Router.NavigateAndReset.Execute(new CarouselViewModel(HostScreen));
    }
    
}