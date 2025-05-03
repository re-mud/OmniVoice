using System.ComponentModel;
using System.Windows.Input;

namespace OmniVoice.Presentation.ViewModelContracts;

public interface IMainWindowModel : INotifyPropertyChanged
{
    ICommand ToggleMicrophoneCommand { get; }
}
