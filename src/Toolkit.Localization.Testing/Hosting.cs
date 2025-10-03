using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Toolkit.Localization.Testing;
using Toolkit.Localizations.Infrastructure.Extensions;

internal sealed partial class Hosting
{
    #region [Host] Property - Хост приложения

    /// <summary> Хост приложения </summary>
    private static IHost? _host;
    /// <summary> Хост приложения </summary>
    public static IHost Host => _host ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

    #endregion

    #region [Services] Property - Сервисы

    /// <summary> Сервисы </summary>
    public static IServiceProvider Services => Host.Services;

    #endregion

    #region [Provider] Property - Провайдер сервисов

    /// <summary> Провайдер сервисов </summary>
    public static IServiceProvider Provider => Host.Services;

    #endregion

    #region [StartHost] Method - Запускаает хост приложения

    /// <summary> Запускаает хост приложения </summary>
    public static async void StartHost()
    {
        IHost host = Host;

        await host.StartAsync().ConfigureAwait(false);
    }

    #endregion

    #region [StopHost] Method - Останавливает хост приложения

    /// <summary> Останавливает хост приложения </summary>
    public static async void StopHost()
    {
        IHost host = Host;
        await host.StopAsync().ConfigureAwait(false);
        host.Dispose();
    }

    #endregion
}
internal sealed partial class Hosting
{
    private static string Location => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
        ?? throw new InvalidOperationException("Unable to determine path to root content folder. ");

    private static IHostBuilder CreateHostBuilder(string[] args)
        => Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
            .UseContentRoot(Location)
            .ConfigureAppConfiguration((host, cfg) => cfg
                .SetBasePath(Location)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            )
            .ConfigureServices(ConfigureServices)
            .ConfigureServices(ConfigureInternalServices)
        ;

    private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        => services
            .AddInheritLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            })
        ;
    private static void ConfigureInternalServices(HostBuilderContext host, IServiceCollection services)
        => services.AddServices();
}

