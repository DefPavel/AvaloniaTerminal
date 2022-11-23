using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using AvaloniaTerminal.ViewModels;
using ReactiveUI;

namespace AvaloniaTerminal.Views;

public partial class MenuView :  ReactiveUserControl<MenuViewModel>
{
    public MenuView()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            Disposable.Create(() => { }).DisposeWith(disposables);
        });
    }
}