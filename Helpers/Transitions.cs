using System;
using Avalonia.Animation;

namespace AvaloniaTerminal.Helpers;

public static class Transitions
{
    public static IPageTransition Fade => new CrossFade(TimeSpan.FromMilliseconds(200));

    public static IPageTransition Slide => new PageSlide(TimeSpan.FromMilliseconds(200));
}