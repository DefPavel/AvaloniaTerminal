using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaTerminal.ViewModels;

namespace AvaloniaTerminal.Views;

public partial class StructurAllView : ReactiveUserControl<StructuralAllViewModel>
{
    public StructurAllView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}