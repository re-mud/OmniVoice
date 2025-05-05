using Microsoft.Extensions.Options;

using OmniVoice.Domain.Services;
using OmniVoice.Domain.Services.Enums;
using OmniVoice.Domain.Services.Events;
using OmniVoice.Infrastructure.Services.Logging.Options;

namespace OmniVoice.Infrastructure.Services.Logging;

public class Logger : ILogger
{
    private readonly object _lockFile = new();
    private readonly LoggerOptions _options;

    public event EventHandler<LogEventArgs> LogEvent;

    public Logger(IOptions<LoggerOptions> options)
    {
        _options = options.Value;

        string? directoryName = Path.GetDirectoryName(_options.LogPath);
        if (directoryName != null)
        {
            Directory.CreateDirectory(directoryName);
        }
        File.WriteAllText(_options.LogPath, string.Empty); 
    }

    private void Log(LogLevel level, string message)
    {
        lock (_lockFile)
        {
            try
            {
                DateTime dateTime = DateTime.Now;

                LogEvent?.Invoke(this, new LogEventArgs(message, level, dateTime));

                File.AppendAllText(_options.LogPath, string.Format(_options.LogFormat,
                    dateTime.ToString(_options.DateFormat),
                    level.ToString(),
                    message
                ));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public void Info(string message) => Log(LogLevel.Info, message);
    public void Debug(string message) => Log(LogLevel.Debug, message);
    public void Warn(string message) => Log(LogLevel.Warn, message);
    public void Error(string message) => Log(LogLevel.Error, message);
    public void Fatal(string message) => Log(LogLevel.Fatal, message);
}
