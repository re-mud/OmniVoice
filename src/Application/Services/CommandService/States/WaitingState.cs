using OmniVoice.Domain.Services.SpeechRecognition.Events;
using OmniVoice.Domain.Services.CommandService.States;
using OmniVoice.Domain.Services.CommandService;
using OmniVoice.Domain.Command.Models;
using OmniVoice.Domain.Command;
using OmniVoice.Domain.Models;

namespace OmniVoice.Application.Services.CommandService.States;

public class WaitingState : ICommandServiceState
{
    private IIdentifiedEntity<ICommand>[] _commands;

    public WaitingState(IIdentifiedEntity<ICommand>[] commands)
    {
        _commands = commands;
    }

    public StateTransition? OnRecognitionCompleted(ICommandServiceContext context, RecognitionEventArgs e)
    {
        CommandRecognitionResult[] results = context.CommandRecognition.Recognize(e.Text);

        if (results.Length == 0) return null;

        return new("Recognize");
    }

    public StateTransition? Start(ICommandServiceContext context)
    {
        context.SpeechRecognitionService.Start();

        return null;
    }

    public StateTransition? Stop(ICommandServiceContext context)
    {
        context.SpeechRecognitionService.Stop();

        return null;
    }

    public void Enter(ICommandServiceContext context, object[]? args = null)
    {
        context.CommandRecognition.SetCommands(_commands);
    }

    public void Exit(ICommandServiceContext context)
    {
        context.CommandRecognition.SetCommands([]);
    }
}
