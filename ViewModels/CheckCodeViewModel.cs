using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using Avalonia.Controls;
using AvaloniaTerminal.Services;
using ReactiveUI;
using Splat;

namespace AvaloniaTerminal.ViewModels;

public sealed class CheckCodeViewModel : ViewModelBase
{

    #region Свойства

    private readonly IChekingService _chekingService;
    
    private string? _code;

    public string? Code
    {
        get => _code;
        set =>  this.RaiseAndSetIfChanged(ref _code, value);
    }  
    
    private bool _status;

    public bool Status
    {
        get => _status;
        private set =>  this.RaiseAndSetIfChanged(ref _status, value);
    }  

    #endregion

    #region Команды
    
    public ReactiveCommand<object, Unit> Check { get; }
    public ReactiveCommand<object, Unit> Close { get; }
    
    #endregion

    #region Логика

    private async Task Checking(object win)
    {
        var isNumeric = int.TryParse(Code, out _);
        
        if (isNumeric && win is Window w)
        {
            var result = await _chekingService.GetPin(Code!);
            if (result.Subscribe)
            {
                Status = true;
                w.Close();
            }
            else Code = "Неверный пин";
        }
        else Code = "Неверные данные";
        
    }

    private static void Exit(object win)
    {
        if (win is not Window w) return;
        w.Close();
    }

    #endregion
    
    
    public CheckCodeViewModel() :
        this(Locator.Current.GetService<IChekingService>()){ }
    
    public CheckCodeViewModel(IChekingService chekingService)
    {
        _chekingService = chekingService;
        // Закрыть окно
        Close = ReactiveCommand.Create<object>(Exit);
        // Проверять только если код не пустой
        var canCheck = this.WhenAnyValue(x => x.Code,
            (code) => !string.IsNullOrWhiteSpace(code));
        // Проверка
        Check = ReactiveCommand.CreateFromTask<object>(Checking, canCheck);
        Check.IsExecuting.ToProperty(this, x => x.IsBusy, out isBusy);
        
        this.WhenActivated((CompositeDisposable disposables) =>
        {
            GC.Collect();
        });
       
    }
}