using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows.Input;
using System.Windows.Threading;

using OmniVoice.Application.Services.CommandService;
using OmniVoice.Presentation.Animations;
using OmniVoice.Presentation.Common.Commands;
using OmniVoice.Presentation.Common.Views;
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

    public ObservableCollection<EllipseModel> EllipseModels { get => _volumeAnimationManager.EllipseModels; }

    private CommandService _commandService;
    private VolumeAnimationManager _volumeAnimationManager;
    private DispatcherTimer _timer;

    public MainPageModel(
        CommandService commandService,
        VolumeAnimationManager volumeAnimationManager,
        DispatcherTimer timer)
    {
        _commandService = commandService;
        _volumeAnimationManager = volumeAnimationManager;
        _timer = timer;

        _timer.Interval = TimeSpan.FromMilliseconds(30);
        _timer.Tick += Timer_Tick;

        _commandService.Start();
        _timer.Start();
    }

    private void Timer_Tick(object? sender, EventArgs e)
    {
        _volumeAnimationManager.Tick((float)_commandService.SpeechRecognitionService.GetVolume());
    }
}
