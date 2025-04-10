using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;

namespace Retegate.Bogus.CzechPersonalIdentificationNumber.Tests;

public class FakerExtensionAuxiliaryTests
{
    #region ChooseFromScenarios

    public static TheoryData<ScenariosChoiceEnum, ICollection<CzechPersonalIdentificationNumberKindEnum>> ValidScenarios
    {
        get
        {
            var results = new TheoryData<ScenariosChoiceEnum, ICollection<CzechPersonalIdentificationNumberKindEnum>>
            {
                {
                    ScenariosChoiceEnum.Male, [
                        CzechPersonalIdentificationNumberKindEnum.MaleBefore1954,
                        CzechPersonalIdentificationNumberKindEnum.Male1954AndLaterWithNoExceptionalRules,
                        CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleMaleInPopulationBoom1974Till1985,
                        CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleMaleInNewEraPopulationBoom2004
                    ]
                },
                {
                    ScenariosChoiceEnum.Female, [
                        CzechPersonalIdentificationNumberKindEnum.FemaleBefore1954,
                        CzechPersonalIdentificationNumberKindEnum.Female1954AndLaterWithNoExceptionalRules,
                        CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleFemaleInPopulationBoom1974Till1985,
                        CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleFemaleInNewEraPopulationBoom2004
                    ]
                }
            };

            return results;
        }
    }

    [Theory]
    [MemberData(nameof(ValidScenarios))]
    public void ChooseFromScenarios_WithValidInput_ReturnsCorrectValue(
        ScenariosChoiceEnum scenarioChoice,
        ICollection<CzechPersonalIdentificationNumberKindEnum> expectedValue)
    {
        // Arrange and Act
        var result = FakerExtensions.ChooseFromScenarios([scenarioChoice]);

        // Assert
        expectedValue.ShouldContain(result);
    }

    [Fact]
    public void ChooseFromScenarios_WithEmptyInput_ThrowsArgumentException()
    {
        // Arrange, Act & Assert
        Should.Throw<ArgumentNullException>(() =>
        {
            _ = FakerExtensions.ChooseFromScenarios([]);
        });
    }

    #endregion ChooseFromScenarios

    #region GetDelimiter

    [Theory]
    [InlineData(FormatEnum.Normalized, "")]
    [InlineData(FormatEnum.WithSlashDelimiter, "/")]
    public void GetDelimiter_WithValidInput_ReturnsCorrectDelimiter(FormatEnum format, string expectedDelimiter)
    {
        // Arrange and Act
        var result = FakerExtensions.GetDelimiter(format);

        // Assert
        result.ShouldBe(expectedDelimiter);
    }

    #endregion GetDelimiter

    #region GetBottomDateTimeLimit

    [Fact]
    public void GetBottomDateTimeLimit_NoInputNeeded_ShouldReturn100YearsMinus1DayDateTimeBack()
    {
        //todo: when it will pass here, please corrent this in the original  code
        // Arrange and Act
        var result = FakerExtensions.GetBottomDateTimeLimit();

        // Assert
        result.Date.ShouldBe(DateTime.UtcNow.AddDays(1d).AddYears(-100).Date);
    }

    #endregion GetBottomDateTimeLimit

    #region GetUpperDateTimeLimit

    [Fact]
    public void GetUpperDateTimeLimit_NoInputNeeded_ShouldReturnToday()
    {
        // Arrange and Act
        var result = FakerExtensions.GetUpperDateTimeLimit();

        // Assert
        result.Date.ShouldBe(DateTime.UtcNow.Date);
    }

    #endregion GetUpperDateTimeLimit

    #region FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954

    [Fact]
    public void
        FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954_WithMale1954AndAfterParser_ShouldPass()
    {
        FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954_ShouldPass<
            Male1954AndLaterCzechPersonalIdentificationNumber>("830304");
    }

    [Fact]
    public void
        FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954_WithFemale1954AndAfterParser_ShouldPass()
    {
        FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954_ShouldPass<
            Male1954AndLaterCzechPersonalIdentificationNumber>("835304");
    }

    [Fact]
    public void
        FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954_WithMaleExceptional2004AndAfterMonthRuleParser_ShouldPass()
    {
        FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954_ShouldPass<
            MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber>("042304");
    }

    [Fact]
    public void
        FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954_WithFemaleExceptional2004AndAfterMonthRuleParser_ShouldPass()
    {
        FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954_ShouldPass<
            FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber>("047304");
    }

    [Fact]
    public void
        FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954_WithMaleExceptionalBetween1974And1985MonthRuleParser_ShouldPass()
    {
        FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954_ShouldPass<
            MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber>("830304");
    }

    [Fact]
    public void
        FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954_WithFemaleExceptionalBetween1974And1985MonthRuleParser_ShouldPass()
    {
        FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954_ShouldPass<
            FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber>("8305304");
    }

    private static void
        FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954_ShouldPass<TParser>(
            string firstPart)
        where TParser : class, IParsable<TParser>
    {
        // Arrange and Act
        var result =
            FakerExtensions.FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954<TParser>(
                firstPart,
                string.Empty);

        // Assert
        (long.Parse(result) % 11).ShouldBe(0);
    }

    #endregion FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954

    #region FinishPersonalIdentificationNumberByGeneratingExceptionalVerificationNumberBetween1974And1985

    [Fact]
    public void
        FinishPersonalIdentificationNumberByGeneratingExceptionalVerificationNumberBetween1974And1985_WithMaleExceptionalVerificationNumberBetween1974And1985Parser_ShouldPass()
    {
        FinishPersonalIdentificationNumberByGeneratingExceptionalVerificationNumberBetween1974And1985_ShouldPass<
            MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber>("830304");
    }

    [Fact]
    public void
        FinishPersonalIdentificationNumberByGeneratingExceptionalVerificationNumberBetween1974And1985_WithFemaleExceptionalVerificationNumberBetween1974And1985Parser_ShouldPass()
    {
        FinishPersonalIdentificationNumberByGeneratingExceptionalVerificationNumberBetween1974And1985_ShouldPass<
            FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber>("830304");
    }

    private static void
        FinishPersonalIdentificationNumberByGeneratingExceptionalVerificationNumberBetween1974And1985_ShouldPass<
            TParser>(string firstPart)
        where TParser : class, IParsable<TParser>
    {
        // Arrange and Act
        var result =
            FakerExtensions
                .FinishPersonalIdentificationNumberByGeneratingExceptionalVerificationNumberBetween1974And1985<TParser>(
                    firstPart, string.Empty);

        // Assert
        result.Last().ShouldBe('0');
        var improvedResult = $"{result[..^1]}10";
        (long.Parse(improvedResult) % 11).ShouldBe(0);
    }

    #endregion FinishPersonalIdentificationNumberByGeneratingExceptionalVerificationNumberBetween1974And1985

    #region ComputeControlNumberLastDigitPivotForPersonsAfter1954

    [Fact]
    public void
        ComputeControlNumberLastDigitPivotForPersonsAfter1954_WithMale1954AndLaterCzechPersonalIdentificationNumber_ShouldPass()
    {
        ComputeControlNumberLastDigitPivotForPersonsAfter1954_ShouldPass<
            Male1954AndLaterCzechPersonalIdentificationNumber>("830304");
    }

    [Fact]
    public void
        ComputeControlNumberLastDigitPivotForPersonsAfter1954_WithFemale1954AndLaterCzechPersonalIdentificationNumber_ShouldPass()
    {
        ComputeControlNumberLastDigitPivotForPersonsAfter1954_ShouldPass<
            Female1954AndLaterCzechPersonalIdentificationNumber>("835304");
    }

    [Fact]
    public void
        ComputeControlNumberLastDigitPivotForPersonsAfter1954_WithMaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber_ShouldPass()
    {
        ComputeControlNumberLastDigitPivotForPersonsAfter1954_ShouldPass<
            MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber>("042304");
    }

    [Fact]
    public void
        ComputeControlNumberLastDigitPivotForPersonsAfter1954_WithFemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber_ShouldPass()
    {
        ComputeControlNumberLastDigitPivotForPersonsAfter1954_ShouldPass<
            FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber>("047304");
    }

    private void ComputeControlNumberLastDigitPivotForPersonsAfter1954_ShouldPass<TParser>(string firstPart)
        where TParser : class, IParsable<TParser>
    {
        // Arrange
        const int initialValue = 123;
        
        // Act
        var result =
            FakerExtensions.ComputeControlNumberLastDigitPivotForPersonsAfter1954<TParser>(10, firstPart, initialValue);

        // Assert
        (long.Parse($"{firstPart}{initialValue:D3}{result}") % 11).ShouldBe(0);
    }

    [Fact]
    public void
        ComputeControlNumberLastDigitPivotForPersonsAfter1954_WithMaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber_ShouldFail()
    {
        ComputeControlNumberLastDigitPivotForPersonsAfter1954_ShouldFail<
            MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber>("830304");
    }

    [Fact]
    public void
        ComputeControlNumberLastDigitPivotForPersonsAfter1954_WithFemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber_ShouldFail()
    {
        ComputeControlNumberLastDigitPivotForPersonsAfter1954_ShouldFail<
            FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber>("835304");
    }

    private void ComputeControlNumberLastDigitPivotForPersonsAfter1954_ShouldFail<TParser>(string firstPart)
        where TParser : class, IParsable<TParser>
    {
        // Arrange and Act
        var verificationNumber =
            FakerExtensions.ComputeControlNumberLastDigitPivotForPersonsAfter1954<TParser>(10, firstPart, 123);

        // Assert
        TParser.TryParse($"{firstPart}{verificationNumber}", null, out _).ShouldBeFalse();
    }

    #endregion ComputeControlNumberLastDigitPivotForPersonsAfter1954
}