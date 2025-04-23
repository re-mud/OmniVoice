namespace OmniVoice.Infrastructure.Services.Options;

public class VoskSpeechRecognitionOptions
{
    public int ThresholdSec { set; get; } = 120;
    public int SampleRate { set; get; } = 16000;
}
