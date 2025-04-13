using Microsoft.Extensions.Options;

using OmniVoice.Domain.Services;
using OmniVoice.Infrastructure.Services.Logging.Options;

namespace OmniVoice.Infrastructure.Services.Logging;

public class Logger : ILogger
{
    private readonly object _lockFile = new();
    private readonly LoggerOptions _options;

    public Logger(IOptions<LoggerOptions> options)
    {
        _options = options.Value;

        File.WriteAllText(_options.LogPath, string.Empty);
    }

    private void Log(string type, string message)
    {
        lock (_lockFile)
        {
            File.AppendAllText(_options.LogPath, string.Format(_options.LogFormat,
                DateTime.Now.ToString(_options.DateFormat),
                type,
                message
            ));
        }
    }

    public void Info(string message) => Log("Info", message);
    public void Debug(string message) => Log("Debug", message);
    public void Warn(string message) => Log("Warn", message);
    public void Error(string message) => Log("Error", message);
    public void Fatal(string message) => Log("Fatal", message);
}
