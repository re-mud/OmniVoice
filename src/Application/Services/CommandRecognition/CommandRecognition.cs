using OmniVoice.Domain.Analyzers;
using OmniVoice.Domain.Command.Interfaces;

namespace OnniVoice.Application.Services.CommandRecognition;

public class CommandRecognition
{
    private ICommand[] _commands;
    private IParser[] _parsers;

    public void SetCommands(ICommand[] commands)
    {
        _commands = commands;
    }

    public void SetParcers(IParser[] parsers)
    {
        _parsers = parsers;
    }

    public void Recognize(string text) 
    {

    }
}