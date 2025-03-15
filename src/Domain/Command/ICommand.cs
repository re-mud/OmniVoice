namespace OmniVoice.Domain.Command;

public interface ICommand
{
    /// <summary>
    /// list of required parsers
    /// </summary>
    string[] RequiredParams { get; }
    /// <summary>
    /// probability calculation
    /// </summary>
    /// <returns>[0.0f, 1.0f]</returns>
    float Probability(string text);
    /// <summary>
    /// execute command
    /// </summary>
    /// <param name="args">data from parsers</param>
    void Execute(object[] args);
    /// <summary>
    /// string display of the command, used for hints
    /// </summary>
    string GetCommand();
}