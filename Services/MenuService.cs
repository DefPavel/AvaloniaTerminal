using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using AvaloniaTerminal.Models;
using AvaloniaTerminal.ViewModels;
using AvaloniaTerminal.Views;
using ReactiveUI;

namespace AvaloniaTerminal.Services;

public sealed class MenuService : IMenuService
{

    /*public async Task<Action> GoyToInfoView(IScreen screen, IRoutableViewModel viewModel)
    {
        return await screen.Router.Navigate.Execute(viewModel);
    }
    */

    public async Task<MenuResult> GetViewCheckCode(TimeSpan timeSpan)
    {
        CheckCodeViewModel viewModel = new(timeSpan);
        CheckCodeView view = new() { DataContext = viewModel };
        
        var mainWindow = Application.Current?.ApplicationLifetime 
            is IClassicDesktopStyleApplicationLifetime desktop 
            ? desktop.MainWindow 
            : null;

        await view.ShowDialog(mainWindow);

        if (viewModel.Code != null)
            return new MenuResult
            {
                Status = viewModel.Status,
                Pin = viewModel.Code,
                SecretPin = viewModel.Code.Length is > 0 and > 2
                    ? string.Concat("●●●●", viewModel.Code.AsSpan(viewModel.Code.Length - 2, 2))
                    : string.Empty
            };
        
        return new MenuResult
        {
            Status = false,
            Pin = string.Empty,
            SecretPin = string.Empty
        };
    }
}