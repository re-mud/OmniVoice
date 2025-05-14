using OmniVoice.Domain.Services.Enums;

namespace OmniVoice.Domain.Services.Events;

public class LogEventArgs(string message, string text, LogLevel level, DateTime timeStamp) : EventArgs
{
    /// <summary>
    /// formatted log
    /// </summary>
    public readonly string Message = message;
    public readonly string Text = text;
    public readonly LogLevel Level = level;
    public readonly DateTime Timestamp = timeStamp;
}