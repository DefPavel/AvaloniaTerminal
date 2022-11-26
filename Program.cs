using Avalonia;
using Avalonia.ReactiveUI;
using System;
using Avalonia.Dialogs;
using Avalonia.Svg;

namespace AvaloniaTerminal
{
    internal static class Program
    {
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);
        private static AppBuilder BuildAvaloniaApp() =>
            AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .With(
                new X11PlatformOptions
                {
                    EnableMultiTouch = true,
                })
                .With(new Win32PlatformOptions
                {
                    EnableMultitouch = true,
                })
                .UseSkia()
                .UseReactiveUI()
                .UseManagedSystemDialogs();
    }
}