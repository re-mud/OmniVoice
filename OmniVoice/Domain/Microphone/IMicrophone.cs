using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OmniVoice.Domain.Microphone.EventArgs;

namespace OmniVoice.Domain.Microphone
{
    internal interface IMicrophone
    {
        public event EventHandler<MicrophoneEventArgs> DataAvailable;
        public void SetInputDevice(int deviceId);
        public void Start();
        public void Stop();
    }
}
