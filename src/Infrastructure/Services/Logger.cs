using OmniVoice.Domain.Services;

namespace OmniVoice.Infrastructure.Services;

public class Logger : ILogger
{
    private readonly object _lockFile = new();
    private readonly string _filePath;

    public Logger(string filePath)
    {
        _filePath = filePath;

        File.WriteAllText(filePath, string.Empty);
    }

    private void Log(string type, string message)
    {
        lock (_lockFile)
        {
            File.AppendAllText(_filePath, $"[{type}] [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}\n");
        }
    }

    public void Info(string message) => Log("Info", message);
    public void Debug(string message) => Log("Debug", message);
    public void Warn(string message) => Log("Warn", message);
    public void Error(string message) => Log("Error", message);
    public void Fatal(string message) => Log("Fatal", message);
}
