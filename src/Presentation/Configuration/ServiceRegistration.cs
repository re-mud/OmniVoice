using Microsoft.Extensions.DependencyInjection;
using OmniVoice.Presentation.Views;

namespace OmniVoice.Presentation.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services)
    {
        services.AddTransient<MainWindow>();

        return services;
    }
}
