﻿using System.Text;

using OmniVoice.Domain.Command;
using OmniVoice.Domain.Command.Models;
using OmniVoice.Domain.Models;

namespace OmniVoice.Extension.Command;

public class TokenCommand : ICommand
{
    public string[] RequiredParams
    {
        get;
        init;
    }
    protected readonly string[] Tokens;
    private readonly int _tokensLength;
    private readonly Func<object[], StateTransition>? _executeFunc;

    /// <param name="tokens">Words, their parts or combinations of words.</param>
    /// <param name="requiredParams">Required types params.</param>
    public TokenCommand(
        string[] tokens,
        string[] requiredParams,
        Func<object[], StateTransition>? executeFunc = null)
    {
        RequiredParams = requiredParams;
        Tokens = tokens;
        _tokensLength = Tokens.Sum(x => x.Length);
        _executeFunc = executeFunc;

        Array.Sort(Tokens, (a, b) => b.Length.CompareTo(a.Length));
    }

    public StateTransition Execute(object[] args) => _executeFunc?.Invoke(args) ?? new();

    public CommandParseResult Parse(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return new CommandParseResult(0, text);
        }
        if (Tokens == null || Tokens.Length == 0)
        {
            throw new ArgumentNullException("Tokens");
        }

        int count = 0;
        StringBuilder resultText = new StringBuilder(text);

        foreach (var token in Tokens)
        {
            if (resultText.ToString().Contains(token))
            {
                count += token.Length;
                resultText = resultText.Replace(token, string.Empty);
            }
        }

        ;

        return new CommandParseResult(
            count / _tokensLength,
            string.Join(' ', resultText.ToString()
                                       .Split(' ', StringSplitOptions.RemoveEmptyEntries)));
    }

    public string GetCommandString()
    {
        return string.Join(' ', Tokens);
    }
}