using OmniVoice.Application.Services.CommandService;
using OmniVoice.Presentation.Common.Command;
using OmniVoice.Presentation.ViewModelContracts;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace OmniVoice.Presentation.ViewModels;

public class MainWindowModel : IMainWindowModel
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public ICommand ToggleMicrophoneCommand => new RelayCommand(ToggleMicrophone);

    private CommandService _commandService;

    public MainWindowModel(CommandService commandService)
    {
        _commandService = commandService;

        _commandService.Start();
    }

    private void ToggleMicrophone()
    {
        if (_commandService.IsRunning)
        {
            _commandService.Stop();
        }
        else
        {
            _commandService.Start();
        }
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        if (PropertyChanged != null)
            PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}