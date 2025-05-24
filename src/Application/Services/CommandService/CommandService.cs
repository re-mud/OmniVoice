using OmniVoice.Domain.Services.Logging;
using OmniVoice.Domain.Services.SpeechRecognition.Events;
using OmniVoice.Application.Command.CommandRecognition;
using OmniVoice.Application.Services.CommandService.States;
using OmniVoice.Application.Services.SpeechRecognition;

namespace OmniVoice.Application.Services.CommandService;

public class CommandService
{
    public CommandRecognition CommandRecognition { get; }
    public SpeechRecognitionService SpeechRecognitionService { get; }
    public bool IsRunning { get => SpeechRecognitionService.IsRunning; }

    private ILogger _logger;

    public CommandService(
        CommandRecognition commandRecognition,
        SpeechRecognitionService speechRecognitionService,
        ILogger logger)
    {
        ArgumentNullException.ThrowIfNull(commandRecognition, nameof(commandRecognition));
        ArgumentNullException.ThrowIfNull(speechRecognitionService, nameof(speechRecognitionService));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        CommandRecognition = commandRecognition;
        SpeechRecognitionService = speechRecognitionService;
        _logger = logger;

        SpeechRecognitionService.RecognitionCompleted += SpeechRecognitionService_RecognitionCompleted;
    }

    private void SpeechRecognitionService_RecognitionCompleted(object? sender, RecognitionEventArgs e)
    {
#if DEBUG
        _logger.Debug($"hears: \"{e.Text}\"");
#endif
        CommandRecognitionResult[] results = CommandRecognition.Recognize(e.Text);

        if (results.Length == 0) return;

        CommandRecognitionResult best = results[0];
        foreach (var result in results)
        {
            if (result.Probability > best.Probability)
            {
                best = result;
            }
        }

#if DEBUG
        _logger.Debug($"recognized Key:\"{best.Key}\" Command:\"{best.Command.GetCommand()}\"");
#endif

        best.Execute();
    }

    /// <summary>
    /// Start recognizing commands and executing them
    /// </summary>
    public void Start()
    {
        SpeechRecognitionService.Start();
    }

    /// <summary>
    /// Stop recognizing
    /// </summary>
    public void Stop()
    {
        SpeechRecognitionService.Stop();
    }
}
