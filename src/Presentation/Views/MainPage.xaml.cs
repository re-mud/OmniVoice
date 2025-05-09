using OmniVoice.Presentation.ViewModelContracts;
using System.Windows.Controls;

namespace OmniVoice.Presentation.Views;

public partial class MainPage : Page
{
    public MainPage(IMainPageModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
