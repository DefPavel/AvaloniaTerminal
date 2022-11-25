
using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaTerminal.ViewModels;
using ReactiveUI;

namespace AvaloniaTerminal.Views;

public partial class CheckCodeView : ReactiveWindow<CheckCodeViewModel>
{
    private ProgressBar CheckProgress => this.FindControl<ProgressBar>("ProgressBar");
    public CheckCodeView()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.WhenAnyObservable(x => x.ViewModel!.Check.IsExecuting)
                .BindTo(this, x => x.CheckProgress.IsVisible)
                .DisposeWith(disposables);
        });
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

   /* private void InputElement_OnGotFocus(object? sender, GotFocusEventArgs e)
    {
        if (!Osklib.OnScreenKeyboard.IsOpened())
            Osklib.OnScreenKeyboard.Show();
    }
    */
}