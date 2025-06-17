using OmniVoice.Domain.Services.SpeechRecognition;
using OmniVoice.Domain.Services.SpeechSynthesizer;
using OmniVoice.Domain.Services.Logging;
using OmniVoice.Domain.Command.Interfaces;

namespace OmniVoice.Domain.Services.CommandService;

public interface ICommandServiceContext
{
    ISpeechRecognitionService SpeechRecognitionService { get; }
    ICommandRecognition CommandRecognition { get; }
    ISpeechSynthesizer SpeechSynthesizer { get; }
    ILogger Logger { get; }
}
