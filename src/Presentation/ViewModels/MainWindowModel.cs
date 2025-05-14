using OmniVoice.Domain.Services;
using OmniVoice.Presentation.Common;
using OmniVoice.Presentation.ViewModelContracts;

using System.Windows.Controls;

namespace OmniVoice.Presentation.ViewModels;

public class MainWindowModel : ViewModelBase, IMainWindowModel
{
    public Page[] Pages;
    public Page CurrentPage
    {
        get => _currentPage;
        set
        {
            _currentPage = value;
            OnPropertyChanged(nameof(CurrentPage));
        }
    }

    private Page _currentPage;

    public MainWindowModel(Page[] pages)
    {
        if (pages == null || pages.Length == 0 || pages[0] == null) throw new ArgumentNullException(nameof(pages));

        Pages = pages;

        CurrentPage = Pages[0];
    }
}