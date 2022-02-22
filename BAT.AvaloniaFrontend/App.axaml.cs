namespace BAT.AvaloniaFrontend
{
    using Avalonia;
    using Avalonia.Controls.ApplicationLifetimes;
    using Avalonia.Markup.Xaml;
    using Microsoft.Extensions.DependencyInjection;

    public class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            BAT.Core.RegisterDependencies.Register(serviceCollection);
            BAT.AvaloniaFrontend.RegisterDependencies.Register(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = _serviceProvider.GetService<MainWindow>();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
