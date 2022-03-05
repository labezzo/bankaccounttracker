namespace BAT.AvaloniaFrontend
{
    using BAT.AvaloniaFrontend.Views;
    using Microsoft.Extensions.DependencyInjection;

    internal static class RegisterDependencies
    {
        internal static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<MainWindow>();
        }
    }
}
