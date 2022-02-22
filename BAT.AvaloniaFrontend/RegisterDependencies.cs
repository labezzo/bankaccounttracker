namespace BAT.AvaloniaFrontend
{
    using Microsoft.Extensions.DependencyInjection;

    internal class RegisterDependencies
    {
        internal static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<MainWindow>();
        }
    }
}
