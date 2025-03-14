namespace Retegate.CzechPersonalIdentificationNumber.Tests;

public static class CzechPersonalIdentificationNumberFixture
{
    public class ValidPersonalIdentificationNumberTestCase(string personalIdentificationNumber, DateOnly dateOfBirth, SexEnum sex, string testCaseName)
    {
        public string PersonalIdentificationNumber { get; } = personalIdentificationNumber;
        public DateOnly DateOfBirth { get; } = dateOfBirth;
        public SexEnum Sex { get; } = sex;
        public string PersonalIdentificationNumberWithSlash => $"{PersonalIdentificationNumber.Replace("/", string.Empty).Replace(" ", string.Empty)[..6]}/{PersonalIdentificationNumber.Replace("/", string.Empty).Replace(" ", string.Empty)[6..]}";
        public string NormalizedPersonalIdentificationNumber => PersonalIdentificationNumber.Replace("/", string.Empty).Replace(" ", string.Empty);

        public override string ToString()
        {
            return testCaseName;
        }
    }

    public static TheoryData<ValidPersonalIdentificationNumberTestCase> ValidPersonalIdentificationNumbers
    {
        get
        {
            var results = new TheoryData<ValidPersonalIdentificationNumberTestCase>
            {
                new("181124/0805", new DateOnly(2018, 11, 24), SexEnum.Male, "Standard male personal identification number after 2004 with slash"),
                new("1811240805", new DateOnly(2018, 11, 24), SexEnum.Male, "Standard male personal identification number after 2004 without delimiter"),
                new("181124/ 0805", new DateOnly(2018, 11, 24), SexEnum.Male, "Standard male personal identification number after 2004 with delimiter and space at the delimiter end"),
                new("181124 /0805", new DateOnly(2018, 11, 24), SexEnum.Male, "Standard male personal identification number after 2004 with delimiter and space at the delimiter start"),
                new("181124 / 0805", new DateOnly(2018, 11, 24), SexEnum.Male, "Standard male personal identification number after 2004 with delimiter and space at both delimiter ends"),
                new("181124 0805", new DateOnly(2018, 11, 24), SexEnum.Male, "Standard male personal identification number after 2004 with space instead of delimiter"),
                new("960215 / 6014", new DateOnly(1996, 02, 15), SexEnum.Male, "Standard male personal identification number before 2004 with slash and spaces at the both delimiter ends"),
                new("125809 / 9447  ", new DateOnly(2012, 08, 09), SexEnum.Female, "Standard female personal identification number after 2004 with slash and spaces at the both delimiter ends and at the end of the string"),
                new("995325/6104", new DateOnly(1999, 03, 25), SexEnum.Female, "Standard female personal identification number before 2004 with slash and without spaces"),
                new("535325/627", new DateOnly(1953, 03, 25), SexEnum.Female, "Standard female personal identification number before 1954 with slash"),
                new("530325 / 223", new DateOnly(1953, 03, 25), SexEnum.Male, "Standard male personal identification number before 1954 with slash and spaces at the both delimiter ends"),
                new(" 057308 / 5502", new DateOnly(2005, 03, 08), SexEnum.Female, "Exceptional female personal identification number with too many children born at the day"),
                new("052308 / 4507 ", new DateOnly(2005, 03, 08), SexEnum.Male, "Exceptional male personal identification number with too many children born at the day"),
                new("800107 / 1440 ", new DateOnly(1980, 01, 07), SexEnum.Male, "Exceptional male personal identification numberwith modulo not 0, but last digit is 0 due to fit last digit needed to be 10 which cannot be due to the rules (used between years 1974-1985)"),
                new("  805101 / 1440", new DateOnly(1980, 01, 01), SexEnum.Female, "Exceptional female personal identification number with modulo not 0, but last digit is 0 due to fit last digit needed to be 10 which cannot be due to the rules (used between years 1974-1985)"),
            };

            return results;
        }
    }

    public static TheoryData<string> InvalidPersonalIdentificationNumberPattern
    {
        get
        {
            var results = new TheoryData<string>()
            {
                "18112 / 0805",
                "181124 / 08055",
                "181/124 / 0805 ",
                "a81124 / 0805 ",
                "181124 / 080e ",
                "1811",
                "18116789322",
            };

            return results;
        }
    }

    public static TheoryData<string> InvalidPersonalIdentificationNumberMonth
    {
        get
        {
            var results = new TheoryData<string>()
            {
                "189124 / 0805",
                "180024 / 0805",
                "183324 / 0805",
                "184241 / 0805",
                "186341 / 0805",
                "188324 / 0805",
            };

            return results;
        }
    }

    public static TheoryData<string> InvalidPersonalIdentificationNumberDay
    {
        get
        {
            var results = new TheoryData<string>() { "181100 / 0805", "181132 / 0805", "010229 / 0805", };

            return results;
        }
    }

    public static TheoryData<string> InvalidPersonalIdentificationNumberModulo
    {
        get
        {
            var results = new TheoryData<string>()
            {
                "771224123", "5312241234", "1811240804", "1811240806",
            };

            return results;
        }
    }

    public static TheoryData<string> InvalidOverallGroupedPersonalIdentificationNumbers
    {
        get
        {
            var results = new TheoryData<string>();

            foreach (var scenario in InvalidPersonalIdentificationNumberPattern)
            {
                results.Add(scenario);
            }

            foreach (var scenario in InvalidPersonalIdentificationNumberMonth)
            {
                results.Add(scenario);
            }

            foreach (var scenario in InvalidPersonalIdentificationNumberDay)
            {
                results.Add(scenario);
            }

            foreach (var scenario in InvalidPersonalIdentificationNumberModulo)
            {
                results.Add(scenario);
            }

            return results;
        }
    }
}