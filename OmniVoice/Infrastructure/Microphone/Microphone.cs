using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NAudio.Wave;

using OmniVoice.Domain.Microphone;
using OmniVoice.Domain.Microphone.EventArgs;

namespace OmniVoice.Infrastructure.Microphone
{
    internal class Microphone : IMicrophone
    {
        public event EventHandler<MicrophoneEventArgs>? DataAvailable;

        private WaveInEvent _waveInEvent;

        public int DeviceNumber
        {
            get => _waveInEvent.DeviceNumber;
            set => _waveInEvent.DeviceNumber = value;
        }

        public Microphone(WaveInEvent waveInEvent)
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
}