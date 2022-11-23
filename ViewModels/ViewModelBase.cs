﻿using ReactiveUI;

namespace AvaloniaTerminal.ViewModels
{
    public class ViewModelBase : ReactiveObject, IActivatableViewModel
    {
        public ViewModelActivator Activator { get; } = new();

        protected ObservableAsPropertyHelper<bool>? isBusy;
        public bool IsBusy => isBusy is { Value: true };
    }
}