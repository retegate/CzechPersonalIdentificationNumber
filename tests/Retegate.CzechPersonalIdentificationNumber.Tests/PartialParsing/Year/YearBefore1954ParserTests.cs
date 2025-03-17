using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.PartialParsing.Year;

public class YearBefore1954ParserTests
{
    [Theory]
    [InlineData("5301011234", 1953)]
    [InlineData("4901011234", 1949)]
    [InlineData("0001011234", 1900)]
    public void Parse_ValidYear_ReturnsCorrectYear(string personalIdentificationNumber, ushort expectedYear)
    {
        // Arrange
        var parser = YearBefore1954Parser.DefaultInstance;
        var span = personalIdentificationNumber.AsSpan();

        // Act
        var result = parser.Parse(span);

        // Assert
        result.ShouldBe(expectedYear);
    }

    [Theory]
    [InlineData("5401011234")]
    [InlineData("9901011234")]
    public void Parse_InvalidYear_ThrowsFormatException(string personalIdentificationNumber)
    {
        // Arrange
        var parser = YearBefore1954Parser.DefaultInstance;

        // Act
        var ex = Should.Throw<FormatException>(() =>
        {
            var span = personalIdentificationNumber.AsSpan();
            return parser.Parse(span);
        });

        // Assert
        ex.Message.ShouldBe(CzechPersonalIdentificationNumber.InvalidYearMessage);
    }
}