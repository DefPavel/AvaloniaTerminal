using System;
using AvaloniaTerminal.Services;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Splat;

namespace AvaloniaTerminal.ViewModels;

public class CarouselViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment => nameof(CarouselViewModel);
    public IScreen HostScreen { get; }

    private readonly ICarouselService _carouselService;
    
    #region Команды
    
    public ReactiveCommand<Unit, IRoutableViewModel> GetMenuView { get; }
    
    #endregion

    public CarouselViewModel(IScreen screen) :
        this(screen, 
            Locator.Current.GetService<ICarouselService>()){ }
    public CarouselViewModel(IScreen screen , ICarouselService carouselService)
    {
        HostScreen = screen;
        _carouselService = carouselService;
        
        GetMenuView = ReactiveCommand.CreateFromTask( async _ => await HostScreen.Router.NavigateAndReset.Execute(new MenuViewModel(HostScreen)));
            
        this.WhenActivated((CompositeDisposable disposables) =>
        {
            // Added here just for testing
            GC.Collect();
        });
    }
}