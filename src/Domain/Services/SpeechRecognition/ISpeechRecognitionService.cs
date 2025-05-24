using OmniVoice.Domain.Services.Microphone;
using OmniVoice.Domain.Services.SpeechRecognition.Events;

namespace OmniVoice.Domain.Services.SpeechRecognition;

public interface ISpeechRecognitionService
{
    event EventHandler<RecognitionEventArgs>? RecognitionCompleted;
    event EventHandler<RecognitionEventArgs>? PartialRecognitionAvailable;

    bool IsRunning { get; }
    int DeviceNumber { get; set; }

    double GetVolume();
    void SetSpeechRecognition(ISpeechRecognition speechRecognition);
    void SetMicrophone(IMicrophone microphone);
    void Start();
    void Stop();
}
