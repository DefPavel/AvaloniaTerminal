﻿using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace AvaloniaTerminal.ViewModels.Faculty;

public class SturcurFizViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment => nameof(SturcurFizViewModel);

    public IScreen HostScreen { get; }

    #region Команды
    public ReactiveCommand<Unit, IRoutableViewModel> GetBack { get; }

    #endregion

    public SturcurFizViewModel(IScreen screen)
    {
        HostScreen = screen;

        GetBack = ReactiveCommand.CreateFromTask(async _ =>
            await HostScreen.Router.NavigateAndReset.Execute(new MenuViewModel(HostScreen)));

        this.WhenActivated((CompositeDisposable disposables) =>
        {
            GC.Collect();
        });
    }
}