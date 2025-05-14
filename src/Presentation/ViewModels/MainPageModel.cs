using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Threading;

using OmniVoice.Application.Services.CommandService;
using OmniVoice.Presentation.Animations;
using OmniVoice.Presentation.Common;
using OmniVoice.Presentation.Models;
using OmniVoice.Presentation.ViewModelContracts;

namespace OmniVoice.Presentation.ViewModels;

public class MainPageModel : ViewModelBase, IMainPageModel
{
    public ICommand ToggleMicrophoneCommand => new RelayCommand(ToggleMicrophone);
    public string Version { get; private set; } = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0.0";


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
}
