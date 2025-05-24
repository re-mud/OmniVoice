using Microsoft.Extensions.Options;
using OmniVoice.Domain.Services.Logging;
using OmniVoice.Domain.Services.Logging.Enums;
using OmniVoice.Domain.Services.Logging.Events;
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

        if (_options.LogPath != "")
        {
            string? directoryName = Path.GetDirectoryName(_options.LogPath);
            if (directoryName != null)
            {
                Directory.CreateDirectory(directoryName);
            }
            File.WriteAllText(_options.LogPath, string.Empty);
        }
    }

    private void Log(LogLevel level, string text)
    {
        lock (_lockFile)
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                string message = string.Format(_options.LogFormat,
                    dateTime.ToString(_options.DateFormat),
                    level.ToString(),
                    text);

                LogEvent?.Invoke(this, new LogEventArgs(message, text, level, dateTime));

                if (_options.LogPath != "")
                {
                    File.AppendAllText(_options.LogPath, message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

    public void Info(string text) => Log(LogLevel.Info, text);
    public void Debug(string text) => Log(LogLevel.Debug, text);
    public void Warn(string text) => Log(LogLevel.Warn, text);
    public void Error(string text) => Log(LogLevel.Error, text);
    public void Fatal(string text) => Log(LogLevel.Fatal, text);
}
