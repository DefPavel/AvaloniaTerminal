using System.Reactive.Disposables;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaTerminal.ViewModels;
using ReactiveUI;

namespace AvaloniaTerminal.Views;

public partial class DirectorsUniversityView :  ReactiveUserControl<DirectorsAllViewModel>
{
    public DirectorsUniversityView()
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