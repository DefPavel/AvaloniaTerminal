using System.Reactive.Disposables;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaTerminal.ViewModels.Faculty;
using ReactiveUI;

namespace AvaloniaTerminal.Views
{
    public partial class ContactIPRView : ReactiveUserControl<ContactIPRViewModel>
    {
        public ContactIPRView()
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
