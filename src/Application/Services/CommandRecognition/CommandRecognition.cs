using OmniVoice.Application.Services.CommandRecognition;
using OmniVoice.Domain.Analyzers;
using OmniVoice.Domain.Command.Interfaces;
using OmniVoice.Domain.Command.Models;

namespace OnniVoice.Application.Services.CommandRecognition;

public class CommandRecognition
{
    private Dictionary<string, ICommand>? _commands;
    private Dictionary<string, IParser>? _parsers;

    /// <exception cref="ArgumentNullException"></exception>
    public void SetCommands(Dictionary<string, ICommand> commands)
    {
        ArgumentNullException.ThrowIfNull(commands);

        _commands = commands;
    }

    /// <exception cref="ArgumentNullException"></exception>
    public void SetParsers(Dictionary<string, IParser> parsers)
    {
        ArgumentNullException.ThrowIfNull(parsers);

        _parsers = parsers;
    }

    /// <exception cref="ArgumentNullException"></exception>
    public CommandRecognitionResult[] Recognize(string text)
    {
        if (_commands == null) throw new ArgumentNullException("Commands is not set");
        if (_parsers == null) throw new ArgumentNullException("Parcers is not set");

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
                if (!_parsers.TryGetValue(param, out IParser parser)) break;

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