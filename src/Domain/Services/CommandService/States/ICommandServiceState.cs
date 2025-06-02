using OmniVoice.Domain.Services.SpeechRecognition.Events;
using OmniVoice.Domain.Models;

namespace OmniVoice.Domain.Services.CommandService.States;

public interface ICommandServiceState
{
    /// <returns>
    /// Returns the new state transition, or null if no change is required.
    /// </returns>
    StateTransition? Start(ICommandServiceContext context);

    /// <returns>
    /// Returns the new state transition, or null if no change is required.
    /// </returns>
    StateTransition? Stop(ICommandServiceContext context);

    /// <returns>
    /// Returns the new state transition, or null if no change is required.
    /// </returns>
    StateTransition? OnRecognitionCompleted(ICommandServiceContext context, RecognitionEventArgs e);

    /// <summary>
    /// Enters the state with arguments.
    /// </summary>
    void Enter(ICommandServiceContext context, object[]? args);

    /// <summary>
    /// Resets the state.
    /// </summary>
    void Exit(ICommandServiceContext context);
}