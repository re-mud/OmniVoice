using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OmniVoice.Presentation.Common;

public class ViewModelBase
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}
