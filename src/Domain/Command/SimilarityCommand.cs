namespace OmniVoice.Domain.Command;

public class SimilarityCommand : ICommand
{
    public string[] RequiredParams
    {
        get;
        private set;
    }
    protected string[] Tokens;

    public SimilarityCommand(string[] tokens, string[] requiredParams)
    {
        RequiredParams = requiredParams;
        Tokens = tokens;
    }

    public SimilarityCommand(string tokens, string[] requiredParams)
    {
        RequiredParams = requiredParams;
        Tokens = tokens.Split(' ');
    }

    public virtual void Execute(object[] args) { }

    public float Probability(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return 0;
        }
        if (Tokens == null || Tokens.Length == 0)
        {
            throw new ArgumentNullException("Tokens");
        }

        int count = 0;

        foreach (var token in Tokens)
        {
            if (text.Contains(token))
            {
                count++;
            }
        }

        return count * 2 / (Tokens.Length + text.Split(' ').Length);
    }

    public virtual string GetCommand()
    {
        return string.Join(" ", Tokens);
    }
}