using System.ComponentModel;
using System.Windows.Input;

namespace OmniVoice.Presentation.ViewModelContracts;

public interface IMainPageModel : INotifyPropertyChanged
{
    ICommand ToggleMicrophoneCommand { get; }
    string Version { get; }
}
