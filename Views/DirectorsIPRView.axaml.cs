using Avalonia.Controls;
using Avalonia.ReactiveUI;
using AvaloniaTerminal.ViewModels;
using AvaloniaTerminal.ViewModels.Faculty;
using ReactiveUI;
using System.Reactive.Disposables;
using Avalonia.Markup.Xaml;

namespace AvaloniaTerminal.Views
{
    public partial class DirectorsIPRView : ReactiveUserControl<DirectorsIPRViewModel>
    {
        public DirectorsIPRView()
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
}
