using EnduranceJudge.Domain.Core.Exceptions;
using EnduranceJudge.Domain.Core.Validation;
using System;
using System.Linq;
using static EnduranceJudge.Domain.DomainConstants;

namespace EnduranceJudge.Domain.Validation
{
    public static class StringValidations
    {
        private const string Template = "has invalid name '{0}'. Must contain first and last name.";
        private const string PersonNameLabel = "Person name";
        private const string InvalidGenderTemplate = "has invalid gender: '{0}'";

        public static string CheckPersonName(this string text)
        {
            var name = text.IsRequired(PersonNameLabel);

            var parts = name.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            var firstName = parts.FirstOrDefault();
            var lastName = parts.LastOrDefault();

            if (parts.Length < 2 || string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new DomainException(Template, name);
            }

            return name;
        }

        public static string CheckValidGender(this string value)
        {
            if (value != DomainConstants.Gender.Female && value != DomainConstants.Gender.Male)
            {
                throw new DomainException(InvalidGenderTemplate, value);
            }

            return value;
        }
    }
}
