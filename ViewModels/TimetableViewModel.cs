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
    
    private string? _currentAdres = "file:///Z:/data-avalonia/pdfFiles/1/Распис_ПП_СПО_ Тифлопедагогика СУрдопедагогика ноябрь.pdf";

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
           await HostScreen.Router.Navigate.Execute(new MenuViewModel(HostScreen)));
        
       this.WhenActivated((CompositeDisposable disposables) =>
       {
           GC.Collect();
       });
   }
   
}