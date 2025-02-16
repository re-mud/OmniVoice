using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniVoice.Domain.Microphone.EventArgs
{
    internal class MicrophoneEventArgs : System.EventArgs
    {
        public readonly int Length;
        public readonly byte[] Buffer;

        public MicrophoneEventArgs(byte[] buffer, int length)
        {
            Buffer = buffer;
            Length = length;
        }
    }
}
