using System.Reflection;
using AvaloniaTerminal.Services;
using Splat;

namespace AvaloniaTerminal;

public class Bootstrapper
{
    public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        // регистрация сервисов
        services.Register<IMenuService>(() => new MenuService());
        services.Register<IChekingService>(() => new ChekingService());
        
        // информация о приложении
        services.RegisterLazySingleton<IApplicationInfo>(() => new ApplicationInfo(Assembly.GetExecutingAssembly()));
        

    }
}