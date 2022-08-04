using LexiconUniversity.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace LexiconUniversity.Web.Validations
{
    public class KalleAnka : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            var errorMessage = "bal bla";

            if (value is string input)
            {
                //var input = value as string;
                var viewModel = validationContext.ObjectInstance as StudentCreateViewModel;
                //var db = validationContext.GetService(typeof(LexiconUniversityContext)) as LexiconUniversityContext;

                if (viewModel is not null)
                {
                    if (viewModel.FirstName == "Kalle" && input == "Anka")
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult(ErrorMessage);
                    }
                }
            }


            return new ValidationResult(ErrorMessage);
        }

    }
}
