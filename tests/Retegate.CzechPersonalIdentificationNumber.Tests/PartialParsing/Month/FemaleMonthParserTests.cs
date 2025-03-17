using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Month;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.PartialParsing.Month;

public class FemaleMonthParserTests
{
    [Theory]
    [InlineData("5151011234", 1)]
    [InlineData("5252011234", 2)]
    [InlineData("5262011234", 12)]
    public void Parse_ValidFemaleMonth_ReturnsCorrectMonthAndSex(string personalIdentificationNumber, byte expectedMonth)
    {
        // Arrange
        var parser = FemaleMonthParser.DefaultInstance;
        var span = personalIdentificationNumber.AsSpan();

        // Act
        var result = parser.Parse(span);

        // Assert
        result.Month.ShouldBe(expectedMonth);
        result.Sex.ShouldBe(SexEnum.Female);
    }

    [Theory]
    [InlineData("5149011234")]
    [InlineData("5263011234")]
    public void Parse_InvalidFemaleMonth_ThrowsFormatException(string personalIdentificationNumber)
    {
        // Arrange
        var parser = FemaleMonthParser.DefaultInstance;

        // Act & Assert
        var ex = Should.Throw<FormatException>(() =>
        {
            var span = personalIdentificationNumber.AsSpan();
            return parser.Parse(span);
        });
        ex.Message.ShouldBe(CzechPersonalIdentificationNumber.InvalidFemaleMonthMessage);
    }
}