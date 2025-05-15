using OmniVoice.Domain.SpeechRecognition.Enums;
using OmniVoice.Domain.Microphone.Events;
using OmniVoice.Application.Services.SpeechRecognition.Events;
using OmniVoice.Domain.SpeechRecognition;
using OmniVoice.Domain.Microphone;

namespace OmniVoice.Application.Services.SpeechRecognition;

public class SpeechRecognitionService
{
    private ISpeechRecognition _speechRecognition;
    private IMicrophone _microphone;

    private byte[]? _lastBuffer; 
    private double? _cachedVolume;

    public event EventHandler<RecognitionEventArgs>? RecognitionCompleted;
    public event EventHandler<RecognitionEventArgs>? PartialRecognitionAvaible;
    public bool IsRunning { get; private set; } = false;

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

    public double GetVolume()
    {
        if (_lastBuffer == null || _lastBuffer.Length == 0 || !IsRunning) return 0;
        if (_cachedVolume.HasValue) return _cachedVolume.Value;

        double sum = 0; 

        for (int i = 0; i < _lastBuffer.Length; i += 2)
        {
            short sample = (short)((_lastBuffer[i + 1] << 8) | _lastBuffer[i]);
            sum += sample * sample;
        }

        _cachedVolume = Math.Sqrt(sum / _lastBuffer.Length * 2);
        return _cachedVolume.Value;
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
        IsRunning = true;

        _microphone.Start();
    }

    public void Stop()
    {
        IsRunning = false;

        _microphone.Stop();

        _speechRecognition.Reset();
    }

    private void Microphone_DataAvailable(object? sender, MicrophoneEventArgs e)
    {
        _lastBuffer = e.Buffer;
        _cachedVolume = null;
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
