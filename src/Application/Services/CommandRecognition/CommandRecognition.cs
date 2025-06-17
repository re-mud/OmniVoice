using OmniVoice.Domain.Analyzers;
using OmniVoice.Domain.Command.Interfaces;
using OmniVoice.Domain.Command.Models;
using OmniVoice.Domain.Models;

namespace OmniVoice.Application.Services.CommandRecognition;

public class CommandRecognition : ICommandRecognition
{
    private IIdentifiedEntity<ICommand>[]? _commands;
    private IIdentifiedEntity<IParser>[]? _parsers;

    public CommandRecognition() { }

    public CommandRecognition(IIdentifiedEntity<ICommand>[] commands, IIdentifiedEntity<IParser>[] parsers)
    {
        SetCommands(commands);
        SetParsers(parsers);
    }

    public void SetCommands(IIdentifiedEntity<ICommand>[] commands)
    {
        _commands = commands;
    }

    public void SetParsers(IIdentifiedEntity<IParser>[] parsers)
    {
        _parsers = parsers;
    }

    public CommandRecognitionResult[] Recognize(string text)
    {
        ArgumentNullException.ThrowIfNull(_parsers);
        ArgumentNullException.ThrowIfNull(_commands);

        List<CommandRecognitionResult> results = new List<CommandRecognitionResult>();

        foreach (var identifiedCommand in _commands)
        {
            ICommand command = identifiedCommand.Value;
            CommandParseResult result = command.Parse(text);

            if (result.Probability < 0.8) continue;

            // params for command
            List<object> extractedParams = new();

            foreach (var param in command.RequiredParams)
            {
                IParser? parser = _parsers.FirstOrDefault(identifiedParser => identifiedParser.Id == param)?.Value;

                if (parser == null) continue;

                object extractData = parser.ExtractData(result.RemainingText);

                if (extractData == null) break;

                extractedParams.Add(param);
            }

            if (extractedParams.Count != command.RequiredParams.Length) continue;

            results.Add(new CommandRecognitionResult(
                identifiedCommand.Id, command, result.Probability, extractedParams.ToArray()));
        }

        return results.ToArray();
    }
}