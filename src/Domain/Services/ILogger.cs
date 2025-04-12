namespace OmniVoice.Domain.Services;

public interface ILogger
{
    void Info(string message);
    void Debug(string message);
    void Warn(string message);
    void Error(string message);
    void Fatal(string message);
}
