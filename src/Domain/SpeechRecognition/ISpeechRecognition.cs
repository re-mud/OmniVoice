using OmniVoice.Domain.SpeechRecognition.Enums;

namespace OmniVoice.Domain.SpeechRecognition;

public interface ISpeechRecognition
{
    public SpeechRecognitionState Accept(byte[] buffer, int length);
    public string PartialResult();
    public string Result();
    public void Reset();
}