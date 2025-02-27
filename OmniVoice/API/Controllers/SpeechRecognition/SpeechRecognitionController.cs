using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OmniVoice.Domain.Microphone;
using OmniVoice.Domain.SpeechRecognition;
using OmniVoice.Domain.SpeechRecognition.Enums;
using OmniVoice.API.Controllers.SpeechRecognition.EventArgs;

namespace OmniVoice.API.Controllers.SpeechRecognition
{
    internal class SpeechRecognitionController
    {
        private ISpeechRecognition _speechRecognition;
        private IMicrophone _microphone;

        public event EventHandler<RecognitionEventArgs>? RecognitionCompleted;
        public event EventHandler<RecognitionEventArgs>? PartialRecognitionAvaible;

        public int DeviceNumber
        {
            get => _microphone.DeviceNumber;
            set => _microphone.DeviceNumber = value;
        }

        public SpeechRecognitionController(ISpeechRecognition speechRecognition, IMicrophone microphone)
        {
            _speechRecognition = speechRecognition;
            _microphone = microphone;

            _microphone.DataAvailable += Microphone_DataAvailable;
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
}
