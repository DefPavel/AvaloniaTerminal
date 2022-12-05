using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive.Disposables;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using AvaloniaTerminal.ViewModels;
using DynamicData.Binding;
using ReactiveUI;

namespace AvaloniaTerminal.Views;

public partial class CarouselView : ReactiveUserControl<CarouselViewModel>
{
    private readonly DispatcherTimer _disTimer = new();
    private readonly Carousel _carousel;

    private void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        if (_carousel.SelectedIndex != _carousel.ItemCount - 1)
            _carousel.Next();
        else
            _carousel.SelectedIndex = 0;
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
            
            var files = Directory.GetFiles("C:\\data-avalonia\\afisha", "*.jpg");
            var images = new ObservableCollection<Image>();
            foreach (var item in files)
            {
               images.Add(new Image { Source = new Bitmap(item) });
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