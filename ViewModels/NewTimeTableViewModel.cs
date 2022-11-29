using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Threading;
using AvaloniaTerminal.Models;
using ReactiveUI;

namespace AvaloniaTerminal.ViewModels;

public class NewTimeTableViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment => nameof(TimetableViewModel);
    public IScreen HostScreen { get; }

    /*private const string PathPdf =
    @"C:\Program Files\Adobe\Acrobat DC\Acrobat\Acrobat.exe";*/
    /*private string? _currentAdres = "http://jmu.api.lgpu.org/storage/eios/educationalPrograms/15/Учебный план_ПП_ЛОМО 2021-2022_2022-08-11.pdf";

    public string? CurrentAddress
    {
        get => _currentAdres;
        set =>  this.RaiseAndSetIfChanged(ref _currentAdres, value);
    }
    */
    
    private readonly DispatcherTimer _disTimer = new();
    
    private static readonly string? PathPdf = ConfigurationManager.AppSettings["pathPDF"];
    
    private static readonly string? NumberFaculty = ConfigurationManager.AppSettings["numberFaculty"];

    private ObservableCollection<ListFiles>? _pdfFile = new();

    public ObservableCollection<ListFiles>? PdfFiles
    {
        get => _pdfFile;
        set =>  this.RaiseAndSetIfChanged(ref _pdfFile, value);
    }

    private ListFiles? _selectedPdf = new();

    public ListFiles? SelectedPdf
    {
        get => _selectedPdf;
        set =>  this.RaiseAndSetIfChanged(ref _selectedPdf, value);
    }

    #region Команды
    public ReactiveCommand<Unit, IRoutableViewModel> GetBack { get; }
    
    public ReactiveCommand<Unit, Unit> OpenFile { get; }

    #endregion

    #region Логика

    private void Opening()
    {
        if (SelectedPdf == null) return;
        
         _ = Process.Start("\"" + PathPdf + "\"",  "\"" +SelectedPdf.OriginalName + "\"");

    }

    #endregion
    public NewTimeTableViewModel(IScreen screen)
    {
        HostScreen = screen;

        GetBack = ReactiveCommand.CreateFromTask(async _ =>
            await HostScreen.Router.Navigate.Execute(new MenuViewModel(HostScreen)));

        OpenFile = ReactiveCommand.Create(Opening);

        this.WhenActivated((CompositeDisposable disposables) =>
        {
            var files = Directory.GetFiles("Z:\\data-avalonia\\pdfFiles\\" + NumberFaculty, "*.pdf");

            _disTimer.Interval = TimeSpan.FromMinutes(5);
            _disTimer.Tick += DispatcherTimer_Tick;
            _disTimer.Start();
            
            Disposable.Create(() => { _disTimer.Stop(); }).DisposeWith(disposables);

            if (files.Length > 0)
            {
                foreach (var item in files)
                {
                    var splitter = item.Split("\\");
                    PdfFiles?.Add(new ListFiles()
                    {
                        Name = splitter[^1],
                        OriginalName = item
                    });
                }     
            }
           
            
            
            GC.Collect();
        });
    }

    private async void DispatcherTimer_Tick(object? sender, EventArgs e)
    {
        await HostScreen.Router.NavigateAndReset.Execute(new CarouselViewModel(HostScreen));
    }
}