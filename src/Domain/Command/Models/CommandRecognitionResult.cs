﻿using OmniVoice.Domain.Models;

namespace OmniVoice.Domain.Command.Models;

public class CommandRecognitionResult(
    string key, 
    ICommand command, 
    float probability, 
    object[] extractedParams)
{
    public readonly string Key = key;
    public readonly ICommand Command = command;
    public readonly float Probability = probability;
    public readonly object[] ExtractedParams = extractedParams;

    public StateTransition Execute() => Command.Execute(ExtractedParams);
}