using System.Reflection;

using Bogus;

using PersonalIdentificationNumber = Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber;

namespace Retegate.Bogus.CzechPersonalIdentificationNumber;

public static class FakerExtensions
{
    //todo: tests
    public static string GenerateCzechPersonalIdentificationNumber(this Faker faker, CzechPersonalIdentificationNumberKindEnum? type, FormatEnum format)
    {
        var delimiter = GetDelimiter(format);

        DateTime bottomDateLimit, upperDateLimit;
        DateOnly dateOfBirth;
        string personalIdentificationNumberFirstPart;
        switch (type)
        {
            case CzechPersonalIdentificationNumberKindEnum.ArbitraryPerson:
                return faker.GenerateCzechPersonalIdentificationNumber(ChooseFromScenarios([ScenariosChoiceEnum.Female, ScenariosChoiceEnum.Male]), format);
            case CzechPersonalIdentificationNumberKindEnum.ArbitraryMale:
                return faker.GenerateCzechPersonalIdentificationNumber(ChooseFromScenarios([ScenariosChoiceEnum.Male]), format);
            case CzechPersonalIdentificationNumberKindEnum.ArbitraryFemale:
                return faker.GenerateCzechPersonalIdentificationNumber(ChooseFromScenarios([ScenariosChoiceEnum.Female]), format);
            case CzechPersonalIdentificationNumberKindEnum.MaleBefore1954:
                bottomDateLimit = GetBottomDateTimeLimit();
                upperDateLimit = DateTime.Parse("1954-01-01T00:00:00Z");
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                return $"{dateOfBirth:yyMMdd}{delimiter}{faker.Random.Number(0, 999):D3}";
            case CzechPersonalIdentificationNumberKindEnum.FemaleBefore1954:
                bottomDateLimit = GetBottomDateTimeLimit();
                upperDateLimit = DateTime.Parse("1954-01-01T00:00:00Z");
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                return $"{dateOfBirth:yy}{(dateOfBirth.Month + 50):D2}{dateOfBirth:dd}{delimiter}{faker.Random.Number(0, 999):D3}";

            case CzechPersonalIdentificationNumberKindEnum.MaleAfter1954:
                bottomDateLimit = DateTime.Parse("1954-01-01T00:00:00Z");
                upperDateLimit = GetUpperDateTimeLimit();
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                personalIdentificationNumberFirstPart = $"{dateOfBirth:yy}{(dateOfBirth.Month):D2}{dateOfBirth:dd}";
                return FinishPersonalIdentificationNumberByGeneratingProperControlNumberAfter1954(dateOfBirth.Year, personalIdentificationNumberFirstPart, delimiter);
            case CzechPersonalIdentificationNumberKindEnum.FemaleAfter1954:
                bottomDateLimit = DateTime.Parse("1954-01-01T00:00:00Z");
                upperDateLimit = GetUpperDateTimeLimit();
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                personalIdentificationNumberFirstPart = $"{dateOfBirth:yy}{(dateOfBirth.Month + 50):D2}{dateOfBirth:dd}";
                return FinishPersonalIdentificationNumberByGeneratingProperControlNumberAfter1954(dateOfBirth.Year, personalIdentificationNumberFirstPart, delimiter);

            case CzechPersonalIdentificationNumberKindEnum.ExceptionalMaleInPopulationBoom1974Till1985:
                bottomDateLimit = DateTime.Parse("1974-01-01T00:00:00Z");
                upperDateLimit = DateTime.Parse("1985-12-31T23:59:59Z");
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                return $"{dateOfBirth:yy}{(dateOfBirth.Month):D2}{dateOfBirth:dd}{delimiter}{faker.Random.Number(1, 9999):D4}";
            case CzechPersonalIdentificationNumberKindEnum.ExceptionalFemaleInPopulationBoom1974Till1985:
                bottomDateLimit = DateTime.Parse("1974-01-01T00:00:00Z");
                upperDateLimit = DateTime.Parse("1985-12-31T23:59:59Z");
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                return $"{dateOfBirth:yy}{(dateOfBirth.Month + 50):D2}{dateOfBirth:dd}{delimiter}{faker.Random.Number(1, 9999):D4}";

            case CzechPersonalIdentificationNumberKindEnum.ExceptionalMaleInNewEraPopulationBoom2004:
                bottomDateLimit = DateTime.Parse("1974-01-01T00:00:00Z");
                upperDateLimit = DateTime.Parse("1985-12-31T23:59:59Z");
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                personalIdentificationNumberFirstPart = $"{dateOfBirth:yy}{(dateOfBirth.Month + 2):D2}{dateOfBirth:dd}";
                return FinishPersonalIdentificationNumberByGeneratingProperControlNumberAfter1954(dateOfBirth.Year, personalIdentificationNumberFirstPart, delimiter);
            case CzechPersonalIdentificationNumberKindEnum.ExceptionalFemaleInNewEraPopulationBoom2004:
                bottomDateLimit = DateTime.Parse("1974-01-01T00:00:00Z");
                upperDateLimit = DateTime.Parse("1985-12-31T23:59:59Z");
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                personalIdentificationNumberFirstPart = $"{dateOfBirth:yy}{(dateOfBirth.Month + 20 + 50):D2}{dateOfBirth:dd}";
                return FinishPersonalIdentificationNumberByGeneratingProperControlNumberAfter1954(dateOfBirth.Year, personalIdentificationNumberFirstPart, delimiter);
            default:
                throw new ArgumentOutOfRangeException($"Unknown {nameof(CzechPersonalIdentificationNumberKindEnum)} generation request value.");
        }
    }

    internal static CzechPersonalIdentificationNumberKindEnum ChooseFromScenarios(IEnumerable<ScenariosChoiceEnum> scenariosChoices)
    {
        var chosenScenariosRange = typeof(CzechPersonalIdentificationNumberKindEnum)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(field => scenariosChoices.Contains(field.GetCustomAttribute<SexMappingAttribute>()!.ScenariosChoice))
            .Select(field => (CzechPersonalIdentificationNumberKindEnum)field.GetValue(null)!)
            .ToList();

        var chosenScenariosIndex = Random.Shared.Next(chosenScenariosRange.Count);

        return chosenScenariosRange[chosenScenariosIndex];
    }

    internal static string GetDelimiter(FormatEnum format)
    {
        return format == FormatEnum.Normalized ? string.Empty : "/";
    }

    internal static DateTime GetBottomDateTimeLimit()
    {
        var now = DateTime.Now;
        var bottomYearLimit = PersonalIdentificationNumber.NineteenHundred + now.Year - PersonalIdentificationNumber.TwoThousand - 1;
        var bottomDateLimit = new DateTime(bottomYearLimit, now.Month, now.Day).AddDays(-1d);
        return bottomDateLimit;
    }

    internal static DateTime GetUpperDateTimeLimit()
    {
        return DateTime.Now;
    }

    internal static DateOnly GenerateDateOfBirth(Faker faker, DateTime bottomDateLimit, DateTime upperDateLimit)
    {
        return DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
    }

    internal static string FinishPersonalIdentificationNumberByGeneratingProperControlNumberAfter1954(int year, string personalIdentificationNumberFirstPart, string delimiter)
    {
        const int maxAttempts = 100;
        var attempts = 0;
        while (attempts < maxAttempts)
        {
            ++attempts;
            var initialControlNumberPivot = Random.Shared.Next(0, 9999 - 11);
            const int limitPivot = 10;
            var pivot = ComputeControlNumberLastDigitPivot(limitPivot, personalIdentificationNumberFirstPart, initialControlNumberPivot);

            if (pivot != limitPivot)
            {
                return $"{personalIdentificationNumberFirstPart}{delimiter}{initialControlNumberPivot:D4}";
            }

            if (year >= PersonalIdentificationNumber.PopulationBoomEndYear) //i.e. the control number has 5 digits and this is no longer valid from this year after
            {
                continue; //new attempty
            }

            var last3digits = initialControlNumberPivot.ToString("D4")[..^1];

            return $"{personalIdentificationNumberFirstPart}{delimiter}{last3digits}0";
        }

        throw new InvalidOperationException("Unable to generate a valid personal identification number.");
    }

    internal static int ComputeControlNumberLastDigitPivot(int limitPivot, string personalIdentificationNumberFirstPart, int initialControlNumberPivot)
    {
        for (var i = 0; i <= limitPivot; i++)
        {
            var possiblePersonalIdentificationNumber = $"{personalIdentificationNumberFirstPart}{(initialControlNumberPivot + i):D4}";

            if (!PersonalIdentificationNumber.TryParse(possiblePersonalIdentificationNumber, null, out _))
            {
                continue;
            }

            return i;
        }

        throw new InvalidOperationException("Unable to generate a valid personal identification number (unexpected state).");
    }
}