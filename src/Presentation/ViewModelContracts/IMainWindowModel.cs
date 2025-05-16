using OmniVoice.Presentation.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

namespace OmniVoice.Presentation.ViewModelContracts;

public interface IMainWindowModel : INotifyPropertyChanged
{
    ObservableCollection<MenuButtonModel> MenuButtonModels { get; }
    Page CurrentPage { get; set; }
}
