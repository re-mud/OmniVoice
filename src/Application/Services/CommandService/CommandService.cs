using OmniVoice.Domain.Services.Logging;
using OmniVoice.Domain.Command.Models;
using OmniVoice.Domain.Services.SpeechRecognition.Events;
using OmniVoice.Domain.Services.CommandService;
using OmniVoice.Domain.Services.SpeechSynthesizer;
using OmniVoice.Domain.Command;
using OmniVoice.Domain.Services.SpeechRecognition;
using OmniVoice.Domain.Services.CommandService.States;
using OmniVoice.Application.Command.CommandRecognition;
using OmniVoice.Application.Services.SpeechRecognition;
using OmniVoice.Application.Models;

namespace OmniVoice.Application.Services.CommandService;

public class CommandService : ICommandServiceContext
{
    public ISpeechRecognitionService SpeechRecognitionService { get; }
    public ICommandRecognition CommandRecognition { get; }
    public ISpeechSynthesizer SpeechSynthesizer { get; }
    public ILogger Logger { get; }
    public bool IsRunning { get => SpeechRecognitionService.IsRunning; }
    public ICommandServiceState? State { get; private set; }

    private IdentifiedState[] _states;

    public CommandService(
        CommandRecognition commandRecognition,
        SpeechRecognitionService speechRecognitionService,
        IdentifiedState[] states,
        ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(commandRecognition, nameof(commandRecognition));
        ArgumentNullException.ThrowIfNull(speechRecognitionService, nameof(speechRecognitionService));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        CommandRecognition = commandRecognition;
        SpeechRecognitionService = speechRecognitionService;
        Logger = logger;
        _states = states;

        SpeechRecognitionService.RecognitionCompleted += SpeechRecognitionService_RecognitionCompleted;
    }

    private void SpeechRecognitionService_RecognitionCompleted(object? sender, RecognitionEventArgs e)
    {
        State?.OnRecognitionCompleted(this, e);
    }

    /// <summary>
    /// Start recognizing commands and executing them
    /// </summary>
    public void Start()
    {
        State?.Start(this);
    }

    /// <summary>
    /// Stop recognizing
    /// </summary>
    public void Stop()
    {
        State?.Stop(this);
    }

    public void SetState(string id)
    {
        IdentifiedState? newIdentifiedState = _states.FirstOrDefault(identifiedState => identifiedState.Id == id);

        if (newIdentifiedState != null)
        {
            State?.Exit(this);
            State = newIdentifiedState.Value;
            State.Enter(this);
        }
        else
        {
            Logger.Fatal($"No found state with id:\"{id}\"");
        }
    }
}
