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
    private ICommand _toggleMicrophoneCommand;
    public ICommand ToggleMicrophoneCommand
    {
        get
        {
            return _toggleMicrophoneCommand ??= new RelayCommand(obj =>
            {
                if (_commandService.IsRunning)
                {
                    _commandService.Stop();
                }
                else
                {
                    _commandService.Start();
                }
            });
        }
    }

    public string Version { get; private set; } = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0.0";


    private CommandService _commandService;

    public MainPageModel(CommandService commandService)
    {
        _commandService = commandService;

        _commandService.Start();
    }
}
