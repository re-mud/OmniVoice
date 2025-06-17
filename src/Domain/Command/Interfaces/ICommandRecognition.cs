using OmniVoice.Domain.Analyzers;
using OmniVoice.Domain.Command.Models;
using OmniVoice.Domain.Models;

namespace OmniVoice.Domain.Command.Interfaces;

public interface ICommandRecognition
{
    void SetCommands(IIdentifiedEntity<ICommand>[] commands);
    void SetParsers(IIdentifiedEntity<IParser>[] parsers);
    /// <summary>
    /// commands and parsers are required before execution
    /// </summary>
    /// <exception cref="ArgumentNullException"></exception>
    CommandRecognitionResult[] Recognize(string text);
}
