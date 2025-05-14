using System.ComponentModel;

namespace OmniVoice.Presentation.ViewModelContracts;

public interface ILogPageModel : INotifyPropertyChanged
{
    string Logs { get; }
}
