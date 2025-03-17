using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.PartialParsing.Month;

public class FemaleMonthException2004AndAfterParserTests
{
    [Theory]
    [InlineData("0471011234", 1)]
    [InlineData("0472011234", 2)]
    [InlineData("0482011234", 12)]
    public void Parse_ValidFemaleMonth_ReturnsCorrectMonthAndSex(string personalIdentificationNumber, byte expectedMonth)
    {
        // Arrange
        var parser = FemaleMonthException2004AndAfterParser.DefaultInstance;
        var span = personalIdentificationNumber.AsSpan();

        // Act
        var result = parser.Parse(span);

        // Assert
        result.Month.ShouldBe(expectedMonth);
        result.Sex.ShouldBe(SexEnum.Female);
    }

    [Theory]
    [InlineData("0469011234")]
    [InlineData("0483011234")]
    public void Parse_InvalidFemaleMonth_ThrowsFormatException(string personalIdentificationNumber)
    {
        // Arrange
        var parser = FemaleMonthException2004AndAfterParser.DefaultInstance;

        // Act & Assert
        var ex = Should.Throw<FormatException>(() =>
        {
            var span = personalIdentificationNumber.AsSpan();
            return parser.Parse(span);
        });
        ex.Message.ShouldBe(CzechPersonalIdentificationNumber.InvalidFemaleMonthMessage);
    }
}