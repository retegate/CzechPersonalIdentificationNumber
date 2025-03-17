using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.PartialParsing.Month;

public class MaleMonthParserTests
{
    [Theory]
    [InlineData("0121011234", 1)]
    [InlineData("0222011234", 2)]
    [InlineData("1232011234", 12)]
    public void Parse_ValidMaleMonth_ReturnsCorrectMonthAndSex(string personalIdentificationNumber, byte expectedMonth)
    {
        // Arrange
        var parser = MaleMonthParser.DefaultInstance;
        var span = personalIdentificationNumber.AsSpan();

        // Act
        var result = parser.Parse(span);

        // Assert
        result.Month.ShouldBe(expectedMonth);
        result.Sex.ShouldBe(SexEnum.Male);
    }

    [Theory]
    [InlineData("0019011234")]
    [InlineData("1333011234")]
    public void Parse_InvalidMaleMonth_ThrowsFormatException(string personalIdentificationNumber)
    {
        // Arrange
        var parser = MaleMonthParser.DefaultInstance;

        // Act & Assert
        var ex = Should.Throw<FormatException>(() =>
        {
            var span = personalIdentificationNumber.AsSpan();
            return parser.Parse(span);
        });
        ex.Message.ShouldBe(CzechPersonalIdentificationNumber.InvalidMaleMonthMessage);
    }
}