using System;
using ReactiveUI;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace AvaloniaTerminal.ViewModels;

public class TimetableViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment => nameof(TimetableViewModel);
    public IScreen HostScreen { get; }
    
    private string? _currentAdres = "http://jmu.api.lgpu.org/storage/eios/educationalPrograms/15/Учебный план_ПП_ЛОМО 2021-2022_2022-08-11.pdf";

   public string? CurrentAddress
   {
       get => _currentAdres;
       set =>  this.RaiseAndSetIfChanged(ref _currentAdres, value);
   }
   
   
   #region Команды
   public ReactiveCommand<Unit, IRoutableViewModel> GetBack { get; }

   #endregion
   public TimetableViewModel(IScreen screen)
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