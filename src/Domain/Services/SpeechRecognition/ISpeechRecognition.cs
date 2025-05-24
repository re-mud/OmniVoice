using OmniVoice.Domain.Services.SpeechRecognition.Enums;

namespace OmniVoice.Domain.Services.SpeechRecognition;

public interface ISpeechRecognition
{
    public SpeechRecognitionState Accept(byte[] buffer, int length);
    public string PartialResult();
    public string Result();
    public void Reset();
}