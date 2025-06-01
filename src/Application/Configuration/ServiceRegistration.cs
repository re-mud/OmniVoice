using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OmniVoice.Application.Command.CommandRecognition;
using OmniVoice.Application.Models;
using OmniVoice.Application.Services.CommandService;
using OmniVoice.Application.Services.CommandService.States;
using OmniVoice.Application.Services.SpeechRecognition;
using OmniVoice.Domain.Services.Logging;

namespace OmniVoice.Application.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddSingleton<CommandService>(sp => CreateCommandService(sp, configuration));

        AddIdentifiedStates(services, configuration);
        AddServices(services, configuration);

        return services;
    }

    private static void AddIdentifiedStates(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient(sp => new IdentifiedState("Recognize",
            new CommandServiceRecognizeState(sp.GetServices<IdentifiedCommand>().ToArray())));
    }

    private static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<SpeechRecognitionService>();
        services.AddTransient<CommandRecognition>(sp => CreateCommandRecognition(sp, configuration));
    }

    private static CommandService CreateCommandService(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        CommandService service = new(
            serviceProvider.GetRequiredService<CommandRecognition>(),
            serviceProvider.GetRequiredService<SpeechRecognitionService>(),
            serviceProvider.GetServices<IdentifiedState>().ToArray(),
            serviceProvider.GetRequiredService<ILogger>());

        service.SetState("Recognize");

        return service;
    }

    private static CommandRecognition CreateCommandRecognition(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        return new(
            [],
            serviceProvider.GetServices<IdentifiedParser>().ToArray());
    }
}
