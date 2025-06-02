namespace OmniVoice.Domain.Models;

public class StateTransition(string? stateId = null, object[]? args = null)
{
    public readonly string? StateId = stateId;
    public readonly object[]? Args = args;
}