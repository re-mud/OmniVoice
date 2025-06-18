using OmniVoice.Presentation.ViewModelContracts;

using System.Windows.Controls;

namespace OmniVoice.Presentation.Views;

public partial class ExtensionsPage : Page
{
    public ExtensionsPage(IExtensionsPageModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
