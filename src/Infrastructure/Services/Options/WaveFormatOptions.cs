namespace OmniVoice.Infrastructure.Services.Options;

public class WaveFormatOptions
{
    public int Bits { set; get; } = 16;
    public int Channels { set; get; } = 1;
    public int SampleRate { set; get; } = 16000;
}