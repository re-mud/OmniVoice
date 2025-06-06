﻿using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace OmniVoice.Presentation.Common.Views;

public class ModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}
