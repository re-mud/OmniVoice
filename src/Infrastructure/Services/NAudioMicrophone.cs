using NAudio.Wave;
using OmniVoice.Domain.Microphone.Events;
using OmniVoice.Domain.Microphone.Interfaces;

namespace OmniVoice.Infrastructure.Services;

public class NAudioMicrophone : IMicrophone
{
    public event EventHandler<MicrophoneEventArgs>? DataAvailable;

    private WaveInEvent _waveInEvent;

    public int DeviceNumber
    {
        get => _waveInEvent.DeviceNumber;
        set => _waveInEvent.DeviceNumber = value;
    }

    public NAudioMicrophone(WaveInEvent waveInEvent)
    {
        _waveInEvent = waveInEvent;
        _waveInEvent.DataAvailable += WaveInEvent_DataAvailable;
    }

    public void Start()
    {
        _waveInEvent.StartRecording();
    }

    public void Stop()
    {
        _waveInEvent.StopRecording();
    }
    private void WaveInEvent_DataAvailable(object? sender, WaveInEventArgs e)
    {
        DataAvailable?.Invoke(this, new MicrophoneEventArgs(
            e.Buffer,
            e.BytesRecorded
        ));
    }
}