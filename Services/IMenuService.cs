using System;
using System.Threading.Tasks;
using AvaloniaTerminal.Models;
using ReactiveUI;

namespace AvaloniaTerminal.Services;

public interface IMenuService
{
    public Task<MenuResult> GetViewCheckCode(TimeSpan timeSpan);
    //Task<Action> GoyToInfoView(IScreen screen, IRoutableViewModel viewModel);
}