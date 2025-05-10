using OmniVoice.Domain.Microphone.Events;

namespace OmniVoice.Domain.Microphone;

public interface IMicrophone
{
    public int SampleRate { get; }
    public int Channels { get; }
    public int Bits { get; }

    public event EventHandler<MicrophoneEventArgs> DataAvailable;
    public int DeviceNumber { get; set; }
    public void Start();
    public void Stop();
}
