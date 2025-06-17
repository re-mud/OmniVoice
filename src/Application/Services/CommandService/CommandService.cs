using OmniVoice.Domain.Services.Logging;
using OmniVoice.Domain.Command.Models;
using OmniVoice.Domain.Services.SpeechRecognition.Events;
using OmniVoice.Domain.Services.CommandService;
using OmniVoice.Domain.Services.SpeechSynthesizer;
using OmniVoice.Domain.Services.SpeechRecognition;
using OmniVoice.Domain.Services.CommandService.States;
using OmniVoice.Application.Command.CommandRecognition;
using OmniVoice.Application.Services.SpeechRecognition;
using OmniVoice.Application.Models;
using OmniVoice.Domain.Models;
using OmniVoice.Domain.Command.Interfaces;

namespace OmniVoice.Application.Services.CommandService;

public class CommandService : ICommandServiceContext
{
    public ISpeechRecognitionService SpeechRecognitionService { get; }
    public ICommandRecognition CommandRecognition { get; }
    public ISpeechSynthesizer SpeechSynthesizer { get; }
    public ILogger Logger { get; }
    public bool IsRunning { get => SpeechRecognitionService.IsRunning; }
    public ICommandServiceState? State { get; private set; }

    private IIdentifiedEntity<ICommandServiceState>[] _states;

    public CommandService(
        ICommandRecognition commandRecognition,
        ISpeechRecognitionService speechRecognitionService,
        ISpeechSynthesizer speechSynthesizer,
        IIdentifiedEntity<ICommandServiceState>[] states,
        ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(commandRecognition, nameof(commandRecognition));
        ArgumentNullException.ThrowIfNull(speechRecognitionService, nameof(speechRecognitionService));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        SpeechSynthesizer = speechSynthesizer;
        CommandRecognition = commandRecognition;
        SpeechRecognitionService = speechRecognitionService;
        Logger = logger;
        _states = states;

        SpeechRecognitionService.RecognitionCompleted += SpeechRecognitionService_RecognitionCompleted;
    }

    private void SpeechRecognitionService_RecognitionCompleted(object? sender, RecognitionEventArgs e)
    {
#if DEBUG
        Logger.Debug($"Hears: \"{e.Text}\"");
#endif
        StateTransition? transition = State?.OnRecognitionCompleted(this, e);

        if (transition != null)
        {
            ApplyTransition(transition);
        }
    }

    /// <summary>
    /// Start recognizing commands and executing them.
    /// </summary>
    public void Start()
    {
#if DEBUG
        Logger.Debug("Start recognizing");
#endif
        StateTransition? transition = State?.Start(this);

        if (transition != null)
        {
            ApplyTransition(transition);
        }
    }

    /// <summary>
    /// Stop recognizing.
    /// </summary>
    public void Stop()
    {
#if DEBUG
        Logger.Debug("Stop recognizing");
#endif
        StateTransition? transition = State?.Stop(this);

        if (transition != null)
        {
            ApplyTransition(transition);
        }
    }

    public void ApplyTransition(StateTransition transition)
    {
        IIdentifiedEntity<ICommandServiceState>? newIdentifiedState = _states.FirstOrDefault(identifiedState => identifiedState.Id == transition.StateId);

        if (newIdentifiedState != null)
        {
#if DEBUG
            Logger.Debug($"Change state to \"{newIdentifiedState.Id}\"");
#endif
            State?.Exit(this);
            State = newIdentifiedState.Value;
            State.Enter(this, transition.Args);
        }
        else
        {
            Logger.Error($"No found state with id:\"{transition.StateId}\"");
        }
    }
}
