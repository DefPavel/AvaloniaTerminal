
using System;
using System.Reactive.Disposables;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using AvaloniaTerminal.ViewModels;
using DynamicData.Binding;
using ReactiveUI;

namespace AvaloniaTerminal.Views;

public partial class CheckCodeView : ReactiveWindow<CheckCodeViewModel>
{
    
    private readonly DispatcherTimer _disTimer = new();

    private void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        Close();
    }
    
    private ProgressBar CheckProgress => this.FindControl<ProgressBar>("ProgressBar");
    public CheckCodeView()
    {
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.WhenAnyObservable(x => x.ViewModel!.Check.IsExecuting)
                .BindTo(this, x => x.CheckProgress.IsVisible)
                .DisposeWith(disposables);

            this.WhenValueChanged(x => x.ViewModel!.TimeSpans).Subscribe(span =>
                {
                    _disTimer.Interval = span;
                    _disTimer.Tick += DispatcherTimer_Tick;
                    _disTimer.Start();
                })
                .DisposeWith(disposables);
            
            Disposable.Create(() => { _disTimer.Stop(); }).DisposeWith(disposables);
            /*this.WhenPropertyChanged(x => x.ViewModel!.IsClose != false)
                .Subscribe(_ => TextCode.Text = "Время истекло!")
                .DisposeWith(disposables);
            */
            /*this.WhenPropertyChanged(x => x.ViewModel!.IsClose)
                .Subscribe(_ => Close())
                .DisposeWith(disposables);
            */
        });
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}