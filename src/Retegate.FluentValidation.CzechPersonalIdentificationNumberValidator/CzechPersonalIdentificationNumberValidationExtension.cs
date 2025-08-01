using FluentValidation;
using FluentValidation.Results;

namespace Retegate.FluentValidation.CzechPersonalIdentificationNumberValidator;

/// <summary>
/// Based on the czech act 133/2000 from the czech collection of law. 
/// </summary>
public static class CzechPersonalIdentificationNumberValidationExtension
{
//todo: udělat test, že jde přepsat with messsage v return textu...fv dá jednu chybu a případně parser jich umí více podat a jsou posané pro upřesnění....ta
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
    public static IRuleBuilderOptions<T, string> CzechPersonalIdentificationNumber<T>(
        this IRuleBuilder<T, string> ruleBuilder)
        where T : class
    {
        var result = ruleBuilder
            .Custom((personalIdentificationNumber, context) =>
            {
                try
                {
                    _ = Retegate.CzechPersonalIdentificationNumber.CzechPersonalIdentificationNumber.Parse(
                        personalIdentificationNumber);
                }
                catch (FormatException e)
                {
                    context.AddFailure(new ValidationFailure(context.PropertyPath, e.Message));
                }
            });

        return (result as IRuleBuilderOptions<T, string>)!;
    }
}