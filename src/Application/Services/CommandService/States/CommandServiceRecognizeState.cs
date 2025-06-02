using OmniVoice.Domain.Services.SpeechRecognition.Events;
using OmniVoice.Domain.Services.CommandService.States;
using OmniVoice.Domain.Services.CommandService;
using OmniVoice.Domain.Command.Models;
using OmniVoice.Application.Models;

namespace OmniVoice.Application.Services.CommandService.States;

public class CommandServiceRecognizeState : ICommandServiceState
{
    private IdentifiedCommand[] _commands;

    public CommandServiceRecognizeState(IdentifiedCommand[] commands)
    {
        _commands = commands;
    }

    public string? OnRecognitionCompleted(ICommandServiceContext context, RecognitionEventArgs e)
    {
#if DEBUG
        context.Logger.Debug($"hears: \"{e.Text}\"");
#endif
        CommandRecognitionResult[] results = context.CommandRecognition.Recognize(e.Text);

        if (results.Length == 0) return null;

        CommandRecognitionResult best = results[0];
        foreach (var result in results)
        {
            if (result.Probability > best.Probability)
            {
                best = result;
            }
        }

#if DEBUG
        context.Logger.Debug($"recognized Key:\"{best.Key}\" Command:\"{best.Command.GetCommandString()}\"");
#endif

        best.Execute();
        return null;
    }

    public string? Start(ICommandServiceContext context)
    {
        context.SpeechRecognitionService.Start();

        return null;
    }

    public string? Stop(ICommandServiceContext context)
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
