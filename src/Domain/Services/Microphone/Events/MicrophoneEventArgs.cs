namespace OmniVoice.Domain.Services.Microphone.Events;

public class MicrophoneEventArgs(byte[] buffer, int length) : EventArgs
{
    public readonly int Length = length;
    public readonly byte[] Buffer = buffer;
}
