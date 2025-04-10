using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OmniVoice.Presentation.Views;

namespace OmniVoice.Presentation.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<MainWindow>();

        return services;
    }
}
