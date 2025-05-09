using OmniVoice.Application.Services.CommandService;
using OmniVoice.Presentation.ViewModelContracts;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;

namespace OmniVoice.Presentation.ViewModels;

public class MainWindowModel : IMainWindowModel
{
    public event PropertyChangedEventHandler? PropertyChanged;

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

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}