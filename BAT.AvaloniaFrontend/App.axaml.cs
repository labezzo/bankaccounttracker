namespace BAT.AvaloniaFrontend
{
    using Avalonia;
    using Avalonia.Controls.ApplicationLifetimes;
    using Avalonia.Markup.Xaml;
    using BAT.AvaloniaFrontend.Views;
    using log4net;
    using log4net.Config;
    using Microsoft.Extensions.DependencyInjection;
    using System.IO;
    using System.Reflection;

    public class App : Application
    {
        private static readonly string Log4NetConfigName = "log4net.config";

        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            BAT.Core.RegisterDependencies.Register(serviceCollection);
            BAT.AvaloniaFrontend.RegisterDependencies.Register(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();

            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo(Log4NetConfigName));
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
