using Vosk;

using OmniVoice.Domain.SpeechRecognition.Interfaces;
using OmniVoice.Domain.SpeechRecognition.Enums;
using OmniVoice.Infrastructure.Services.Options;

using Microsoft.Extensions.Options;

namespace OmniVoice.Infrastructure.Services;

public class VoskSpeechRecognition : ISpeechRecognition
{
    private VoskRecognizer _recognizer;
    private Model _model;
    private VoskSpeechRecognitionOptions _options;

    private string _lastPartialResult = string.Empty;
    private int _dataProcessed = 0;

    public VoskSpeechRecognition(IOptions<VoskSpeechRecognitionOptions> options, Model model)
    {
        ArgumentNullException.ThrowIfNull(model);
        if (options.Value.SampleRate < 8000) throw new ArgumentException("Invalid sampleRate");

        _options = options.Value;
        _model = model;
        _recognizer = new VoskRecognizer(_model, _options.SampleRate);
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
        string result = _recognizer.PartialResult();

        return result.Substring(14, result.Length - 17);
    }

    public void Reset()
    {
        if (_dataProcessed >= _options.ThresholdSec * _options.SampleRate)
        {
            _recognizer.Dispose();
            _recognizer = new VoskRecognizer(_model, _options.SampleRate);
            _dataProcessed = 0;
        }

        _recognizer.Reset();
    }

    public string Result()
    {
        string result = _recognizer.Result();

        return result.Substring(14, result.Length - 17);
    }
}