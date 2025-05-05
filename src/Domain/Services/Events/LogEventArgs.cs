using OmniVoice.Domain.Services.Enums;

namespace OmniVoice.Domain.Services.Events;

public class LogEventArgs(string message, LogLevel level, DateTime timeStamp) : EventArgs
{
    public readonly string Message = message;
    public readonly LogLevel Level = level;
    public readonly DateTime Timestamp = timeStamp;
}