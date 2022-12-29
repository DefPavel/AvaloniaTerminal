using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Threading;
using ReactiveUI;

namespace AvaloniaTerminal.ViewModels;

public class StructuralAllViewModel :  ViewModelBase, IRoutableViewModel
{
    private readonly DispatcherTimer _disTimer = new();
    public string? UrlPathSegment => nameof(StructuralAllViewModel);
    
    public IScreen HostScreen { get; }

    #region Команды
    public ReactiveCommand<Unit, IRoutableViewModel> GetBack { get; }
    
    #endregion

    private async void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        await HostScreen.Router.NavigateAndReset.Execute(new CarouselViewModel(HostScreen));
    }
    
    public StructuralAllViewModel(IScreen screen)
    {
        HostScreen = screen;

        GetBack = ReactiveCommand.CreateFromTask(async _ =>
            await HostScreen.Router.Navigate.Execute(new MenuViewModel(HostScreen)));

        this.WhenActivated(disposables =>
        {
            _disTimer.Interval = TimeSpan.FromMinutes(2);
            _disTimer.Tick += DispatcherTimer_Tick;
            _disTimer.Start();
            Disposable.Create(() => { _disTimer.Stop(); }).DisposeWith(disposables);
            
            GC.Collect();
        });
    }
    
    
}