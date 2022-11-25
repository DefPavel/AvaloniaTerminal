using System.Reactive.Disposables;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaTerminal.ViewModels.Faculty;
using ReactiveUI;

namespace AvaloniaTerminal.Views;

public partial class IprView : ReactiveUserControl<IPRViewModel>
{
    public IprView()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            Disposable.Create(() => { }).DisposeWith(disposables);
        });
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}