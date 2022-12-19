using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using AvaloniaTerminal.Models;
using AvaloniaTerminal.Services.Api;
using AvaloniaTerminal.ViewModels;
using DynamicData.Binding;
using ReactiveUI;

namespace AvaloniaTerminal.Views;

public partial class CarouselView : ReactiveUserControl<CarouselViewModel>
{
    // private static readonly string? NumberFaculty = ConfigurationManager.AppSettings["numberFaculty"];
    
    private readonly DispatcherTimer _disTimer = new();

    private readonly DispatcherTimer _dispatcherApi = new();
    
    private readonly Carousel _carousel;

    private void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        if (_carousel.SelectedIndex != _carousel.ItemCount - 1)
            _carousel.Next();
        else
            _carousel.SelectedIndex = 0;
    }
    private static async void DispatherApi(object? sender, EventArgs e)
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
    public CarouselView()
    {
        InitializeComponent();
        
        _carousel = this.Get<Carousel>("ItemsCarousel");

        this.WhenActivated(disposables =>
        {
            _disTimer.Interval = TimeSpan.FromSeconds(10);
            _disTimer.Tick += DispatcherTimer_Tick;
            _disTimer.Start();

            _dispatcherApi.Interval = TimeSpan.FromSeconds(30);
            _dispatcherApi.Tick += DispatherApi;
            _dispatcherApi.Start();


            var files = 
                Directory.EnumerateFiles("C:\\data-avalonia\\afisha", "*.*", SearchOption.AllDirectories)
                    .Where(s => s.EndsWith(".png") || s.EndsWith(".jpg"));
                //Directory.GetFiles("C:\\data-avalonia\\afisha", "*.jpg");
            var images = new ObservableCollection<Image>();
            var enumerable = files as string[] ?? files.ToArray();
            
            if (enumerable.Any())
            {
                foreach (var item in enumerable)
                {
                    images.Add(new Image { Source = new Bitmap(item) });
                }
     
            }
           
            _carousel.Items = images;

            Disposable.Create(() => { _disTimer.Stop(); }).DisposeWith(disposables);
        });

        /*this.WhenValueChanged(x => x.ViewModel!.PdfFiles)
            .Subscribe(items =>
            {
                var convertImg = new ObservableCollection<Image>();

                foreach (var item in items)
                {
                    convertImg.Add(new Image { Source = new Bitmap(item.OriginalName) });
                }

            });
        */
            
        /*_carousel.Items = new ObservableCollection<Image>
        {
            new(){ Source = new Bitmap("C:\\data-avalonia\\afisha\\afk.jpg") },
            new(){ Source = new Bitmap("/Assets/slide2.jpg")}
        };
        */


    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}