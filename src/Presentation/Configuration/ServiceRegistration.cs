using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OmniVoice.Presentation.ViewModelContracts;
using OmniVoice.Presentation.ViewModels;
using OmniVoice.Presentation.Views;
using OmniVoice.Presentation.Animations;
using OmniVoice.Domain.Services;
using OmniVoice.Presentation.Models;
using OmniVoice.Application.Common.IdentifiedEntities;

using System.Windows.Threading;

namespace OmniVoice.Presentation.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        AddServices(services, configuration);
        AddViews(services, configuration);
        AddMenuButtons(services, configuration);

        return services;
    }

    private static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<VolumeAnimationManager>();
        services.AddTransient<DispatcherTimer>();
    }

    private static void AddMenuButtons(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient(sp => new MenuButtonModel("Main", "MainPage", "#A4B5BE", "#C5D9E3"));
        services.AddTransient(sp => new MenuButtonModel("Logs", "LogPage", "#A4B5BE", "#C5D9E3"));
        services.AddTransient(sp => new MenuButtonModel("Plugins", "PluginsPage", "#A4B5BE", "#C5D9E3"));
        services.AddTransient(sp => new MenuButtonModel("Settings", "SettingsPage", "#A4B5BE", "#C5D9E3"));
    }

    private static void AddViews(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IMainWindowModel>(sp => CreateMainWindowModel(sp, configuration));
        services.AddTransient<MainWindow>();

        services.AddTransient<IMainPageModel, MainPageModel>();
        services.AddTransient(sp => new IdentifiedPage("MainPage", new MainPage(sp.GetRequiredService<IMainPageModel>())));
        services.AddTransient<ILogPageModel, LogPageModel>();
        services.AddTransient(sp => new IdentifiedPage("LogPage", new LogPage(sp.GetRequiredService<ILogPageModel>())));
    }

    private static MainWindowModel CreateMainWindowModel(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        return new MainWindowModel(
            serviceProvider.GetServices<IdentifiedPage>().ToArray(),
            serviceProvider.GetServices<MenuButtonModel>().ToArray(),
            serviceProvider.GetRequiredService<ILogger>());
    }
}
