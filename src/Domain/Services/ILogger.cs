using OmniVoice.Domain.Services.Events;

namespace OmniVoice.Domain.Services;

public interface ILogger
{
    event EventHandler<LogEventArgs> LogEvent;
    void Info(string text);
    void Debug(string text);
    void Warn(string text);
    void Error(string text);
    void Fatal(string text);
}
