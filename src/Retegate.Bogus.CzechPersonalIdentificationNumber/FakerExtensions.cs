using System.Reflection;

using Bogus;

using Retegate.CzechPersonalIdentificationNumber;
using Retegate.CzechPersonalIdentificationNumber.ParserHypothesis;

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
                    FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954<
                        Male1954AndLaterCzechPersonalIdentificationNumber>(
                        personalIdentificationNumberFirstPart, delimiter), dateOfBirth, SexEnum.Male);
            case CzechPersonalIdentificationNumberKindEnum.Female1954AndLaterWithNoExceptionalRules:
                bottomDateLimit = DateTime.Parse("1954-01-01T00:00:00Z");
                upperDateLimit = GetUpperDateTimeLimit();
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                personalIdentificationNumberFirstPart =
                    $"{dateOfBirth:yy}{(dateOfBirth.Month + 50):D2}{dateOfBirth:dd}";
                return new Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber(
                    FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954<
                        Female1954AndLaterCzechPersonalIdentificationNumber>(
                        personalIdentificationNumberFirstPart, delimiter), dateOfBirth, SexEnum.Female);

            case CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleMaleInPopulationBoom1974Till1985:
                bottomDateLimit = DateTime.Parse("1974-01-01T00:00:00Z");
                upperDateLimit = DateTime.Parse("1985-12-31T23:59:59Z");
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                personalIdentificationNumberFirstPart = $"{dateOfBirth:yy}{(dateOfBirth.Month):D2}{dateOfBirth:dd}";
                return new Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber(
                    FinishPersonalIdentificationNumberByGeneratingExceptionalVerificationNumberBetween1974And1985<
                        MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber>(
                        personalIdentificationNumberFirstPart, delimiter),
                    dateOfBirth, SexEnum.Male);
            case CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleFemaleInPopulationBoom1974Till1985:
                bottomDateLimit = DateTime.Parse("1974-01-01T00:00:00Z");
                upperDateLimit = DateTime.Parse("1985-12-31T23:59:59Z");
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                personalIdentificationNumberFirstPart =
                    $"{dateOfBirth:yy}{(dateOfBirth.Month + 50):D2}{dateOfBirth:dd}";

                return new Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber(
                    FinishPersonalIdentificationNumberByGeneratingExceptionalVerificationNumberBetween1974And1985<
                        FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber>(
                        personalIdentificationNumberFirstPart, delimiter),
                    dateOfBirth, SexEnum.Female);

            case CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleMaleInNewEraPopulationBoom2004:
                bottomDateLimit = DateTime.Parse("1974-01-01T00:00:00Z");
                upperDateLimit = DateTime.Parse("1985-12-31T23:59:59Z");
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                personalIdentificationNumberFirstPart =
                    $"{dateOfBirth:yy}{(dateOfBirth.Month + 20):D2}{dateOfBirth:dd}";
                return new Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber(
                    FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954<
                        MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber>(
                        personalIdentificationNumberFirstPart, delimiter), dateOfBirth, SexEnum.Male);
            case CzechPersonalIdentificationNumberKindEnum.ExceptionalRuleFemaleInNewEraPopulationBoom2004:
                bottomDateLimit = DateTime.Parse("1974-01-01T00:00:00Z");
                upperDateLimit = DateTime.Parse("1985-12-31T23:59:59Z");
                dateOfBirth = DateOnly.FromDateTime(faker.Date.Between(bottomDateLimit, upperDateLimit));
                personalIdentificationNumberFirstPart =
                    $"{dateOfBirth:yy}{(dateOfBirth.Month + 20 + 50):D2}{dateOfBirth:dd}";
                return new Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber(
                    FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954<
                        FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber>(
                        personalIdentificationNumberFirstPart, delimiter), dateOfBirth, SexEnum.Female);
            default:
                throw new ArgumentOutOfRangeException(
                    $"Unknown {nameof(CzechPersonalIdentificationNumberKindEnum)} generation request value.");
        }
    }

    internal static CzechPersonalIdentificationNumberKindEnum ChooseFromScenarios(
        IEnumerable<ScenariosChoiceEnum> scenariosChoices)
    {
        var scenariosChoiceEnums = scenariosChoices.ToList();
        if (scenariosChoices == null || !scenariosChoiceEnums.Any())
        {
            throw new ArgumentNullException(nameof(scenariosChoices), "Scenarios choices cannot be null or empty.");
        }

        var chosenScenariosRange = typeof(CzechPersonalIdentificationNumberKindEnum)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(field =>
            {
                var customAttributes = field.GetCustomAttribute<SexMappingAttribute>();
                return customAttributes is not null && scenariosChoiceEnums.Contains(customAttributes!.ScenariosChoice);
            })
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
        return DateTime.Now.AddYears(-100).AddDays(1d);
    }

    internal static DateTime GetUpperDateTimeLimit()
    {
        return DateTime.Now;
    }

    internal static string FinishPersonalIdentificationNumberByGeneratingProperVerificationNumberAfter1954<TParser>(
        string personalIdentificationNumberFirstPart,
        string delimiter)
        where TParser : class, IParsable<TParser>
    {
        while (true)
        {
            var initialControlNumberPivot = Random.Shared.Next(0, 999 - 11);
            const int limitPivot = 10;
            int pivot;
            try
            {
                pivot = ComputeControlNumberLastDigitPivotForPersonsAfter1954<TParser>(limitPivot,
                    personalIdentificationNumberFirstPart,
                    initialControlNumberPivot);
            }
            catch (InvalidOperationException)
            {
                continue;
            }

            if (pivot != limitPivot)
            {
                return $"{personalIdentificationNumberFirstPart}{delimiter}{initialControlNumberPivot:D3}{pivot}";
            }
        }
    }

    internal static string
        FinishPersonalIdentificationNumberByGeneratingExceptionalVerificationNumberBetween1974And1985<TParser>(
            string personalIdentificationNumberFirstPart,
            string delimiter)
        where TParser : class, IParsable<TParser>
    {
        while (true)
        {
            var initialControlNumberPivot = Random.Shared.Next(0, 999 - 11);
            var possiblePersonalIdentificationNumber =
                $"{personalIdentificationNumberFirstPart}{initialControlNumberPivot:D3}10";
            if (ulong.Parse(possiblePersonalIdentificationNumber) % 11 != 0)
            {
                continue;
            }

            var result = $"{personalIdentificationNumberFirstPart}{delimiter}{initialControlNumberPivot:D3}0";
            return result;
        }
    }

    internal static int ComputeControlNumberLastDigitPivotForPersonsAfter1954<TParser>(
        int limitPivot,
        string personalIdentificationNumberFirstPart,
        int initialControlNumberPivot)
        where TParser : class, IParsable<TParser>
    {
        for (var i = 0; i <= limitPivot; i++)
        {
            var possiblePersonalIdentificationNumber =
                $"{personalIdentificationNumberFirstPart}{(initialControlNumberPivot):D3}{i}";

            if (long.Parse(possiblePersonalIdentificationNumber) % 11 != 0)
            {
                continue;
            }

            return i;
        }

        throw new InvalidOperationException(
            "Unable to generate a valid personal identification number (unexpected state).");
    }
}