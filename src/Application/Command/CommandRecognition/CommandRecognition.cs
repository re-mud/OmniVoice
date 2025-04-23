using OmniVoice.Domain.Analyzers;
using OmniVoice.Domain.Command.Interfaces;
using OmniVoice.Domain.Command.Models;

namespace OmniVoice.Application.Command.CommandRecognition;

public class CommandRecognition
{
    private Dictionary<string, ICommand>? _commands;
    private Dictionary<string, IParser>? _parsers;

    public void SetCommands(Dictionary<string, ICommand> commands)
    {
        _commands = commands;
    }

    public void SetParsers(Dictionary<string, IParser> parsers)
    {
        _parsers = parsers;
    }

    /// <summary>
    /// commands and parsers are required before execution
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    public CommandRecognitionResult[] Recognize(string text)
    {
        ArgumentNullException.ThrowIfNull(_parsers);
        ArgumentNullException.ThrowIfNull(_commands);

        List<CommandRecognitionResult> results = new List<CommandRecognitionResult>();

        foreach (var kvp in _commands)
        {
            ICommand command = kvp.Value;
            CommandParseResult result = command.Parse(text);

            if (result.Probability < 0.8) continue;

            // params for command
            List<object> extractedParams = new();

            foreach (var param in command.RequiredParams)
            {
                if (!_parsers.TryGetValue(param, out IParser? parser)) break;

                object extractData = parser.ExtractData(result.RemainingText);

                if (extractData == null) break;

                extractedParams.Add(param);
            }

            if (extractedParams.Count != command.RequiredParams.Length) continue;

            results.Add(new CommandRecognitionResult(
                kvp.Key, command, result.Probability, extractedParams.ToArray()));
        }

        return results.ToArray();
    }
}