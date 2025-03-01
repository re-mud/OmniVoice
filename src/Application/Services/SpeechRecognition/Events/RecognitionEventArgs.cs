namespace OmniVoice.Application.Services.SpeechRecognition.Events;

public class RecognitionEventArgs(string text) : EventArgs
{
    public readonly string Text = text;
}
