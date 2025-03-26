namespace OmniVoice.Domain.Command.Models;

public class CommandParseResult(float probability, string remainingText)
{
    public readonly float Probability = probability;
    public readonly string RemainingText = remainingText;
}
