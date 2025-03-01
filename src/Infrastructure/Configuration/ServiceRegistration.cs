using Microsoft.Extensions.DependencyInjection;

using OmniVoice.Domain.Microphone.Interfaces;
using OmniVoice.Domain.SpeechRecognition.Interfaces;
using OmniVoice.Infrastructure.Services;

namespace OmniVoice.Infrastructure.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IMicrophone, NAudioMicrophone>();
        services.AddTransient<ISpeechRecognition, VoskSpeechRecognition>();

        return services;
    }
}
