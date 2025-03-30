using FluentValidation;
using FluentValidation.Results;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator.PartialValidationHypothesis;

public static class MaleBefore1954CzechPersonalIdentificationNumberValidationExtension 
{
    /// <summary>
    /// Validation method of the czech personal identification number.
    /// </summary>
    /// <param name="ruleBuilder"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>
    /// Possible validation failures are:
    ///<para><see cref="Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.NullInputFormatMessage"/></para>
    ///<para><see cref="Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidFormatMessage"/></para>
    ///<para><see cref="Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidYearMessage"/></para>
    ///<para><see cref="Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidMaleMonthMessage"/></para>
    ///<para><see cref="Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidFemaleMonthMessage"/></para>
    ///<para><see cref="Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidDayMessage"/></para>
    ///<para><see cref="Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.InvalidVerificationNumberMessage"/></para>
    /// </returns>
    public static IRuleBuilderOptions<T, string> MaleBefore1954CzechPersonalIdentificationNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        var result = ruleBuilder
            .Custom((personalIdentificationNumber, context) =>
            {
                try
                {
                    _ = CzechPersonalIdentificationNumber.ParserHypothesis.MaleBefore1954CzechPersonalIdentificationNumber.Parse(personalIdentificationNumber);
                }
                catch (FormatException e)
                {
                    context.AddFailure(new ValidationFailure(context.PropertyPath, e.Message));
                }
            });

        return (result as IRuleBuilderOptions<T, string>)!;
    }
}