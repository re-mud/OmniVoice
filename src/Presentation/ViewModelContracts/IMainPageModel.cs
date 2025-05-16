using OmniVoice.Presentation.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace OmniVoice.Presentation.ViewModelContracts;

public interface IMainPageModel : INotifyPropertyChanged
{
    ObservableCollection<EllipseModel> EllipseModels { get; }
    ICommand ToggleMicrophoneCommand { get; }
    string Version { get; }
}
