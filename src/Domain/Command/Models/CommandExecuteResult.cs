namespace OmniVoice.Domain.Command.Models;

public class CommandExecuteResult(string? newState = null, object[]? newStateArgs = null)
{
    public readonly string? NewState = newState;
    public readonly object[]? NewStateArgs = newStateArgs;
}