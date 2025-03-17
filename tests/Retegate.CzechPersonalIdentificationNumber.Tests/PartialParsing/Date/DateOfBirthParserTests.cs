using Retegate.CzechPersonalIdentificationNumber.PartialParsing.Date;

namespace Retegate.CzechPersonalIdentificationNumber.Tests.PartialParsing.Date;

public class DateOfBirthParserTests
{
    [Theory]
    [InlineData(2000, 1, "0001011234", "2000-01-01")]
    [InlineData(1999, 12, "9912311234", "1999-12-31")]
    [InlineData(2020, 2, "2002291234", "2020-02-29")]
    [InlineData(1900, 1, "0001011234", "1900-01-01")]
    public void Parse_ValidDate_ReturnsDateOfBirth(ushort year, byte month, string personalIdentificationNumber, string expectedDate)
    {
        // Arrange
        var span = personalIdentificationNumber.AsSpan();

        // Act
        var dateOfBirth = DateOfBirthParser.Parse(year, month, span);

        // Assert
        dateOfBirth.ShouldBe(DateOnly.Parse(expectedDate));
    }

    [Theory]
    [InlineData(2000, 1, "0001321234")]
    [InlineData(1999, 2, "9902301234")]
    [InlineData(2020, 2, "2002301234")]
    [InlineData(1900, 4, "0004311234")]
    public void Parse_InvalidDate_ThrowsFormatException(ushort year, byte month, string personalIdentificationNumber)
    {
        // Arrange, Act & Assert
        var ex = Should.Throw<FormatException>(() =>
        {
            var span = personalIdentificationNumber.AsSpan();
            return DateOfBirthParser.Parse(year, month, span);
        });
        ex.Message.ShouldBe(CzechPersonalIdentificationNumber.InvalidDayMessage);
    }
}