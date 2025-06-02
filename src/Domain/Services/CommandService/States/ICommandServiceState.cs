using OmniVoice.Domain.Services.SpeechRecognition.Events;

namespace OmniVoice.Domain.Services.CommandService.States;

public interface ICommandServiceState
{
    /// <returns>
    /// Returns the identifier of the new state, or null if no change is required.
    /// </returns>
    string? Start(ICommandServiceContext context);

    /// <returns>
    /// Returns the identifier of the new state, or null if no change is required.
    /// </returns>
    string? Stop(ICommandServiceContext context);

    /// <returns>
    /// Returns the identifier of the new state, or null if no change is required.
    /// </returns>
    string? OnRecognitionCompleted(ICommandServiceContext context, RecognitionEventArgs e);

    /// <summary>
    /// Enters the state with arguments.
    /// </summary>
    void Enter(ICommandServiceContext context, object[]? args);

    /// <summary>
    /// Resets the state.
    /// </summary>
    void Exit(ICommandServiceContext context);
}