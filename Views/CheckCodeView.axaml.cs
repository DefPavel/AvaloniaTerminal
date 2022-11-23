using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaTerminal.ViewModels;
using ReactiveUI;

namespace AvaloniaTerminal.Views;

public partial class CheckCodeView : ReactiveWindow<MainWindowViewModel>
{
    public CheckCodeView()
    {
        InitializeComponent();
        this.WhenActivated(disposables => { });
    }
}