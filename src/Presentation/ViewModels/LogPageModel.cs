using OmniVoice.Domain.Services;
using OmniVoice.Domain.Services.Events;
using OmniVoice.Presentation.Common.Views;
using OmniVoice.Presentation.ViewModelContracts;

namespace OmniVoice.Presentation.ViewModels;

public class LogPageModel : ViewModelBase, ILogPageModel
{
    private const int MaxLogLength = 100;

    private List<string> _logsArray = new();
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
        _logsArray.Add(e.Message);

        if (_logsArray.Count > MaxLogLength)
        {
            _logsArray.RemoveAt(0);
        }

        Logs = string.Join("", _logsArray);
    }
}
