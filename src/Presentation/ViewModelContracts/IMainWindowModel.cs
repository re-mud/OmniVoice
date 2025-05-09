using System.ComponentModel;
using System.Windows.Controls;

namespace OmniVoice.Presentation.ViewModelContracts;

public interface IMainWindowModel : INotifyPropertyChanged
{
    Page CurrentPage { get; set; }
}
