using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Normalization;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.PartialParsing.Normalization;

public class NormalizedParserTests
{
    [Theory]
    [InlineData("123456/7890", "1234567890")]
    [InlineData(" 123 456 7890", "1234567890")]
    [InlineData("123 456/7890 ", "1234567890")]
    public void Parse_RemovesSeparatorsAndSpaces_ReturnsNormalizedString(string input, string expected)
    {
        // Act
        var result = NormalizedParser.Parse(input);

        // Assert
        result.ShouldBe(expected);
    }
}