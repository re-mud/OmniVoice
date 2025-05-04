using OmniVoice.Domain.Command;
using OmniVoice.Domain.Command.Models;

using System.Text;

namespace OmniVoice.Application.Common.Command;

public class TokenCommand : ICommand
{
    public string[] RequiredParams
    {
        get;
        init;
    }
    protected readonly string[] Tokens;
    private readonly int _tokensLenght;

    /// <param name="tokens">words, their parts or combinations of words</param>
    /// <param name="requiredParams">required types params</param>
    public TokenCommand(string[] tokens, string[] requiredParams)
    {
        RequiredParams = requiredParams;
        Tokens = tokens;
        _tokensLenght = Tokens.Sum(x => x.Length);

        Array.Sort(Tokens, (a, b) => b.Length.CompareTo(a.Length));
    }

    public virtual void Execute(object[] args) { }

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

        return new CommandParseResult(
            count / _tokensLenght,
            resultText.ToString());
    }

    public virtual string GetCommand()
    {
        return string.Join(' ', Tokens);
    }
}