using OmniVoice.Domain.Microphone.Events;

namespace OmniVoice.Domain.Microphone.Interfaces;

public interface IMicrophone
{
    public event EventHandler<MicrophoneEventArgs> DataAvailable;
    public int DeviceNumber { get; set; }
    public void Start();
    public void Stop();
}
