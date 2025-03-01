using Microsoft.Extensions.DependencyInjection;

using OmniVoice.Application.Services.SpeechRecognition;

namespace OmniVoice.Application.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddSingleton<SpeechRecognitionService>();

        return services;
    }
}
