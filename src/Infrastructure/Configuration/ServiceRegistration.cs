using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OmniVoice.Domain.Microphone.Interfaces;
using OmniVoice.Domain.Services;
using OmniVoice.Domain.SpeechRecognition.Interfaces;
using OmniVoice.Infrastructure.Services;
using OmniVoice.Infrastructure.Services.Logging;
using OmniVoice.Infrastructure.Services.Logging.Options;

namespace OmniVoice.Infrastructure.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<LoggerOptions>(configuration.GetSection(nameof(LoggerOptions)));

        services.AddSingleton<ILogger, Logger>();

        services.AddTransient<IMicrophone, NAudioMicrophone>();
        services.AddTransient<ISpeechRecognition, VoskSpeechRecognition>();

        return services;
    }
}
