using System.Reactive.Disposables;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using AvaloniaTerminal.ViewModels.Faculty;
using ReactiveUI;

namespace AvaloniaTerminal.Views
{
    public partial class ContactIprView : ReactiveUserControl<ContactIPRViewModel>
    {
        public ContactIprView()
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
