using Microsoft.Extensions.DependencyInjection;

using OmniVoice.Presentation.Views;
using OmniVoice.Application.Configuration;
using OmniVoice.Infrastructure.Configuration;

namespace OmniVoice.Presentation;

public static class Program
{
    [STAThread]
    static void Main()
    {
        IServiceProvider service = new ServiceCollection()
            .AddApplicationServices()
            .AddInfrastructureServices()
            .BuildServiceProvider();

        ApplicationConfiguration.Initialize();
        System.Windows.Forms.Application.Run(new Form1());
    }
}