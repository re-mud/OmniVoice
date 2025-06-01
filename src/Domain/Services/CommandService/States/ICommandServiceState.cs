using OmniVoice.Domain.Services.SpeechRecognition.Events;

namespace OmniVoice.Domain.Services.CommandService.States;

public interface ICommandServiceState
{
    /// <returns>
    /// return the identifier of the new state, null if no change is required
    /// </returns>
    string? Start(ICommandServiceContext context);
    /// <returns>
    /// return the identifier of the new state, null if no change is required
    /// </returns>
    string? Stop(ICommandServiceContext context);
    /// <returns>
    /// return the identifier of the new state, null if no change is required
    /// </returns>
    string? OnRecognitionCompleted(ICommandServiceContext context, RecognitionEventArgs e);
    void Enter(ICommandServiceContext context);
    void Exit(ICommandServiceContext context);
}