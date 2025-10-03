using Microsoft.Extensions.DependencyInjection;

namespace Toolkit.Localization.Testing
{
    internal static class Registrator
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
            => services
                .AddSingleton<Service>()
                .AddSingleton<Service2>()
                .AddSingleton(typeof(Service<>))
            ;
    }
}
