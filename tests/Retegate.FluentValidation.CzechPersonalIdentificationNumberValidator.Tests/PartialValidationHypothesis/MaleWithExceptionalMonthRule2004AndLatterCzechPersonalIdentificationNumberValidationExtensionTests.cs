using FluentValidation;

using Retegate.CzechPersonalIdentificationNumber.Tests.ParserHypothesis.Scenarios;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.PartialValidationHypothesis;
using Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis.Base;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.Tests.PartialValidationHypothesis;

public sealed class MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberValidationExtensionTests()
    : ValidationTestsBase<MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberValidationExtensionTests.TestValidator,
            TestModel, MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberScenarios>(
            new TestValidator(),
            x => new TestModel { PersonalIdentificationNumber = x }),
        ITestScenarios<MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberScenarios>
{
    public class TestValidator : AbstractValidator<TestModel>
    {
        public TestValidator()
        {
            RuleFor(x => x.PersonalIdentificationNumber)
                .MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumber();
        }
    }

    public static MaleWithExceptionalMonthRule2004AndLatterCzechPersonalIdentificationNumberScenarios TestScenarios =>
        new();
}