using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OmniVoice.Application.Command.CommandRecognition;
using OmniVoice.Application.Models;
using OmniVoice.Application.Services.CommandService;
using OmniVoice.Application.Services.SpeechRecognition;
using OmniVoice.Domain.Analyzers;
using OmniVoice.Domain.Command;
using OmniVoice.Domain.Models;

namespace OmniVoice.Application.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<SpeechRecognitionService>();
        services.AddTransient<CommandRecognition>(sp => CreateCommandRecognition(sp, configuration));

        services.AddSingleton<CommandService>();

        return services;
    }

    private static CommandRecognition CreateCommandRecognition(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        return new(
            (IIdentifiedEntity<ICommand>[])serviceProvider.GetServices<IdentifiedCommand>(),
            (IIdentifiedEntity<IParser>[])serviceProvider.GetServices<IdentifiedParser>()
        );
    }
}
