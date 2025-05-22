using System.Speech.Synthesis;

using OmniVoice.Domain.SpeechSynthesizer;

namespace OmniVoice.Infrastructure.Services;

public class WindowsSpeechSynthesizer : ISpeechSynthesizer
{
    private SpeechSynthesizer _speechSynthesizer;

    public WindowsSpeechSynthesizer(SpeechSynthesizer speechSynthesizer)
    {
        _speechSynthesizer = speechSynthesizer;
    }

    public void SelectVoice(string name)
    {
        _speechSynthesizer.SelectVoice(name);
    }

    public void SetRate(float rate)
    {
        if (rate < 0 || rate > 1) throw new ArgumentOutOfRangeException(nameof(rate));

        _speechSynthesizer.Rate = (int)((rate - 0.5f) * 20);
    }

    public void SetVolume(float volume)
    {
        if (volume < 0 || volume > 1) throw new ArgumentOutOfRangeException(nameof(volume));

        _speechSynthesizer.Volume = (int)(volume * 100);
    }

    public void Speak(string text)
    {
        _speechSynthesizer.Speak(text);
    }

    public void SpeakAsync(string text)
    {
        _speechSynthesizer.SpeakAsync(text);
    }
}