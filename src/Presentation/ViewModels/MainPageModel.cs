using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using OmniVoice.Application.Services.CommandService;
using OmniVoice.Presentation.Common.Command;
using OmniVoice.Presentation.ViewModelContracts;

namespace OmniVoice.Presentation.ViewModels;

public class MainPageModel : IMainPageModel
{
    public ICommand ToggleMicrophoneCommand => new RelayCommand(ToggleMicrophone);
    public string Version { get; private set; } = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0.0";

    public event PropertyChangedEventHandler? PropertyChanged;

    private CommandService _commandService;

    public MainPageModel(CommandService commandService)
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
