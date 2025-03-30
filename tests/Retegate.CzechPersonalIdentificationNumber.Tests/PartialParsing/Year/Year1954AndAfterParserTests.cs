using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Year;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.PartialParsing.Year;

public class Year1954AndAfterParserTests
{
    [Theory]
    [InlineData("5401011234", 1954)]
    [InlineData("9912311234", 1999)]
    [InlineData("0001011234", 2000)]
    [InlineData("2301011234", 2023)]
    public void Parse_ValidYear_ReturnsCorrectYear(string personalIdentificationNumber, ushort expectedYear)
    {
        // Arrange
        var parser = Year1954AndAfterParser.DefaultInstance;
        var span = personalIdentificationNumber.AsSpan();

        // Act
        var result = parser.Parse(span);

        // Assert
        result.ShouldBe(expectedYear);
    }

    [Theory]
    [InlineData("5301011234")]
    public void Parse_InvalidYear_ThrowsFormatException(string personalIdentificationNumber)
    {
        // Arrange
        var parser = Year1954AndAfterParser.DefaultInstance;

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