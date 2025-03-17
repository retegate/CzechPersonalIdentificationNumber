using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.PartialParsing.Month;

public class MaleMonthException2004AndAfterParserTests
{
    [Theory]
    [InlineData("0421011234", 1)]
    [InlineData("0422011234", 2)]
    [InlineData("0432011234", 12)]
    public void Parse_ValidMaleMonth_ReturnsCorrectMonthAndSex(string personalIdentificationNumber, byte expectedMonth)
    {
        // Arrange
        var parser = MaleMonthException2004AndAfterParser.DefaultInstance;
        var span = personalIdentificationNumber.AsSpan();

        // Act
        var result = parser.Parse(span);

        // Assert
        result.Month.ShouldBe(expectedMonth);
        result.Sex.ShouldBe(SexEnum.Male);
    }

    [Theory]
    [InlineData("0419011234")]
    [InlineData("0433011234")]
    public void Parse_InvalidMaleMonth_ThrowsFormatException(string personalIdentificationNumber)
    {
        // Arrange
        var parser = MaleMonthException2004AndAfterParser.DefaultInstance;

        // Act & Assert
        var ex = Should.Throw<FormatException>(() =>
        {
            var span = personalIdentificationNumber.AsSpan();
            return parser.Parse(span);
        });
        ex.Message.ShouldBe(CzechPersonalIdentificationNumber.InvalidMaleMonthMessage);
    }
}