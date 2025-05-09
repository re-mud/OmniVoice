using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OmniVoice.Domain.Analyzers;
using OmniVoice.Domain.Command;
using OmniVoice.Domain.Models;
using OmniVoice.Application.Models;
using OmniVoice.Application.Services.CommandService;
using OmniVoice.Presentation.ViewModelContracts;
using OmniVoice.Presentation.ViewModels;
using OmniVoice.Presentation.Views;

namespace OmniVoice.Presentation.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<MainWindow>();
        services.AddTransient<IMainWindowModel>(sp => CreateMainWindowModel(sp, configuration));

        return services;
    }

    private static MainWindowModel CreateMainWindowModel(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        CommandService commandService = serviceProvider.GetRequiredService<CommandService>();

        commandService.CommandRecognition.SetParsers(
            (IIdentifiedEntity<IParser>[])serviceProvider.GetServices<IdentifiedParser>());
        commandService.CommandRecognition.SetCommands(
            (IIdentifiedEntity<ICommand>[])serviceProvider.GetServices<IdentifiedCommand>());

        return new MainWindowModel(commandService);
    }
}
