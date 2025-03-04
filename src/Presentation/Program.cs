using Microsoft.Extensions.DependencyInjection;

using OmniVoice.Application.Configuration;
using OmniVoice.Infrastructure.Configuration;
using OmniVoice.Presentation.Configuration;

using OmniVoice.Presentation.Views;

namespace OmniVoice.Presentation;

public class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        IServiceCollection services = new ServiceCollection()
            .AddApplicationServices()
            .AddInfrastructureServices()
            .AddPresentationServices();
        IServiceProvider serviceProvider = services.BuildServiceProvider();

        System.Windows.Application app = new System.Windows.Application();
        app.Run(serviceProvider.GetService<MainWindow>());
    }
}
