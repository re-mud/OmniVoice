namespace OmniVoice.Infrastructure.Services.Options;

public class WaveFormatOptions
{
    public int Bits { get; } = 16;
    public int Channels { get; } = 1;
    public int SampleRate { get; } = 16000;
}