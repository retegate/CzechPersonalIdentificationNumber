using System.Reflection;

using Bogus;

using PersonalIdentificationNumber = Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber;

namespace Retegate.Bogus.CzechPersonalIdentificationNumber;

public static class FakerExtensions
{
    //todo: tests
    public static string GenerateCustomString(this Faker faker, CzechPersonalIdentificationNumberKindEnum? type, FormatEnum format)
    {
        var delimiter = GetDelimiter(format);

        DateTime bottomDateLimit, upperDateLimit;
        DateOnly dateOfBirth;
        string personalIdentificationNumberFirstPart;
        switch (type)
        {
            case CzechPersonalIdentificationNumberKindEnum.ArbitraryPerson:
                return faker.GenerateCustomString(ChooseFromScenarios([ScenariosChoiceEnum.Female, ScenariosChoiceEnum.Male]), format);
            case CzechPersonalIdentificationNumberKindEnum.ArbitraryMale:
                return faker.GenerateCustomString(ChooseFromScenarios([ScenariosChoiceEnum.Male]), format);
            case CzechPersonalIdentificationNumberKindEnum.ArbitraryFemale:
                return faker.GenerateCustomString(ChooseFromScenarios([ScenariosChoiceEnum.Female]), format);
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
        var bottomYearLimit = PersonalIdentificationNumber.NineteenHundred + DateTime.Now.Year - PersonalIdentificationNumber.TwoThousand - 1;
        var bottomDateLimit = new DateTime(bottomYearLimit, 1, 1);
        return bottomDateLimit;
    }

    internal static DateTime GetUpperDateTimeLimit()
    {
        var upperYearLimit = DateTime.Now.Year;
        var upperDateLimit = new DateTime(upperYearLimit, 12, 31);
        return upperDateLimit;
    }

    internal static DateOnly GenerateDateOfBirth(Faker faker, DateTime bottomDateLimit, DateTime upperDateLimit)
    {
        return DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
    }

    internal static string FinishPersonalIdentificationNumberByGeneratingProperControlNumberAfter1954(int year, string personalIdentificationNumberFirstPart, string delimiter)
    {
        while (true)
        {
            var initialControlNumberPivot = Random.Shared.Next(0, 9999 - 11);
            var pivot = -1;
            const int limitPivot = 10;
            for (var i = 0; i <= limitPivot; i++)
            {
                var possiblePersonalIdentificationNumber = $"{personalIdentificationNumberFirstPart}{(initialControlNumberPivot + i):D4}";

                if (!PersonalIdentificationNumber.TryParse(possiblePersonalIdentificationNumber, null, out _))
                {
                    continue;
                }

                pivot = i;
                break;
            }

            if (year >= PersonalIdentificationNumber.PopulationBoomEndYear)
            {
                continue;
            }

            if (pivot == limitPivot)
            {
                pivot = 0;
            }

            return $"{personalIdentificationNumberFirstPart}{delimiter}{(initialControlNumberPivot + pivot):D4}";
        }
    }
}