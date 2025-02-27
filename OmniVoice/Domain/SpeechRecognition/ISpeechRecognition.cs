using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OmniVoice.Domain.SpeechRecognition.Enums;

namespace OmniVoice.Domain.SpeechRecognition
{
    internal interface ISpeechRecognition
    {
        public SpeechRecognitionState Accept(byte[] buffer, int length);
        public string PartialResult();
        public string Result();
        public void Reset();
    }
}