namespace OmniVoice.Domain.Services.SpeechSynthesizer;

public interface ISpeechSynthesizer
{
    void Speak(string text);
    void SpeakAsync(string text);
    /// <param name="rate">[0.0f, 1.0f]</param>
    void SetRate(float rate);
    /// <param name="volume">[0.0f, 1.0f]</param>
    void SetVolume(float volume);
    void SelectVoice(string name);
}
