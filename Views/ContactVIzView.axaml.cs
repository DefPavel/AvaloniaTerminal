using System.Reactive.Disposables;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaTerminal.ViewModels.Faculty;
using ReactiveUI;

namespace AvaloniaTerminal.Views;

public partial class ContactVIzView : ReactiveUserControl<ContactVizViewModel>
{
    public ContactVIzView()
    {
        this.WhenActivated(disposables =>
        {
            Disposable.Create(() => { }).DisposeWith(disposables);
        });
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}