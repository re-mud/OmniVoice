﻿using NAudio.Wave;
using OmniVoice.Domain.Services.Microphone;
using OmniVoice.Domain.Services.Microphone.Events;

namespace OmniVoice.Infrastructure.Services;

public class NAudioMicrophone : IMicrophone
{
    public int SampleRate { get => _waveInEvent.WaveFormat.SampleRate; }
    public int Channels { get => _waveInEvent.WaveFormat.Channels; }
    public int Bits { get => _waveInEvent.WaveFormat.BitsPerSample; }

    public event EventHandler<MicrophoneEventArgs>? DataAvailable;

    private WaveInEvent _waveInEvent;

    public int DeviceNumber
    {
        get => _waveInEvent.DeviceNumber;
        set => _waveInEvent.DeviceNumber = value;
    }

    public NAudioMicrophone(WaveInEvent waveInEvent)
    {
        ArgumentNullException.ThrowIfNull(waveInEvent);

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
        if (e.Buffer.Length != e.BytesRecorded) return;

        DataAvailable?.Invoke(this, new MicrophoneEventArgs(
            e.Buffer,
            e.BytesRecorded
        ));
    }
}