using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaTerminal.Views;

public partial class IprView : UserControl
{
    public IprView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}