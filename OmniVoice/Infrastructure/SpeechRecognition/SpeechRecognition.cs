using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vosk;

using OmniVoice.Domain.SpeechRecognition;
using OmniVoice.Domain.SpeechRecognition.Enums;

namespace OmniVoice.Infrastructure.SpeechRecognition
{
    internal class SpeechRecognition : ISpeechRecognition
    {
        private VoskRecognizer _recognizer;
        private Model _model;
        private int _thresholdSec;
        private int _sampleRate;

        private string _lastPartialResult = string.Empty;
        private int _dataProcessed = 0;

        public SpeechRecognition(Model model, int sampleRate, int thresholdSec)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (sampleRate < 8000) throw new ArgumentException("Invalid sampleRate");

            _model = model;
            _thresholdSec = thresholdSec;
            _sampleRate = sampleRate;
            _recognizer = new VoskRecognizer(_model, _sampleRate);
        }

        /// <exception cref="ArgumentException"></exception>
        public SpeechRecognitionState Accept(byte[] buffer, int length)
        {
            if (length != buffer.Length) throw new ArgumentException("Invalid buffer length");

            _dataProcessed += length;

            if (_recognizer.AcceptWaveform(buffer, length))
            {
                return SpeechRecognitionState.Full;
            }

            string currentPartial = PartialResult();
            if (currentPartial != _lastPartialResult)
            {
                _lastPartialResult = currentPartial;
                return SpeechRecognitionState.Partial;
            }

            return SpeechRecognitionState.None;
        }

        public string PartialResult()
        {
            return _recognizer.PartialResult();
        }

        public void Reset()
        {
            if (_dataProcessed >= _thresholdSec * _sampleRate)
            {
                _recognizer.Dispose();
                _recognizer = new VoskRecognizer(_model, _sampleRate);
                _dataProcessed = 0;
            }

            _recognizer.Reset();
        }

        public string Result()
        {
            return _recognizer.Result();
        }
    }
}