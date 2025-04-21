namespace OmniVoice.Infrastructure.Services.Logging.Options;

public class LoggerOptions
{
    public string LogPath { get; } = "logs.txt";
    /// <summary>
    /// {0} - date
    /// {1} - log level
    /// {2} - message
    /// </summary>
    public string LogFormat { get; } = "[{0}] [{1}] {2}\n";
    public string DateFormat { get; } = "yyyy-MM-dd HH:mm:ss";
}
