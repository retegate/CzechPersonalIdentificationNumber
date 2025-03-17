using Bogus;

namespace Retegate.Bogus.CzechPersonalIdentificationNumber.Tests;

public class FakerExtensionsTests
{
    private readonly Faker _faker = new();

    #region GenerateCzechPersonalIdentificationNumber

    public sealed class CzechPersonalIdentificationNumberScenario
    {
        public required CzechPersonalIdentificationNumberKindEnum CzechPersonalIdentificationNumberKind { get; init; }
        public required FormatEnum Format { get; init; }
        public required Action<string> ExpectedAssertions { get; init; }
    }

    public static TheoryData<CzechPersonalIdentificationNumberScenario> ValidCzechPersonalIdentificationNumberScenarios
    {
        get
        {
            const int changingYear = Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.NewNumberStyleStartYear - Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.NineteenHundred;

            var maleBefore1954 = new CzechPersonalIdentificationNumberScenario
            {
                CzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum.MaleBefore1954,
                Format = FormatEnum.Normalized,
                ExpectedAssertions = czechPersonalIdentificationNumber =>
                {
                    czechPersonalIdentificationNumber.Length.ShouldBe(9);
                    var year = int.Parse(czechPersonalIdentificationNumber[..2]);
                    year.ShouldBeLessThan(changingYear);
                    var month = int.Parse(czechPersonalIdentificationNumber[2..4]);
                    month.ShouldBeInRange(1, 12);
                    var day = int.Parse(czechPersonalIdentificationNumber[4..6]);
                    var date = new DateTime(year, month, day);
                    date.ShouldBeLessThan(new DateTime(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.NewNumberStyleStartYear, 1, 1));
                    var controlNumber = int.Parse(czechPersonalIdentificationNumber[6..]);
                    controlNumber.ShouldBeInRange(0, 999);
                }
            };

            var maleBefore1954WithSlash = new CzechPersonalIdentificationNumberScenario
            {
                CzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum.MaleBefore1954,
                Format = FormatEnum.WithSlashDelimiter,
                ExpectedAssertions = czechPersonalIdentificationNumber =>
                {
                    czechPersonalIdentificationNumber.Length.ShouldBe(9);
                    var year = int.Parse(czechPersonalIdentificationNumber[..2]);
                    year.ShouldBeLessThan(changingYear);
                    var month = int.Parse(czechPersonalIdentificationNumber[2..4]);
                    month.ShouldBeInRange(1, 12);
                    var day = int.Parse(czechPersonalIdentificationNumber[4..6]);
                    var date = new DateTime(year, month, day);
                    date.ShouldBeLessThan(new DateTime(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.NewNumberStyleStartYear, 1, 1));
                    var slash = czechPersonalIdentificationNumber[6];
                    slash.ShouldBe(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.ControlNumberOptionalSeparator);
                    var controlNumber = int.Parse(czechPersonalIdentificationNumber[7..]);
                    controlNumber.ShouldBeInRange(0, 999);
                }
            };

            var femaleBefore1954 = new CzechPersonalIdentificationNumberScenario
            {
                CzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum.FemaleBefore1954,
                Format = FormatEnum.Normalized,
                ExpectedAssertions = czechPersonalIdentificationNumber =>
                {
                    czechPersonalIdentificationNumber.Length.ShouldBe(9);
                    var year = int.Parse(czechPersonalIdentificationNumber[..2]);
                    year.ShouldBeLessThan(changingYear);
                    var month = int.Parse(czechPersonalIdentificationNumber[2..4]);
                    month.ShouldBeGreaterThanOrEqualTo(50);
                    month.ShouldBeInRange(51, 62);

                    var day = int.Parse(czechPersonalIdentificationNumber[4..6]);
                    var date = new DateTime(year, month - 50, day);
                    date.ShouldBeLessThan(new DateTime(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.NewNumberStyleStartYear, 1, 1));
                }
            };

            var maleAfter1954 = new CzechPersonalIdentificationNumberScenario
            {
                CzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum.Male1954AndLaterWithNoExceptionalRules,
                Format = FormatEnum.Normalized,
                ExpectedAssertions = czechPersonalIdentificationNumber =>
                {
                    czechPersonalIdentificationNumber.Length.ShouldBe(10);
                    var year = int.Parse(czechPersonalIdentificationNumber[..2]);
                    year.ShouldBeGreaterThanOrEqualTo(changingYear);
                    var month = int.Parse(czechPersonalIdentificationNumber[2..4]);
                    month.ShouldBeInRange(1, 12);
                    var day = int.Parse(czechPersonalIdentificationNumber[4..6]);
                    var date = new DateTime(year, month, day);
                    date.ShouldBeGreaterThanOrEqualTo(new DateTime(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.NewNumberStyleStartYear, 1, 1));
                    var controlNumber = int.Parse(czechPersonalIdentificationNumber[6..]);
                    controlNumber.ShouldBeInRange(0, 9999);

                    var czechPersonalIdentificationNumberWithoutControlNumber = czechPersonalIdentificationNumber.Replace(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.ControlNumberOptionalSeparator.ToString(), string.Empty);
                    var modulo = long.Parse(czechPersonalIdentificationNumberWithoutControlNumber) % Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.VerificationNumberModuloDivider;
                    modulo.ShouldBe(0);
                }
            };

            var femaleAfter1954 = new CzechPersonalIdentificationNumberScenario
            {
                CzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum.Female1954AndLaterWithNoExceptionalRules,
                Format = FormatEnum.Normalized,
                ExpectedAssertions = czechPersonalIdentificationNumber =>
                {
                    czechPersonalIdentificationNumber.Length.ShouldBe(10);
                    var year = int.Parse(czechPersonalIdentificationNumber[..2]);
                    year.ShouldBeGreaterThanOrEqualTo(changingYear);
                    var month = int.Parse(czechPersonalIdentificationNumber[2..4]);
                    month.ShouldBeGreaterThanOrEqualTo(50);
                    month.ShouldBeInRange(51, 62);

                    var day = int.Parse(czechPersonalIdentificationNumber[4..6]);
                    var date = new DateTime(year, month - 50, day);
                    date.ShouldBeGreaterThanOrEqualTo(new DateTime(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.NewNumberStyleStartYear, 1, 1));
                    var controlNumber = int.Parse(czechPersonalIdentificationNumber[6..]);
                    controlNumber.ShouldBeInRange(0, 9999);

                    var czechPersonalIdentificationNumberWithoutControlNumber = czechPersonalIdentificationNumber.Replace(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.ControlNumberOptionalSeparator.ToString(), string.Empty);
                    var modulo = long.Parse(czechPersonalIdentificationNumberWithoutControlNumber) % Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.VerificationNumberModuloDivider;
                    modulo.ShouldBe(0);
                }
            };

            var exceptionalMaleInPopulationBoom1974Till1985 = new CzechPersonalIdentificationNumberScenario
            {
                CzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleMaleInPopulationBoom1974Till1985,
                Format = FormatEnum.Normalized,
                ExpectedAssertions = czechPersonalIdentificationNumber =>
                {
                    czechPersonalIdentificationNumber.Length.ShouldBe(10);
                    var year = int.Parse(czechPersonalIdentificationNumber[..2]);
                    year.ShouldBeGreaterThanOrEqualTo(74);
                    year.ShouldBeLessThanOrEqualTo(85);
                    var month = int.Parse(czechPersonalIdentificationNumber[2..4]);
                    month.ShouldBeInRange(1, 12);
                    var day = int.Parse(czechPersonalIdentificationNumber[4..6]);
                    var date = new DateTime(year, month, day);
                    date.ShouldBeGreaterThanOrEqualTo(new DateTime(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.PopulationBoomStartYear, 1, 1));
                    date.ShouldBeLessThanOrEqualTo(new DateTime(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.PopulationBoomEndYear, 12, 31));
                    var controlNumber = int.Parse(czechPersonalIdentificationNumber[6..]);
                    controlNumber.ShouldBeInRange(0, 9999);

                    var czechPersonalIdentificationNumberWithoutControlNumber = czechPersonalIdentificationNumber.Replace(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.ControlNumberOptionalSeparator.ToString(), string.Empty);
                    var modulo = long.Parse(czechPersonalIdentificationNumberWithoutControlNumber) % Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.VerificationNumberModuloDivider;
                    try
                    {
                        modulo.ShouldBe(0);
                    }
                    catch (ShouldAssertException)
                    {
                        czechPersonalIdentificationNumberWithoutControlNumber = czechPersonalIdentificationNumberWithoutControlNumber[..^1] + "10";
                        modulo = long.Parse(czechPersonalIdentificationNumberWithoutControlNumber) % Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.VerificationNumberModuloDivider;
                        modulo.ShouldBe(0);
                    }
                }
            };

            var exceptionalFemaleInPopulationBoom1974Till1985 = new CzechPersonalIdentificationNumberScenario
            {
                CzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleFemaleInPopulationBoom1974Till1985,
                Format = FormatEnum.Normalized,
                ExpectedAssertions = czechPersonalIdentificationNumber =>
                {
                    czechPersonalIdentificationNumber.Length.ShouldBe(10);
                    var year = int.Parse(czechPersonalIdentificationNumber[..2]);
                    year.ShouldBeGreaterThanOrEqualTo(74);
                    year.ShouldBeLessThanOrEqualTo(85);
                    var month = int.Parse(czechPersonalIdentificationNumber[2..4]);
                    month.ShouldBeGreaterThanOrEqualTo(50);
                    month.ShouldBeInRange(51, 62);

                    var day = int.Parse(czechPersonalIdentificationNumber[4..6]);
                    var date = new DateTime(year, month - 50, day);
                    date.ShouldBeGreaterThanOrEqualTo(new DateTime(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.PopulationBoomStartYear, 1, 1));
                    date.ShouldBeLessThanOrEqualTo(new DateTime(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.PopulationBoomEndYear, 12, 31));
                    var controlNumber = int.Parse(czechPersonalIdentificationNumber[6..]);
                    controlNumber.ShouldBeInRange(0, 9999);

                    var czechPersonalIdentificationNumberWithoutControlNumber = czechPersonalIdentificationNumber.Replace(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.ControlNumberOptionalSeparator.ToString(), string.Empty);
                    var modulo = long.Parse(czechPersonalIdentificationNumberWithoutControlNumber) % Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.VerificationNumberModuloDivider;
                    try
                    {
                        modulo.ShouldBe(0);
                    }
                    catch (ShouldAssertException)
                    {
                        czechPersonalIdentificationNumberWithoutControlNumber = czechPersonalIdentificationNumberWithoutControlNumber[..^1] + "10";
                        modulo = long.Parse(czechPersonalIdentificationNumberWithoutControlNumber) % Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.VerificationNumberModuloDivider;
                        modulo.ShouldBe(0);
                    }
                }
            };

            var exceptionalMaleInNewEraPopulationBoom2004 = new CzechPersonalIdentificationNumberScenario
            {
                CzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleMaleInNewEraPopulationBoom2004,
                Format = FormatEnum.Normalized,
                ExpectedAssertions = czechPersonalIdentificationNumber =>
                {
                    czechPersonalIdentificationNumber.Length.ShouldBe(10);
                    var year = int.Parse(czechPersonalIdentificationNumber[..2]);
                    year.ShouldBeGreaterThanOrEqualTo(04);
                    year.ShouldBeLessThanOrEqualTo(DateTime.Today.Year - Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.TwoThousand);
                    var month = int.Parse(czechPersonalIdentificationNumber[2..4]);
                    month.ShouldBeInRange(21, 32);
                    var day = int.Parse(czechPersonalIdentificationNumber[4..6]);
                    var date = new DateTime(year, month - 20, day);
                    date.ShouldBeGreaterThanOrEqualTo(new DateTime(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.NewEraPopulationBoomStartYear, 1, 1));
                    var controlNumber = int.Parse(czechPersonalIdentificationNumber[6..]);
                    controlNumber.ShouldBeInRange(0, 9999);

                    var czechPersonalIdentificationNumberWithoutControlNumber = czechPersonalIdentificationNumber.Replace(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.ControlNumberOptionalSeparator.ToString(), string.Empty);
                    var modulo = long.Parse(czechPersonalIdentificationNumberWithoutControlNumber) % Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.VerificationNumberModuloDivider;
                    modulo.ShouldBe(0);
                }
            };

            var exceptionalFemaleInNewEraPopulationBoom2004 = new CzechPersonalIdentificationNumberScenario
            {
                CzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleFemaleInNewEraPopulationBoom2004,
                Format = FormatEnum.Normalized,
                ExpectedAssertions = czechPersonalIdentificationNumber =>
                {
                    czechPersonalIdentificationNumber.Length.ShouldBe(10);
                    var year = int.Parse(czechPersonalIdentificationNumber[..2]);
                    year.ShouldBeGreaterThanOrEqualTo(04);
                    year.ShouldBeLessThanOrEqualTo(DateTime.Today.Year - Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.TwoThousand);
                    var month = int.Parse(czechPersonalIdentificationNumber[2..4]);
                    month.ShouldBeInRange(71, 82);
                    var day = int.Parse(czechPersonalIdentificationNumber[4..6]);
                    var date = new DateTime(year, month - 70, day);
                    date.ShouldBeGreaterThanOrEqualTo(new DateTime(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.NewEraPopulationBoomStartYear, 1, 1));
                    var controlNumber = int.Parse(czechPersonalIdentificationNumber[6..]);
                    controlNumber.ShouldBeInRange(0, 9999);

                    var czechPersonalIdentificationNumberWithoutControlNumber = czechPersonalIdentificationNumber.Replace(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.ControlNumberOptionalSeparator.ToString(), string.Empty);
                    var modulo = long.Parse(czechPersonalIdentificationNumberWithoutControlNumber) % Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.VerificationNumberModuloDivider;
                    modulo.ShouldBe(0);
                }
            };

            var arbitraryMale = new CzechPersonalIdentificationNumberScenario { CzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum.ArbitraryMale, Format = FormatEnum.Normalized, ExpectedAssertions = czechPersonalIdentificationNumber => GeneralCumulativeAssertion(new[] { maleBefore1954.ExpectedAssertions, maleAfter1954.ExpectedAssertions, exceptionalMaleInNewEraPopulationBoom2004.ExpectedAssertions, exceptionalMaleInNewEraPopulationBoom2004.ExpectedAssertions, }, czechPersonalIdentificationNumber) };

            var arbitraryFemale = new CzechPersonalIdentificationNumberScenario
            {
                CzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum.ArbitraryFemale,
                Format = FormatEnum.Normalized,
                ExpectedAssertions = czechPersonalIdentificationNumber =>
                    GeneralCumulativeAssertion([femaleBefore1954.ExpectedAssertions, femaleAfter1954.ExpectedAssertions, exceptionalFemaleInNewEraPopulationBoom2004.ExpectedAssertions, exceptionalFemaleInNewEraPopulationBoom2004.ExpectedAssertions], czechPersonalIdentificationNumber)
            };

            var arbitraryPerson = new CzechPersonalIdentificationNumberScenario { CzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum.ArbitraryPerson, Format = FormatEnum.Normalized, ExpectedAssertions = czechPersonalIdentificationNumber => GeneralCumulativeAssertion([arbitraryMale.ExpectedAssertions, arbitraryFemale.ExpectedAssertions], czechPersonalIdentificationNumber) };

            return
            [
                maleBefore1954,
                maleBefore1954WithSlash,
                femaleBefore1954,
                maleAfter1954,
                femaleAfter1954,
                exceptionalMaleInPopulationBoom1974Till1985,
                exceptionalFemaleInPopulationBoom1974Till1985,
                exceptionalMaleInNewEraPopulationBoom2004,
                exceptionalFemaleInNewEraPopulationBoom2004,
                arbitraryMale,
                arbitraryFemale,
                arbitraryPerson
            ];
        }
    }

    private static void GeneralCumulativeAssertion(IEnumerable<Action<string>> possibleAssertions, string czechPersonalIdentificationNumber)
    {
        var cumulativeAssertionResult = false;

        foreach (var assertion in possibleAssertions)
        {
            try
            {
                assertion(czechPersonalIdentificationNumber);
                cumulativeAssertionResult = true;
                break;
            }
            catch (ShouldAssertException)
            {
                /* ignored */
            }
        }

        cumulativeAssertionResult.ShouldBeTrue();
    }

    [Theory]
    [MemberData(nameof(ValidCzechPersonalIdentificationNumberScenarios))]
    public void GenerateCustomString_WithValidParameters_ReturnsCzechPersonalIdentificationNumberWithSpecifiedFormat(CzechPersonalIdentificationNumberScenario scenario)
    {
        // Arrange and Act
        var actual = _faker.GenerateCzechPersonalIdentificationNumber(scenario.CzechPersonalIdentificationNumberKind, scenario.Format);

        // Assert
        actual.ShouldNotBeNullOrWhiteSpace();
        scenario.ExpectedAssertions(actual);
    }

    #endregion GenerateCzechPersonalIdentificationNumber

    #region ChooseFromScenarios

    public sealed class ScenarioChoices
    {
        public required IEnumerable<ScenariosChoiceEnum> ScenariosChoices { get; init; }
        public required IEnumerable<CzechPersonalIdentificationNumberKindEnum> Expected { get; init; }
    }

    public static TheoryData<ScenarioChoices> ScenarioChoicesScenarios => new()
    {
        new ScenarioChoices()
        {
            ScenariosChoices = [ScenariosChoiceEnum.Male, ScenariosChoiceEnum.Female],
            Expected =
            [
                CzechPersonalIdentificationNumberKindEnum.MaleBefore1954,
                CzechPersonalIdentificationNumberKindEnum.FemaleBefore1954,
                CzechPersonalIdentificationNumberKindEnum.Male1954AndLaterWithNoExceptionalRules,
                CzechPersonalIdentificationNumberKindEnum.Female1954AndLaterWithNoExceptionalRules,
                CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleMaleInPopulationBoom1974Till1985,
                CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleFemaleInPopulationBoom1974Till1985,
                CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleMaleInNewEraPopulationBoom2004,
                CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleFemaleInNewEraPopulationBoom2004
            ]
        },
        new ScenarioChoices()
        {
            ScenariosChoices = [ScenariosChoiceEnum.Female],
            Expected =
            [
                CzechPersonalIdentificationNumberKindEnum.FemaleBefore1954,
                CzechPersonalIdentificationNumberKindEnum.Female1954AndLaterWithNoExceptionalRules,
                CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleFemaleInPopulationBoom1974Till1985,
                CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleFemaleInNewEraPopulationBoom2004
            ]
        },
        new ScenarioChoices()
        {
            ScenariosChoices = [ScenariosChoiceEnum.Male],
            Expected =
            [
                CzechPersonalIdentificationNumberKindEnum.MaleBefore1954,
                CzechPersonalIdentificationNumberKindEnum.Male1954AndLaterWithNoExceptionalRules,
                CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleMaleInPopulationBoom1974Till1985,
                CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleMaleInNewEraPopulationBoom2004,
            ]
        },
    };

    [Theory]
    [MemberData(nameof(ScenarioChoicesScenarios))]
    public void ChooseFromScenarios_WithValidParameters_ReturnsCzechPersonalIdentificationNumberKindEnum(ScenarioChoices scenariosChoices)
    {
        // 

        // Arrange and Act
        var result = FakerExtensions.ChooseFromScenarios(scenariosChoices.ScenariosChoices);

        // Assert
        scenariosChoices.Expected.ShouldContain(result);
    }

    #endregion ChooseFromScenarios

    #region GetDelimiter

    [Theory]
    [InlineData(FormatEnum.Normalized)]
    [InlineData(FormatEnum.WithSlashDelimiter)]
    public void GetDelimiter_WithValidParameters_ReturnsDelimiter(FormatEnum format)
    {
        // Arrange and  Act
        var actual = FakerExtensions.GetDelimiter(format);

        // Assert
        actual.ShouldNotBeNull();
        actual.ShouldBe(format == FormatEnum.Normalized ? String.Empty : "/");
    }

    #endregion GetDelimiter

    #region GetBottomDateTimeLimit

    [Fact]
    public void GetBottomDateTimeLimit_WithValidParameters_ReturnsBottomDateTimeLimit()
    {
        // Arrange
        var expected = DateTime.Now.AddYears(-101).AddDays(-1d);

        // Act
        var actual = FakerExtensions.GetBottomDateTimeLimit();

        // Assert
        actual.Date.ShouldBe(expected.Date);
    }

    #endregion GetBottomDateTimeLimit

    #region GetUpperDateTimeLimit

    [Fact]
    public void GetUpperDateTimeLimit_WithValidParameters_ReturnsUpperDateTimeLimit()
    {
        // Arrange
        var expected = DateTime.Today;

        // Act
        var actual = FakerExtensions.GetUpperDateTimeLimit();

        // Assert
        actual.Date.ShouldBe(expected.Date);
    }

    #endregion GetUpperDateTimeLimit

    #region GenerateDateOfBirth

    [Fact]
    public void GenerateDateOfBirth_WithValidParameters_ReturnsDateOfBirth()
    {
        for (var i = 0; i < 10_000; ++i)
        {
            // Arrange
            var start = new DateTime(1983, 1, 1);
            var end = DateTime.Today;

            // Act
            var date = FakerExtensions.GenerateDateOfBirth(_faker, start, end);

            // Assert
            var dateTime = date.ToDateTime(TimeOnly.MinValue);
            date.ShouldNotBe(default);
            dateTime.ShouldBeGreaterThanOrEqualTo(start);
            dateTime.ShouldBeLessThanOrEqualTo(end);
        }
    }

    #endregion GenerateDateOfBirth

    #region FinishPersonalIdentificationNumberByGeneratingProperControlNumberAfter1954

    [Theory]
    [InlineData(["830304", 1983])]
    [InlineData(["000000", 2000])]
    [InlineData(["999999", 1999])]
    [InlineData(["531224", 1953])]
    public void FinishPersonalIdentificationNumberByGeneratingProperControlNumberAfter1954_WithValidParameters_ReturnsPersonalIdentificationNumber(string personalIdentificationNumberFirstPart, int year)
    {
        // Arrange
        const string delimiter = "";

        // Act
        string actual = null!;
        var action = () => actual = FakerExtensions.FinishPersonalIdentificationNumberByGeneratingProperControlNumberAfter1954(year, personalIdentificationNumberFirstPart, delimiter);

        // Assert
        action.ShouldNotThrow();
        actual.ShouldNotBeNullOrWhiteSpace();
        var personalIdentificationNumber = long.Parse(actual);

        personalIdentificationNumber.ShouldBeInRange(0, 999999_9999);
        if (year < Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.NewNumberStyleStartYear)
        {
            personalIdentificationNumber.ShouldBeInRange(0, 999999_999);
        }
        else
        {
            personalIdentificationNumber.ShouldBeInRange(0, 999999_9999);
            try
            {
                (personalIdentificationNumber % Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.VerificationNumberModuloDivider).ShouldBe(0);
            }
            catch
            {
                year.ShouldBeLessThanOrEqualTo(Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.PopulationBoomEndYear);
                actual[^1].ShouldBe('0');
                (long.Parse($"{actual[..^1]}10") % Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.VerificationNumberModuloDivider).ShouldBe(0);
            }
        }
    }

    #endregion FinishPersonalIdentificationNumberByGeneratingProperControlNumberAfter1954

    #region ComputeControlNumberLastDigitPivot

    [Theory]
    [InlineData("830304")]
    [InlineData("000000")]
    [InlineData("999999")]
    public void ComputeControlNumberLastDigitPivot_WithValidParameters_ReturnsControlNumberLastDigitPivot(string personalIdentificationNumberFirstPart)
    {
        // Arrange
        const int limitPivot = 10;
        const int initialControlNumberPivot = 1234;

        for (var i = 0; i < 10_000 - limitPivot - 1; ++i)
        {
            {
                // Act
                int actual = -1;
                var action = () => actual = FakerExtensions.ComputeControlNumberLastDigitPivot(limitPivot, personalIdentificationNumberFirstPart, initialControlNumberPivot);

                // Assert
                action.ShouldNotThrow();
                actual.ShouldBeInRange(0, limitPivot);
            }
        }
    }

    #endregion ComputeControlNumberLastDigitPivot
}