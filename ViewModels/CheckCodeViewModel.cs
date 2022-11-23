using System.Reactive;
using System.Reactive.Disposables;
using Avalonia.Controls;
using ReactiveUI;

namespace AvaloniaTerminal.ViewModels;

public sealed class CheckCodeViewModel : ViewModelBase
{

    #region Свойства

    private string? _code;

    public string? Code
    {
        get => _code;
        set =>  this.RaiseAndSetIfChanged(ref _code, value);
    }    

    #endregion

    #region Команды
    
    public ReactiveCommand<object, Unit> Check { get; }
    public ReactiveCommand<object, Unit> Close { get; }
    
    #endregion

    #region Логика

    private void Checking(object win)
    {
        if (win is not Window w) return;

        if (Code == "123456")
            w.Close();
        else Code = "Пошел нах!";
    }

    private void Exit(object win)
    {
        if (win is not Window w) return;
        w.Close();
    }

    #endregion
    
    public CheckCodeViewModel()
    {
        Close = ReactiveCommand.Create<object>(Exit);

        Check = ReactiveCommand.Create<object>(Checking);

        this.WhenActivated((CompositeDisposable disposables) =>
        {

        });
       
    }
}