using System.Reactive.Disposables;
using Avalonia.ReactiveUI;
using AvaloniaTerminal.ViewModels;
using ReactiveUI;

namespace AvaloniaTerminal.Views;

public partial class InfoView : ReactiveUserControl<InfoViewModel>
{
    public InfoView()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            Disposable.Create(() => { }).DisposeWith(disposables);
        });
    }
    
}