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

    private TimeSpan _timeSpan;

    public TimeSpan TimeSpans
    {
        get => _timeSpan;
        set =>  this.RaiseAndSetIfChanged(ref _timeSpan, value);
    }
    
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
        private set => this.RaiseAndSetIfChanged(ref _status, value);
    }  

    #endregion

    #region Команды
    
    public ReactiveCommand<string, Unit> AddNumberCommand { get; }
    public ReactiveCommand<Unit, Unit> ClearCommand { get; }
    public ReactiveCommand<object, Unit> Check { get; }
    public ReactiveCommand<object, Unit> Close { get; }
    
    #endregion

    #region Логика


    private void Clear()
    {
        Code = string.Empty;
    }
    
    private void AddNumber(string value)
    {
        if (Code is "Неверный пин" or "Неверные данные") Code = string.Empty;
        
        if (Code?.Length != 6) Code += value;
    }

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
    
    public CheckCodeViewModel(TimeSpan span) :
        this(
            Locator.Current.GetService<IChekingService>(),
            span
        ){ }
    
    public CheckCodeViewModel(IChekingService chekingService , TimeSpan timeSpan)
    {
        TimeSpans = timeSpan - TimeSpan.FromSeconds(5);
        _chekingService = chekingService;
        // Закрыть окно
        Close = ReactiveCommand.Create<object>(Exit);

        AddNumberCommand = ReactiveCommand.Create<string>(AddNumber);
        ClearCommand = ReactiveCommand.Create(Clear);
        // Проверять только если код не пустой
        var canCheck = this.WhenAnyValue(x => x.Code,
            (code) => !string.IsNullOrWhiteSpace(code));
        // Проверка
        Check = ReactiveCommand.CreateFromTask<object>(Checking, canCheck);
        Check.IsExecuting.ToProperty(this, x => x.IsBusy, out isBusy);
        
      
        
        this.WhenActivated(disposables =>
        {

            Disposable.Create(() => { }).DisposeWith(disposables);
            GC.Collect();
        });
       
    }

}