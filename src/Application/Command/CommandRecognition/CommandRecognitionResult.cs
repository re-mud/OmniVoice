using OmniVoice.Domain.Command;

namespace OmniVoice.Application.Command.CommandRecognition;

public class CommandRecognitionResult(string key, ICommand command, float probability, object[] extractedParams)
{
    public readonly string Key = key;
    public readonly ICommand Command = command;
    public readonly float Probability = probability;
    public readonly object[] ExtractedParams = extractedParams;

    public void Execute()
    {
        Command.Execute(ExtractedParams);
    }
}