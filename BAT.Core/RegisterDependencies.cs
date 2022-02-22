namespace BAT.Core
{
    using BAT.Core.Services;
    using Microsoft.Extensions.DependencyInjection;

    public static class RegisterDependencies
    {
        public static void Register(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IFileSystemService, FileSystemService>();
            serviceCollection.AddTransient<IJsonService, JsonService>();
        }
    }
}
