using System;
using System.Reactive.Disposables;
using AvaloniaTerminal.Services;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Linq;
using Splat;

namespace AvaloniaTerminal.ViewModels;

public sealed class MenuViewModel : ViewModelBase, IRoutableViewModel
{
    #region Свойства

    private readonly IMenuService? _menuService;
    private readonly IApplicationInfo? _applicationInfo;
    public string UrlPathSegment => nameof(MenuViewModel);
    public IScreen HostScreen { get; } 

    #endregion

    #region Команды
    public ReactiveCommand<Unit, IRoutableViewModel> GetInfoView { get; }
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
        
        GetInfoView = ReactiveCommand.CreateFromTask( async _ => await HostScreen.Router.Navigate.Execute(new CarouselViewModel(HostScreen)));
        GetInfoView.IsExecuting.ToProperty(this, x => x.IsBusy, out isBusy);
        
        
        this.WhenActivated((CompositeDisposable disposables) =>
        {
            // Added here just for testing
            GC.Collect();
        });
    }


   
}