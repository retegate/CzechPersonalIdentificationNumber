using System.Reflection;

using Bogus;

using Retegate.CzechPersonalIdentificationNumber;

namespace Retegate.Bogus.CzechPersonalIdentificationNumber;

public static class FakerExtensions
{
    //todo: tests
    public static Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber
        GenerateCzechPersonalIdentificationNumber(this Faker faker,
            CzechPersonalIdentificationNumberKindEnum? type, FormatEnum format)
    {
        var delimiter = GetDelimiter(format);

        DateTime bottomDateLimit, upperDateLimit;
        DateOnly dateOfBirth;
        string personalIdentificationNumberFirstPart;
        switch (type)
        {
            case CzechPersonalIdentificationNumberKindEnum.ArbitraryPerson:
                return faker.GenerateCzechPersonalIdentificationNumber(
                    ChooseFromScenarios([ScenariosChoiceEnum.Female, ScenariosChoiceEnum.Male]), format);
            case CzechPersonalIdentificationNumberKindEnum.ArbitraryMale:
                return faker.GenerateCzechPersonalIdentificationNumber(ChooseFromScenarios([ScenariosChoiceEnum.Male]),
                    format);
            case CzechPersonalIdentificationNumberKindEnum.ArbitraryFemale:
                return faker.GenerateCzechPersonalIdentificationNumber(
                    ChooseFromScenarios([ScenariosChoiceEnum.Female]), format);
            case CzechPersonalIdentificationNumberKindEnum.MaleBefore1954:
                bottomDateLimit = GetBottomDateTimeLimit();
                upperDateLimit = DateTime.Parse("1954-01-01T00:00:00Z");
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                return new Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber(
                    $"{dateOfBirth:yyMMdd}{delimiter}{faker.Random.Number(0, 999):D3}", dateOfBirth, SexEnum.Male);
            case CzechPersonalIdentificationNumberKindEnum.FemaleBefore1954:
                bottomDateLimit = GetBottomDateTimeLimit();
                upperDateLimit = DateTime.Parse("1954-01-01T00:00:00Z");
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                return
                    new Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber(
                        $"{dateOfBirth:yy}{(dateOfBirth.Month + 50):D2}{dateOfBirth:dd}{delimiter}{faker.Random.Number(0, 999):D3}",
                        dateOfBirth, SexEnum.Female);

            case CzechPersonalIdentificationNumberKindEnum.Male1954AndLaterWithNoExceptionalRules:
                bottomDateLimit = DateTime.Parse("1954-01-01T00:00:00Z");
                upperDateLimit = GetUpperDateTimeLimit();
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                personalIdentificationNumberFirstPart = $"{dateOfBirth:yy}{(dateOfBirth.Month):D2}{dateOfBirth:dd}";
                return new Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber(
                    FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954(dateOfBirth.Year,
                        personalIdentificationNumberFirstPart, delimiter), dateOfBirth, SexEnum.Male);
            case CzechPersonalIdentificationNumberKindEnum.Female1954AndLaterWithNoExceptionalRules:
                bottomDateLimit = DateTime.Parse("1954-01-01T00:00:00Z");
                upperDateLimit = GetUpperDateTimeLimit();
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                personalIdentificationNumberFirstPart =
                    $"{dateOfBirth:yy}{(dateOfBirth.Month + 50):D2}{dateOfBirth:dd}";
                return new Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber(
                    FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954(dateOfBirth.Year,
                        personalIdentificationNumberFirstPart, delimiter), dateOfBirth, SexEnum.Female);

            case CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleMaleInPopulationBoom1974Till1985:
                bottomDateLimit = DateTime.Parse("1974-01-01T00:00:00Z");
                upperDateLimit = DateTime.Parse("1985-12-31T23:59:59Z");
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                personalIdentificationNumberFirstPart = $"{dateOfBirth:yy}{(dateOfBirth.Month):D2}{dateOfBirth:dd}";
                return new Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber(
                    FinishPersonalIdentificationNumberByGeneratingExceptionalVerificationNumberBetween1974And1985(
                        personalIdentificationNumberFirstPart, delimiter),
                    dateOfBirth, SexEnum.Male);
            case CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleFemaleInPopulationBoom1974Till1985:
                bottomDateLimit = DateTime.Parse("1974-01-01T00:00:00Z");
                upperDateLimit = DateTime.Parse("1985-12-31T23:59:59Z");
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                personalIdentificationNumberFirstPart =
                    $"{dateOfBirth:yy}{(dateOfBirth.Month + 50):D2}{dateOfBirth:dd}";

                return new Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber(
                    FinishPersonalIdentificationNumberByGeneratingExceptionalVerificationNumberBetween1974And1985(
                        personalIdentificationNumberFirstPart, delimiter),
                    dateOfBirth, SexEnum.Female);

            case CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleMaleInNewEraPopulationBoom2004:
                bottomDateLimit = DateTime.Parse("1974-01-01T00:00:00Z");
                upperDateLimit = DateTime.Parse("1985-12-31T23:59:59Z");
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                personalIdentificationNumberFirstPart =
                    $"{dateOfBirth:yy}{(dateOfBirth.Month + 20):D2}{dateOfBirth:dd}";
                return new Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber(
                    FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954(dateOfBirth.Year,
                        personalIdentificationNumberFirstPart, delimiter), dateOfBirth, SexEnum.Male);
            case CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleFemaleInNewEraPopulationBoom2004:
                bottomDateLimit = DateTime.Parse("1974-01-01T00:00:00Z");
                upperDateLimit = DateTime.Parse("1985-12-31T23:59:59Z");
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                personalIdentificationNumberFirstPart =
                    $"{dateOfBirth:yy}{(dateOfBirth.Month + 20 + 50):D2}{dateOfBirth:dd}";
                return new Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber(
                    FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954(dateOfBirth.Year,
                        personalIdentificationNumberFirstPart, delimiter), dateOfBirth, SexEnum.Female);
            default:
                throw new ArgumentOutOfRangeException(
                    $"Unknown {nameof(CzechPersonalIdentificationNumberKindEnum)} generation request value.");
        }
    }

    internal static CzechPersonalIdentificationNumberKindEnum ChooseFromScenarios(
        IEnumerable<ScenariosChoiceEnum> scenariosChoices)
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
        return format == FormatEnum.Normalized ? string.Empty : "/"; //todo: put this into some constant 
    }

    internal static DateTime GetBottomDateTimeLimit()
    {
        var now = DateTime.Now;
        var bottomYearLimit =
            Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.NineteenHundred + now.Year -
            Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.TwoThousand - 1;
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

    internal static string FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954(int year,
        string personalIdentificationNumberFirstPart, string delimiter)
    {
        while (true)
        {
            var initialControlNumberPivot = Random.Shared.Next(0, 9999 - 11);
            const int limitPivot = 10;
            int pivot;
            try
            {
                pivot = ComputeControlNumberLastDigitPivot(limitPivot, personalIdentificationNumberFirstPart,
                    initialControlNumberPivot);
            }
            catch (InvalidOperationException)
            {
                continue;
            }

            if (pivot != limitPivot)
            {
                return $"{personalIdentificationNumberFirstPart}{delimiter}{(initialControlNumberPivot + pivot):D4}";
            }
        }
    }

    internal static string
        FinishPersonalIdentificationNumberByGeneratingExceptionalVerificationNumberBetween1974And1985(
            string personalIdentificationNumberFirstPart,
            string delimiter)
    {
        while (true)
        {
            var initialControlNumberPivot = Random.Shared.Next(0, 9999 - 11);
            const int limitPivot = 10;
            int pivot;
            try
            {
                pivot = ComputeControlNumberLastDigitPivot(limitPivot, personalIdentificationNumberFirstPart,
                    initialControlNumberPivot);
            }
            catch (InvalidOperationException)
            {
                continue;
            }

            if (pivot != limitPivot)
            {
                continue;
            }

            var last3digits = initialControlNumberPivot.ToString("D4")[..^1];

            var result = $"{personalIdentificationNumberFirstPart}{delimiter}{last3digits}0";
            return result;
        }
    }

    internal static int ComputeControlNumberLastDigitPivot(int limitPivot, string personalIdentificationNumberFirstPart,
        int initialControlNumberPivot)
    {
        for (var i = 0; i <= limitPivot; i++)
        {
            var possiblePersonalIdentificationNumber =
                $"{personalIdentificationNumberFirstPart}{(initialControlNumberPivot + i):D4}";

            if (!Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.TryParse(
                    possiblePersonalIdentificationNumber, null, out _))
            {
                continue;
            }

            return i;
        }

        throw new InvalidOperationException(
            "Unable to generate a valid personal identification number (unexpected state).");
    }
}