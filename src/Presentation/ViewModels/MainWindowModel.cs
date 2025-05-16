using OmniVoice.Domain.Models;
using OmniVoice.Domain.Services;
using OmniVoice.Presentation.Common;
using OmniVoice.Presentation.Common.Command;
using OmniVoice.Presentation.Models;
using OmniVoice.Presentation.ViewModelContracts;

using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace OmniVoice.Presentation.ViewModels;

public class MainWindowModel : ViewModelBase, IMainWindowModel
{
    public IIdentifiedEntity<Page>[] Pages;

    private Page _currentPage;
    public Page CurrentPage
    {
        get => _currentPage;
        set
        {
            _currentPage = value;
            OnPropertyChanged(nameof(CurrentPage));
        }
    }

    private ICommand _changePageCommand;
    public ICommand ChangePageCommand
    {
        get
        {
            return _changePageCommand ??= new RelayCommand(obj =>
            {
                string? id = obj as string;
                if (id == null) return;

                ChangePage(id);
            });
        }
    }

    public ObservableCollection<MenuButtonModel> MenuButtonModels { get; }

    private ILogger _logger;

    public MainWindowModel(IIdentifiedEntity<Page>[] pages, MenuButtonModel[] menuButtonModels, ILogger logger)
    {
        if (pages == null || pages.Length == 0 || pages[0] == null) throw new ArgumentNullException(nameof(pages));

        MenuButtonModels = new(menuButtonModels);
        _logger = logger;
        Pages = pages;

        ChangePage(Pages[0].Id);
    }

    private void ChangePage(string id)
    {
        IIdentifiedEntity<Page>? page = Pages.FirstOrDefault(identifiedPage => identifiedPage.Id == id);

        if (page != null)
        {
            SetButtonStates(id);
            CurrentPage = page.Value;
        }
        else
        {
            _logger.Warn($"Page with ID \"{id}\" not found.");
        }
    }

    private void SetButtonStates(string id)
    {
        foreach (var menuButtonModel in MenuButtonModels)
        {
            menuButtonModel.SetState(menuButtonModel.PageId == id);
        }
    }
}