using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaTerminal.ViewModels;
using ReactiveUI;

namespace AvaloniaTerminal.Views;

public partial class TimetableView : ReactiveUserControl<TimetableViewModel>
{
    public TimetableView()
    {
        InitializeComponent();
        
        this.WhenActivated(disposables => { });
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}