using OmniVoice.Application.Command.CommandRecognition;
using OmniVoice.Application.Services.SpeechRecognition;

namespace OmniVoice.Application.Services.CommandService;

public class CommandService
{
    private CommandRecognition _commandRecognition;
    private SpeechRecognitionService _speechRecognitionService;

    public CommandService(
        CommandRecognition commandRecognition,
        SpeechRecognitionService speechRecognitionService)
    {
        _commandRecognition = commandRecognition;
        _speechRecognitionService = speechRecognitionService;

        _speechRecognitionService.RecognitionCompleted += SpeechRecognitionService_RecognitionCompleted;
    }

    private void SpeechRecognitionService_RecognitionCompleted(object? sender, SpeechRecognition.Events.RecognitionEventArgs e)
    {
        CommandRecognitionResult[] results = _commandRecognition.Recognize(e.Text);

        if (results.Length == 0) return;

        CommandRecognitionResult best = results[0];
        foreach (var result in results)
        {
            if (result.Probability > best.Probability)
            {
                best = result;
            }
        }

        best.Execute();
    }

    public void Start()
    {
        _speechRecognitionService.Start();
    }
}
