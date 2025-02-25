using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OmniVoice.API.Controllers.SpeechRecognition.EventArgs;
using OmniVoice.Domain.Microphone;
using OmniVoice.Domain.SpeechRecognition;

namespace OmniVoice.API.Controllers.SpeechRecognition
{
    internal class SpeechRecognitionController
    {
        private ISpeechRecognition _speechRecognition;
        private IMicrophone _microphone;

        private string _partialResult = string.Empty;

        event EventHandler<RecognitionEventArgs>? RecognitionCompleted;
        event EventHandler<RecognitionEventArgs>? PartialRecognitionAvaible;

        public SpeechRecognitionController(ISpeechRecognition speechRecognition, IMicrophone microphone)
        {
            _speechRecognition = speechRecognition;
            _microphone = microphone;

            _microphone.DataAvailable += Microphone_DataAvailable;
        }

        public void SetInputDevice(int deviceId)
        {
            _microphone.SetInputDevice(deviceId);
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

        private void Microphone_DataAvailable(object? sender, Domain.Microphone.EventArgs.MicrophoneEventArgs e)
        {
            if (_speechRecognition.Accept(e.Buffer, e.Length))
            {
                RecognitionCompleted?.Invoke(null, new RecognitionEventArgs(
                    _speechRecognition.Result()
                ));
                _speechRecognition.Reset();
            }
            else if (_speechRecognition.PartialResult() != _partialResult)
            {
                _partialResult = _speechRecognition.PartialResult();

                PartialRecognitionAvaible?.Invoke(null, new RecognitionEventArgs(_partialResult));
            }
        }
    }
}
