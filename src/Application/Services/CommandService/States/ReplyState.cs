using OmniVoice.Domain.Models;
using OmniVoice.Domain.Services.CommandService;
using OmniVoice.Domain.Services.CommandService.States;
using OmniVoice.Domain.Services.SpeechRecognition.Events;

namespace OmniVoice.Application.Services.CommandService.States;

public class ReplyState : ICommandServiceState
{
    private string _nextState = "Recognize";

    /// <param name="args">
    /// <para>[0] - text to speech(string)</para>
    /// <para>[1] - next state id(string)</para>
    /// </param>
    public void Enter(ICommandServiceContext context, object[]? args)
    {
        if (args != null && args.Length == 2)
        {
            string? text = args[0] as string;
            string? nextState = args[1] as string;

            if (!string.IsNullOrEmpty(text)) context.SpeechSynthesizer.Speak(text);
            if (!string.IsNullOrEmpty(nextState)) _nextState = nextState;
        }
    }

    public void Exit(ICommandServiceContext context)
    {

    }

    public StateTransition? OnRecognitionCompleted(ICommandServiceContext context, RecognitionEventArgs e)
    {
        return new(_nextState);
    }

    public StateTransition? Start(ICommandServiceContext context)
    {
        return new(_nextState);
    }

    public StateTransition? Stop(ICommandServiceContext context)
    {
        return new(_nextState);
    }
}
