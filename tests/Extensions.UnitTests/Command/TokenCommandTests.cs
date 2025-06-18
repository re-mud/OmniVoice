using OmniVoice.Extension.Command;

namespace Extensions.UnitTests.Command;

[TestClass]
public class TokenCommandTests
{
    [TestMethod]
    public void Parse_WhenTextContainsTokens_ShouldReturnCorrectResult()
    {
        var tokens = new[] { "a test sentence" };
        var requiredParams = new string[] { };
        var command = new TokenCommand(tokens, requiredParams);
        string inputText = "this is a test sentence";

        var result = command.Parse(inputText);

        Assert.AreEqual("this is", result.RemainingText);
        Assert.AreEqual(1, result.Probability);
    }

    [TestMethod]
    public void Parse_WhenTextNotContainsTokens_ShouldReturnCorrectResult()
    {
        var tokens = new[] { "dog", "cat" };
        var requiredParams = new string[] { };
        var command = new TokenCommand(tokens, requiredParams);
        string inputText = "this is a test sentence";

        var result = command.Parse(inputText);

        Assert.AreEqual("this is a test sentence", result.RemainingText);
        Assert.AreEqual(0, result.Probability);
    }
}
