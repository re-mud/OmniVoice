using OmniVoice.Domain.Services;
using OmniVoice.Domain.Services.Events;
using OmniVoice.Presentation.Common;
using OmniVoice.Presentation.ViewModelContracts;

using System.Text;

namespace OmniVoice.Presentation.ViewModels;

public class LogPageModel : ViewModelBase, ILogPageModel
{
    private const int MaxLogLength = 100;

    private StringBuilder _logsBuilder = new();
    private ILogger _logger;

    private string _logs = string.Empty;
    public string Logs
    {
        get => _logs;
        set
        {
            _logs = value;
            OnPropertyChanged(nameof(Logs));
        }
    }

    public LogPageModel(ILogger logger)
    {
        _logger = logger;

        _logger.LogEvent += Logger_LogEvent;
    }

    private void Logger_LogEvent(object? sender, LogEventArgs e)
    {
        _logsBuilder.Append(e.Message);

        if (_logsBuilder.Length > MaxLogLength)
        {
            _logsBuilder.Remove(0, 1);
        }

        Logs = _logsBuilder.ToString();
    }
}
