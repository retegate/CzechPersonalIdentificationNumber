using Bogus;

using FluentValidation;

using Retegate.Bogus.CzechPersonalIdentificationNumber.PartialGenerators;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.PartialValidationHypothesis;

using Xunit.Abstractions;

namespace Retegate.Bogus.CzechPersonalIdentificationNumber.Tests;

public class FakerExtensionsTests(ITestOutputHelper output)
{
    private readonly Faker _faker = new();

    private readonly Func<IRuleBuilder<TestModel, string>, IRuleBuilderOptions<TestModel, string>>[]
        _allValidationExtensionRules =
        [
            Female1954AndLaterCzechPersonalIdentificationNumberValidationExtension
                .Female1954AndLaterCzechPersonalIdentificationNumber,
            FemaleBefore1954CzechPersonalIdentificationNumberValidationExtension
                .FemaleBefore1954CzechPersonalIdentificationNumber,
            FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberValidationExtension
                .FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber,
            FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberValidationExtension
                .FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber,
            Male1954AndLaterCzechPersonalIdentificationNumberValidationExtension
                .Male1954AndLaterCzechPersonalIdentificationNumber,
            MaleBefore1954CzechPersonalIdentificationNumberValidationExtension
                .MaleBefore1954CzechPersonalIdentificationNumber,
            MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberValidationExtension
                .MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber,
            MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberValidationExtension
                .MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber,
        ];

    public sealed class TestScenario
    {
        public required Func<IRuleBuilder<TestModel, string>, IRuleBuilderOptions<TestModel, string>>[] ValidationRules
        {
            get;
            init;
        }

        public required Func<Faker, FormatEnum,
            Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber> Generator
        {
            get;
            init;
        }

        public required CzechPersonalIdentificationNumberKindEnum ExpectedCzechPersonalIdentificationNumberKind
        {
            get;
            init;
        }
    }

    public sealed class TestModel
    {
        public required string CzechPersonalIdentificationNumber { get; init; }
    }

    private sealed class TestValidator : AbstractValidator<TestModel>
    {
        public TestValidator(
            Func<IRuleBuilder<TestModel, string>, IRuleBuilderOptions<TestModel, string>> validationRule)
        {
            validationRule(RuleFor(x => x.CzechPersonalIdentificationNumber));
        }
    }

    [Fact]
    public void
        GenerateFemale1954AndLaterPersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator()
    {
        var scenario = new TestScenario()
        {
            Generator =
                Female1954AndLaterCzechPersonalIdentificationNumberFakerExtension
                    .GenerateFemale1954AndLaterCzechPersonalIdentificationNumber,
            ValidationRules =
            [
                Female1954AndLaterCzechPersonalIdentificationNumberValidationExtension
                    .Female1954AndLaterCzechPersonalIdentificationNumber
            ],
            ExpectedCzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum
                .Female1954AndLaterWithNoExceptionalRules,
        };
        GenerateSpecifiedPersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator(scenario);
    }

    [Fact]
    public void
        GenerateMale1954AndLaterPersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator()
    {
        var scenario = new TestScenario()
        {
            Generator =
                Male1954AndLaterCzechPersonalIdentificationNumberFakerExtension
                    .GenerateMale1954AndLaterCzechPersonalIdentificationNumber,
            ValidationRules =
            [
                Male1954AndLaterCzechPersonalIdentificationNumberValidationExtension
                    .Male1954AndLaterCzechPersonalIdentificationNumber
            ],
            ExpectedCzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum
                .Male1954AndLaterWithNoExceptionalRules,
        };
        GenerateSpecifiedPersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator(scenario);
    }

    [Fact]
    public void
        GenerateFemaleBefore1954PersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator()
    {
        var scenario = new TestScenario()
        {
            Generator =
                FemaleBefore1954CzechPersonalIdentificationNumberFakerExtension
                    .GenerateFemaleBefore1954CzechPersonalIdentificationNumber,
            ValidationRules =
            [
                FemaleBefore1954CzechPersonalIdentificationNumberValidationExtension
                    .FemaleBefore1954CzechPersonalIdentificationNumber
            ],
            ExpectedCzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum
                .FemaleBefore1954,
        };
        GenerateSpecifiedPersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator(scenario);
    }

    [Fact]
    public void
        GenerateMaleBefore1954PersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator()
    {
        var scenario = new TestScenario()
        {
            Generator =
                MaleBefore1954CzechPersonalIdentificationNumberFakerExtension
                    .GenerateMaleBefore1954CzechPersonalIdentificationNumber,
            ValidationRules =
            [
                MaleBefore1954CzechPersonalIdentificationNumberValidationExtension
                    .MaleBefore1954CzechPersonalIdentificationNumber
            ],
            ExpectedCzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum
                .MaleBefore1954,
        };
        GenerateSpecifiedPersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator(scenario);
    }

    [Fact]
    public void
        GenerateFemaleWithExceptionalVerificationNumberBetween1974And1985PersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator()
    {
        var scenario = new TestScenario()
        {
            Generator =
                FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberFakerExtension
                    .GenerateFemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentification,
            ValidationRules =
            [
                FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberValidationExtension
                    .FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber
            ],
            ExpectedCzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum
                .ExceptionalRuleFemaleInPopulationBoom1974Till1985,
        };
        GenerateSpecifiedPersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator(scenario);
    }

    [Fact]
    public void
        GenerateMaleWithExceptionalVerificationNumberBetween1974And1985PersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator()
    {
        var scenario = new TestScenario()
        {
            Generator =
                MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberFakerExtension
                    .GenerateMaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber,
            ValidationRules =
            [
                MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberValidationExtension
                    .MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber
            ],
            ExpectedCzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum
                .ExceptionalRuleMaleInPopulationBoom1974Till1985,
        };
        GenerateSpecifiedPersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator(scenario);
    }

    [Fact]
    public void
        GenerateFemaleWithExceptionalMonthRule2004AndAfterPersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator()
    {
        var scenario = new TestScenario()
        {
            Generator =
                FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberFakerExtension
                    .GenerateFemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber,
            ValidationRules =
            [
                FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberValidationExtension
                    .FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber
            ],
            ExpectedCzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum
                .ExceptionalRuleFemaleInNewEraPopulationBoom2004,
        };
        GenerateSpecifiedPersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator(scenario);
    }

    [Fact]
    public void
        GenerateMaleWithExceptionalMonthRule2004AndAfterPersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator()
    {
        var scenario = new TestScenario()
        {
            Generator =
                MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberFakerExtension
                    .GenerateMaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber,
            ValidationRules =
            [
                MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberValidationExtension
                    .MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber
            ],
            ExpectedCzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum
                .ExceptionalRuleMaleInNewEraPopulationBoom2004,
        };
        GenerateSpecifiedPersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator(scenario);
    }

    [Fact]
    public void GenerateFemalePersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator()
    {
        var scenario = new TestScenario()
        {
            Generator = FemaleFakerExtension.GenerateFemaleCzechPersonalIdentificationNumber,
            ValidationRules =
            [
                Female1954AndLaterCzechPersonalIdentificationNumberValidationExtension
                    .Female1954AndLaterCzechPersonalIdentificationNumber,
                FemaleBefore1954CzechPersonalIdentificationNumberValidationExtension
                    .FemaleBefore1954CzechPersonalIdentificationNumber,
                FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberValidationExtension
                    .FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber,
                FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberValidationExtension
                    .FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber
            ],
            ExpectedCzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum
                .ArbitraryFemale,
        };
        GenerateSpecifiedPersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator(scenario);
    }

    [Fact]
    public void GenerateMalePersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator()
    {
        var scenario = new TestScenario()
        {
            Generator = MaleFakerExtension.GenerateMaleCzechPersonalIdentificationNumber,
            ValidationRules =
            [
                Male1954AndLaterCzechPersonalIdentificationNumberValidationExtension
                    .Male1954AndLaterCzechPersonalIdentificationNumber,
                MaleBefore1954CzechPersonalIdentificationNumberValidationExtension
                    .MaleBefore1954CzechPersonalIdentificationNumber,
                MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberValidationExtension
                    .MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber,
                MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberValidationExtension
                    .MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber
            ],
            ExpectedCzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum
                .ArbitraryMale,
        };
        GenerateSpecifiedPersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator(scenario);
    }

    [Fact]
    public void GenerateArbitraryPersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator()
    {
        var scenario = new TestScenario()
        {
            Generator = ArbitraryPersonCzechPersonalIdentificationNumberFakerExtension
                .GenerateArbitraryPersonCzechPersonalIdentificationNumber,
            ValidationRules =
            [
                Female1954AndLaterCzechPersonalIdentificationNumberValidationExtension
                    .Female1954AndLaterCzechPersonalIdentificationNumber,
                FemaleBefore1954CzechPersonalIdentificationNumberValidationExtension
                    .FemaleBefore1954CzechPersonalIdentificationNumber,
                FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberValidationExtension
                    .FemaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber,
                FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberValidationExtension
                    .FemaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber,
                MaleBefore1954CzechPersonalIdentificationNumberValidationExtension
                    .MaleBefore1954CzechPersonalIdentificationNumber,
                MaleBefore1954CzechPersonalIdentificationNumberValidationExtension
                    .MaleBefore1954CzechPersonalIdentificationNumber,
                MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumberValidationExtension
                    .MaleWithExceptionalModuloRuleBetween1974And1985CzechPersonalIdentificationNumber,
                MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberValidationExtension
                    .MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber
            ],
            ExpectedCzechPersonalIdentificationNumberKind = CzechPersonalIdentificationNumberKindEnum
                .ArbitraryPerson,
        };
        GenerateSpecifiedPersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator(scenario);
    }

    private void GenerateSpecifiedPersonalIdentificationNumber_WithNoExceptionalRules_ShouldMatchDedicatedValidator(
        TestScenario testScenario)
    {
        // Arrange and Act
        var value1 = testScenario.Generator(_faker, FormatEnum.Normalized);
        var value2 =
            _faker.GenerateCzechPersonalIdentificationNumber(testScenario.ExpectedCzechPersonalIdentificationNumberKind,
                FormatEnum.Normalized);

        // Assert
        // positive - custom extension method generation
        var validationResults1 = new List<bool>();
        var validationResults2 = new List<bool>();
        foreach (var validationRule in testScenario.ValidationRules)
        {
            var validator1 = new TestValidator(validationRule);
            var result1 = validator1.Validate(new TestModel
            {
                CzechPersonalIdentificationNumber = value1.NormalizedCzechNormalizedPersonalIdentificationNumber
            });
            validationResults1.Add(result1.IsValid);

            // positive - generation via scenario enum
            var validator2 = new TestValidator(validationRule);
            var result2 = validator2.Validate(new TestModel
            {
                CzechPersonalIdentificationNumber = value2.NormalizedCzechNormalizedPersonalIdentificationNumber
            });
            validationResults2.Add(result2.IsValid);
        }
        
        output.WriteLine(value1.NormalizedCzechNormalizedPersonalIdentificationNumber);
        output.WriteLine(value2.NormalizedCzechNormalizedPersonalIdentificationNumber);

        validationResults1.Where(vr => vr).ShouldHaveSingleItem();
        validationResults2.Where(vr => vr).ShouldHaveSingleItem();

        //negative
        var scenarioTypes = testScenario.ValidationRules
            .Select(vr => vr.GetType())
            .ToHashSet();
        foreach (var validationExtensionRule in _allValidationExtensionRules)
        {
            var validationExtensionRuleType = validationExtensionRule.GetType();
            if (scenarioTypes.Contains(validationExtensionRuleType))
            {
                continue;
            }

            var validator3 = new TestValidator(validationExtensionRule);
            var result3 = validator3.Validate(new TestModel
            {
                CzechPersonalIdentificationNumber = value1.NormalizedCzechNormalizedPersonalIdentificationNumber
            });
            result3.IsValid.ShouldBeFalse();
        }
    }
}