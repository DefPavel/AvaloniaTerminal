using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
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
        _carousel.IsVirtualized = true;
        
        this.WhenActivated(disposables =>
        {
            Disposable.Create(() => { }).DisposeWith(disposables);
        });
        
        _disTimer.Interval = TimeSpan.FromSeconds(5);
        _disTimer.Tick += DispatcherTimer_Tick;
        _disTimer.Start();
        
        
        /*carousel.Items = new ObservableCollection<Image>
        {
            new(){ Source = new Bitmap("https://lgpu.org/data/sliders/600fb31e2dd1a.jpg") },
            new(){ Source = new Bitmap("/Assets/slide2.jpg")}
        };
        */

    }
}