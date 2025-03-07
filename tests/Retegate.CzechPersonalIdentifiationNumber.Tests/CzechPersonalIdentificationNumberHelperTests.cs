using PavelKudrna.Demo.Validation.PersonalIdentificationNumber;

namespace PavelKudrna.Demo.Validation.Tests.Validators.PersonalIdentificationNumber;

public class CzechPersonalIdentificationNumberHelperTests
{
    [Theory]
    [MemberData(nameof(CzechPersonalIdentificationNumberFixture.ValidPersonalIdentificationNumbers), MemberType = typeof(CzechPersonalIdentificationNumberFixture))]
    public void ValidatePattern_WithValidPattern_ReturnsTrue(CzechPersonalIdentificationNumberFixture.ValidPersonalIdentificationNumberTestCase testCase)
    {
        // Act
        var result = CzechPersonalIdentificationNumberHelper.ValidatePattern(testCase.PersonalIdentificationNumber);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(CzechPersonalIdentificationNumberFixture.InvalidPersonalIdentificationNumberPattern), MemberType = typeof(CzechPersonalIdentificationNumberFixture))]
    public void ValidatePattern_WithInvalidPattern_ReturnsFalse(string personalIdentificationNumber)
    {
        // Act
        var result = CzechPersonalIdentificationNumberHelper.ValidatePattern(personalIdentificationNumber);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    [InlineData(8)]
    [InlineData(9)]
    [InlineData(10)]
    [InlineData(11)]
    [InlineData(12)]
    public void ValidatePattern_WithValidMonth_ReturnsTrue(int month)
    {
        // Act
        var result = CzechPersonalIdentificationNumberHelper.ValidateMonth(1990, ref month);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(13)]
    public void ValidatePattern_WithInvalidMonth_ReturnsFalse(int month)
    {
        // Act
        var result = CzechPersonalIdentificationNumberHelper.ValidateMonth(1990, ref month);

        // Assert
        result.Should().BeFalse();
    }

    public static TheoryData<int> ValidDays
    {
        get
        {
            var results = new TheoryData<int>();

            for (var i = 1; i <= 31; i++)
            {
                results.Add(i);
            }

            return results;
        }
    }

    [Theory]
    [MemberData(nameof(ValidDays))]
    public void ValidatePattern_WithValidDay_ReturnsTrue(int day)
    {
        // Act
        var result = CzechPersonalIdentificationNumberHelper.ValidateDay(1990, 1, day);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(32)]
    public void ValidatePattern_WithInvalidDay_ReturnsFalse(int day)
    {
        // Act
        var result = CzechPersonalIdentificationNumberHelper.ValidateDay(1990, 1, day);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(CzechPersonalIdentificationNumberFixture.ValidPersonalIdentificationNumbers), MemberType = typeof(CzechPersonalIdentificationNumberFixture))]
    public void ValidateModulo_WithValidModulo_ReturnsTrue(CzechPersonalIdentificationNumberFixture.ValidPersonalIdentificationNumberTestCase testCase)
    {
        // Arrange
        var normalizedPersonalIdentificationNumber = CzechPersonalIdentificationNumberHelper.GetNormalizedPersonalIdentificationNumber(testCase.PersonalIdentificationNumber);
        
        // Act
        var result = CzechPersonalIdentificationNumberHelper.ValidateModulo(testCase.DateOfBirth.Year, normalizedPersonalIdentificationNumber);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(CzechPersonalIdentificationNumberFixture.InvalidPersonalIdentificationNumberModulo), MemberType = typeof(CzechPersonalIdentificationNumberFixture))]
    public void ValidateModulo_WithInvalidModulo_ReturnsFalse(string invalidPersonalIdentificationNumber)
    {
        // Act
        var result = CzechPersonalIdentificationNumberHelper.ValidateModulo(2000, invalidPersonalIdentificationNumber);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GetNormalizedPersonalIdentificationNumber_WithNull_ThrowsArgumentNullException()
    {
        // Act
        var action = () => CzechPersonalIdentificationNumberHelper.GetNormalizedPersonalIdentificationNumber(null!);

        // Assert
        action.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData("181124/0805", "1811240805")]
    [InlineData("1811240805", "1811240805")]
    [InlineData("181124/ 0805", "1811240805")]
    [InlineData("181124 /0805", "1811240805")]
    [InlineData("181124 / 0805", "1811240805")]
    [InlineData(" 960215 / 6014 ", "9602156014")]
    [InlineData(" 960215 / 6014", "9602156014")]
    [InlineData("960215 / 6014 ", "9602156014")]
    public void GetNormalizedPersonalIdentificationNumber_WithVariousScenarios_ReturnsNormalizedPersonalIdentificationNumber(string personalIdentificationNumber, string expectedNormalizedPersonalIdentificationNumber)
    {
        // Act
        var result = CzechPersonalIdentificationNumberHelper.GetNormalizedPersonalIdentificationNumber(personalIdentificationNumber);

        // Assert
        result.Should().Be(expectedNormalizedPersonalIdentificationNumber);
    }
}