namespace Retegate.CzechPersonalIdentificationNumber.Tests;

public class AuxiliaryMethodsTests
{
    #region ValidateYear

    public sealed class ValidYearsScenario
    {
        public required int Year { get; init; }
        public required string CzechPersonalIdentificationNumber { get; init; }
        public required int ExpectedYear { get; init; }
    }

    public static TheoryData<ValidYearsScenario> ValidYears =
    [
        new() { Year = 00, CzechPersonalIdentificationNumber = "1234567890", ExpectedYear = 2000 },
        new() { Year = 90, CzechPersonalIdentificationNumber = "1234567890", ExpectedYear = 1990 },
        new() { Year = 22, CzechPersonalIdentificationNumber = "1234567890", ExpectedYear = 2022 },
        new() { Year = 22, CzechPersonalIdentificationNumber = "123456789", ExpectedYear = 1922 },
        new() { Year = 99, CzechPersonalIdentificationNumber = "1234567890", ExpectedYear = 1999 },
        new() { Year = DateTime.Now.Year - 2000, CzechPersonalIdentificationNumber = "1234567890", ExpectedYear = DateTime.Now.Year },
    ];

    [Theory]
    [MemberData(nameof(ValidYears))]
    public void ValidateYear_WithValidYear_ReturnsTrue(ValidYearsScenario yearScenario)
    {
        // Arrange and Act
        var result = CzechPersonalIdentificationNumber.ValidateShortYear(yearScenario.Year, yearScenario.CzechPersonalIdentificationNumber);

        // Assert
        result.IsValid.ShouldBeTrue();
        result.Year.ShouldNotBeNull();
        result.Year.ShouldBe(yearScenario.ExpectedYear);
    }

    public sealed class InvalidYearsScenario
    {
        public required int Year { get; init; }
        public required string CzechPersonalIdentificationNumber { get; init; }
    }

    public static TheoryData<InvalidYearsScenario> InvalidYears =
    [
        new() { Year = 1899, CzechPersonalIdentificationNumber = "1234567890" },
    ];

    [Theory]
    [MemberData(nameof(InvalidYears))]
    public void ValidateYear_WithInvalidYearOutOfRange_ReturnsFalse(InvalidYearsScenario scenario)
    {
        // Arrange and Act
        var result = CzechPersonalIdentificationNumber.ValidateShortYear(scenario.Year, scenario.CzechPersonalIdentificationNumber);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Year.ShouldBeNull();
    }

    #endregion ValidateYear

    #region ValidateMonth

    public sealed class ValidMonthsScenario
    {
        public required int Year { get; init; }
        public required int Month { get; init; }
        public int? ExpectedMonth { get; init; }
        public SexEnum? ExpectedSex { get; init; }
    }

    public static TheoryData<ValidMonthsScenario> ValidMonthsScenarios
    {
        get
        {
            var results = new TheoryData<ValidMonthsScenario>();

            for (var month = 1; month <= 12; month++)
            {
                // male
                results.Add(new ValidMonthsScenario { Year = 1900, Month = month, ExpectedMonth = month, ExpectedSex = SexEnum.Male });
                results.Add(new ValidMonthsScenario { Year = 2003, Month = month, ExpectedMonth = month, ExpectedSex = SexEnum.Male });
                results.Add(new ValidMonthsScenario { Year = 2004, Month = month, ExpectedMonth = month, ExpectedSex = SexEnum.Male });
                results.Add(new ValidMonthsScenario { Year = 2004, Month = month + 20, ExpectedMonth = month, ExpectedSex = SexEnum.Male });
                results.Add(new ValidMonthsScenario { Year = 2014, Month = month, ExpectedMonth = month, ExpectedSex = SexEnum.Male });
                results.Add(new ValidMonthsScenario { Year = 2014, Month = month + 20, ExpectedMonth = month, ExpectedSex = SexEnum.Male });

                // female
                results.Add(new ValidMonthsScenario { Year = 1900, ExpectedSex = SexEnum.Female, Month = month + 50, ExpectedMonth = month });
                results.Add(new ValidMonthsScenario { Year = 2003, ExpectedSex = SexEnum.Female, Month = month + 50, ExpectedMonth = month });
                results.Add(new ValidMonthsScenario { Year = 2004, ExpectedSex = SexEnum.Female, Month = month + 50, ExpectedMonth = month });
                results.Add(new ValidMonthsScenario { Year = 2004, ExpectedSex = SexEnum.Female, Month = month + 70, ExpectedMonth = month });
                results.Add(new ValidMonthsScenario { Year = 2014, ExpectedSex = SexEnum.Female, Month = month + 50, ExpectedMonth = month });
                results.Add(new ValidMonthsScenario { Year = 2014, ExpectedSex = SexEnum.Female, Month = month + 70, ExpectedMonth = month });
            }

            return results;
        }
    }

    [Theory]
    [MemberData(nameof(ValidMonthsScenarios))]
    public void ValidateMonth_WithValidMonth_ReturnsTrue(ValidMonthsScenario monthScenario)
    {
        // 

        // Arrange and Act
        var result = CzechPersonalIdentificationNumber.ValidateMonth(monthScenario.Year, monthScenario.Month);

        // Assert
        result.IsValid.ShouldBeTrue();
        result.Month.ShouldNotBeNull();
        result.Month.ShouldBe(monthScenario.ExpectedMonth!.Value);
        result.Sex.ShouldNotBeNull();
        result.Sex.ShouldBe(monthScenario.ExpectedSex!.Value);
    }

    public static TheoryData<ValidMonthsScenario> InvalidMonthsScenarios
    {
        get
        {
            var results = new TheoryData<ValidMonthsScenario>();

            for (var month = 13; month <= 19; month++)
            {
                results.Add(new ValidMonthsScenario { Year = 2003, Month = month });
                results.Add(new ValidMonthsScenario { Year = 2003, Month = month });
            }

            for (var month = 33; month <= 49; month++)
            {
                results.Add(new ValidMonthsScenario { Year = 2003, Month = month });
                results.Add(new ValidMonthsScenario { Year = 2003, Month = month });
            }
            
            for (var month = 63; month <= 69; month++)
            {
                results.Add(new ValidMonthsScenario { Year = 2003, Month = month });
                results.Add(new ValidMonthsScenario { Year = 2003, Month = month });
            }
            
            for (var month = 83; month <= 100; month++)
            {
                results.Add(new ValidMonthsScenario { Year = 2003, Month = month });
                results.Add(new ValidMonthsScenario { Year = 2003, Month = month });
            }

            return results;
        }
    }

    [Theory]
    [MemberData(nameof(InvalidMonthsScenarios))]
    public void ValidateMonth_WithInvalidMonthOutOfRange_ReturnsFalse(ValidMonthsScenario monthScenario)
    {
        // Arrange and Act
        var result = CzechPersonalIdentificationNumber.ValidateMonth(monthScenario.Year, monthScenario.Month);

        // Assert
        result.IsValid.ShouldBeFalse();
        result.Month.ShouldBeNull();
        result.Sex.ShouldBeNull();
    }

    #endregion ValidateMonth

    #region ValidateDay

    public sealed class DateScenario
    {
        public required int Year { get; init; }
        public required int Month { get; init; }
        public required int Day { get; init; }
    }

    public static TheoryData<DateScenario> ValidDateScenarios
    {
        get
        {
            var results = new TheoryData<DateScenario>();

            var dt = new DateOnly(1900, 1, 1);
            var today = DateOnly.FromDateTime(DateTime.Now.AddDays(1d));

            do
            {
                results.Add(new DateScenario { Year = dt.Year, Month = dt.Month, Day = dt.Day });
                dt = dt.AddDays(1);
            } while (dt < today);

            return results;
        }
    }

    [Theory]
    [MemberData(nameof(ValidDateScenarios))]
    public void ValidateDay_WithValidDay_ReturnsTrue(DateScenario dateScenario)
    {
        // Arrange and Act
        var result = CzechPersonalIdentificationNumber.ValidateDay(dateScenario.Year, dateScenario.Month, dateScenario.Day);

        // Assert
        result.ShouldBeTrue();
    }

    public static TheoryData<DateScenario> InvalidDateScenarios
    {
        get
        {
            var results = new TheoryData<DateScenario>
            {
                new() { Year = 1900, Month = 1, Day = 0 },
                new() { Year = 1900, Month = 1, Day = 32 },
                new() { Year = 1900, Month = 2, Day = 29 },
                new() { Year = 1900, Month = 2, Day = 30 },
                new() { Year = 1900, Month = 2, Day = 31 },
                new() { Year = 1900, Month = 4, Day = 31 },
                new() { Year = 1900, Month = 6, Day = 31 },
                new() { Year = 1900, Month = 9, Day = 31 },
                new() { Year = 1900, Month = 11, Day = 31 },
                new() { Year = 1900, Month = 12, Day = 32 },
                new() { Year = 1900, Month = 0, Day = 15 },
                new() { Year = 1900, Month = 13, Day = 15 },
                new() { Year = 1900, Month = 13, Day = 15 }
            };

            return results;
        }
    }

    [Theory]
    [MemberData(nameof(InvalidDateScenarios))]
    public void ValidateDay_WithInvalidDateOutOfRange_ReturnsFalse(DateScenario dateScenario)
    {
        // Arrange and Act
        var result = CzechPersonalIdentificationNumber.ValidateDay(dateScenario.Year, dateScenario.Month, dateScenario.Day);

        // Assert
        result.ShouldBeFalse();
    }

    #endregion ValidateDay

    #region ValidateModulo

    public sealed class ModuloScenario
    {
        public required int Year { get; init; }
        public required string PersonalIdentificationNumber { get; init; }
    }

    public static TheoryData<ModuloScenario> ValidModuloScenarios
    {
        get
        {
            var results = new TheoryData<ModuloScenario>
            {
                new() { Year = 1983, PersonalIdentificationNumber = "8303040130" },
                new() { Year = 2014, PersonalIdentificationNumber = "1404100852" },
                new() { Year = 2012, PersonalIdentificationNumber = "1205120620" },
                new() { Year = 1978, PersonalIdentificationNumber = "7859020422" },
                new() { Year = 2016, PersonalIdentificationNumber = "1660071314" },
                new() { Year = 1953, PersonalIdentificationNumber = "531224376" },
            };

            return results;
        }
    }

    [Theory]
    [MemberData(nameof(ValidModuloScenarios))]
    public void ValidateModulo_WithValidModulo_ReturnsTrue(ModuloScenario moduloScenario)
    {
        // Arrange and Act
        var result = CzechPersonalIdentificationNumber.ValidateModulo(moduloScenario.Year, moduloScenario.PersonalIdentificationNumber);

        // Assert
        result.ShouldBeTrue();
    }

    public static TheoryData<ModuloScenario> InvalidScenarios
    {
        get
        {
            var results = new TheoryData<ModuloScenario>
            {
                new() { Year = 1982, PersonalIdentificationNumber = "8303040142" },
                new() { Year = 2014, PersonalIdentificationNumber = "1404100851" },
                new() { Year = 2012, PersonalIdentificationNumber = "1205120626" },
                new() { Year = 1978, PersonalIdentificationNumber = "7860020422" },
                new() { Year = 2016, PersonalIdentificationNumber = "1560071314" },
                new() { Year = 1953, PersonalIdentificationNumber = "5312243761" },
            };

            return results;
        }
    }

    [Theory]
    [MemberData(nameof(InvalidScenarios))]
    public void ValidateModulo_WithInvalidModulo_ReturnsFalse(ModuloScenario moduloScenario)
    {
        // Arrange and Act
        var result = CzechPersonalIdentificationNumber.ValidateModulo(moduloScenario.Year, moduloScenario.PersonalIdentificationNumber);

        // Assert
        result.ShouldBeFalse();
    }

    #endregion ValidateModulo
}