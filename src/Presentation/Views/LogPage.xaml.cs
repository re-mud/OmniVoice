using OmniVoice.Presentation.ViewModelContracts;

using System.Windows.Controls;

namespace OmniVoice.Presentation.Views;

public partial class LogPage : Page
{
    public LogPage(ILogPageModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
