using OmniVoice.Domain.SpeechRecognition.Interfaces;
using OmniVoice.Domain.SpeechRecognition.Enums;
using OmniVoice.Domain.Microphone.Interfaces;
using OmniVoice.Domain.Microphone.Events;
using OmniVoice.Application.Services.SpeechRecognition.Events;

namespace OmniVoice.Application.Services.SpeechRecognition;

public class SpeechRecognitionService
{
    private ISpeechRecognition _speechRecognition;
    private IMicrophone _microphone;

    public event EventHandler<RecognitionEventArgs>? RecognitionCompleted;
    public event EventHandler<RecognitionEventArgs>? PartialRecognitionAvaible;

    public int DeviceNumber
    {
        get => _microphone.DeviceNumber;
        set => _microphone.DeviceNumber = value;
    }

    public SpeechRecognitionService(ISpeechRecognition speechRecognition, IMicrophone microphone)
    {
        ArgumentNullException.ThrowIfNull(speechRecognition, nameof(speechRecognition));
        ArgumentNullException.ThrowIfNull(microphone, nameof(microphone));

        _speechRecognition = speechRecognition;
        _microphone = microphone;

        _microphone.DataAvailable += Microphone_DataAvailable;
    }

    public void SetSpeechRecognition(ISpeechRecognition speechRecognition)
    {
        ArgumentNullException.ThrowIfNull(speechRecognition, nameof(speechRecognition));

        Stop();

        _speechRecognition = speechRecognition;
    }

    public void SetMicrophone(IMicrophone microphone)
    {
        ArgumentNullException.ThrowIfNull(microphone, nameof(microphone));

        Stop();

        _microphone.DataAvailable -= Microphone_DataAvailable;

        microphone.DataAvailable += Microphone_DataAvailable;

        _microphone = microphone;
    }

    public void Start()
    {
        _microphone.Start();
    }

    public void Stop()
    {
        _microphone.Stop();

        _speechRecognition.Reset();
    }

    private void Microphone_DataAvailable(object? sender, MicrophoneEventArgs e)
    {
        SpeechRecognitionState state = _speechRecognition.Accept(e.Buffer, e.Length);

        switch (state)
        {
            case SpeechRecognitionState.Partial:
                PartialRecognitionAvaible?.Invoke(null, new RecognitionEventArgs(
                    _speechRecognition.PartialResult()
                ));
                break;

            case SpeechRecognitionState.Full:
                RecognitionCompleted?.Invoke(null, new RecognitionEventArgs(
                    _speechRecognition.Result()
                ));
                _speechRecognition.Reset();
                break;

            case SpeechRecognitionState.None:
                break;
        }
    }
}
