namespace OmniVoice.Infrastructure.Services.Options;

public class VoskSpeechRecognitionOptions
{
    public int ThresholdSec { get; } = 120;
    public int SampleRate { get; } = 16000;
}
