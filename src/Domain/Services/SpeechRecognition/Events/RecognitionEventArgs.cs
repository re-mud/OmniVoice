namespace OmniVoice.Domain.Services.SpeechRecognition.Events;

public class RecognitionEventArgs(string text) : EventArgs
{
    public readonly string Text = text;
}
