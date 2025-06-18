using System.ComponentModel;
using System.Windows.Input;

namespace OmniVoice.Presentation.ViewModelContracts;

public interface ILogPageModel : INotifyPropertyChanged
{
    string Logs { get; }
    ICommand ClearCommand { get; }
}
