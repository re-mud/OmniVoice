using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OmniVoice.Application.Configuration;
using OmniVoice.Infrastructure.Configuration;
using OmniVoice.Presentation.Configuration;
using OmniVoice.Presentation.Views;

using System.IO;

namespace OmniVoice.Presentation;

public class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true)
            .Build();

        IServiceCollection services = new ServiceCollection()
            .AddApplicationServices(configuration)
            .AddInfrastructureServices(configuration)
            .AddPresentationServices(configuration);

        IServiceProvider serviceProvider = services.BuildServiceProvider();

        System.Windows.Application app = new System.Windows.Application();
        app.Run(serviceProvider.GetService<MainWindow>());
    }
}
