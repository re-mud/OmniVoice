using System.Windows;

using OmniVoice.Presentation.Views;

namespace OmniVoice.Presentation;

public class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        Application app = new Application();
        app.Run(new MainWindow());
    }
}
