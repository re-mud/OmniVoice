using OmniVoice.Domain.Command.Models;

namespace OmniVoice.Domain.Command;

public interface ICommand
{
    /// <summary>
    /// list of required parsers
    /// </summary>
    string[] RequiredParams { get; }
    CommandParseResult Parse(string text);
    /// <summary>
    /// execute command
    /// </summary>
    /// <param name="args">data from parsers</param>
    CommandExecuteResult Execute(object[] args);
    /// <summary>
    /// string display of the command, used for hints
    /// </summary>
    string GetCommandString();
}