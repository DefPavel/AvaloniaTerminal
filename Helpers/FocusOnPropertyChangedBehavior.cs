using Avalonia;
using Avalonia.Controls;
using Avalonia.Xaml.Interactivity;

namespace AvaloniaTerminal.Helpers;

public class FocusOnPropertyChangedBehavior : Behavior<Control>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        if (AssociatedObject != null) AssociatedObject.PropertyChanged += FocuseControl;
    }
    protected override void OnDetaching()
    {
        base.OnDetaching();
        if (AssociatedObject != null) AssociatedObject.PropertyChanged -= FocuseControl;
    }
    private void FocuseControl(object sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (AssociatedObject is { IsFocused: false }) AssociatedObject.Focus();
    }
}