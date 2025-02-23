using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmniVoice.API.Controllers.SpeechRecognition.EventArgs
{
    internal class RecognitionEventArgs : System.EventArgs
    {
        public readonly string Text;

        public RecognitionEventArgs(string text)
        {
            Text = text;
        }
    }
}
