using EnduranceJudge.Core.Exceptions;
using EnduranceJudge.Domain.Core.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace EnduranceJudge.Domain.Validation
{
    public static class DateTimeValidations
    {
        private const string InvalidBirthDateTemplate = "has invalid birth date: {0}";

        public static DateTime? IsFutureDate(this DateTime? dateTime)
        {
            return dateTime?.IsFutureDate();
        }

        public static DateTime IsFutureDate(this DateTime dateTime)
        {
            if (dateTime >= DateTime.Now)
            {
                throw new CoreException(InvalidBirthDateTemplate, dateTime);
            }

            return dateTime;
        }

        public static DateTime IsDateInTheFuture(this DateTime dateTime, string message)
        {
            if (dateTime <= DateTime.Now)
            {
                throw new CoreException(InvalidBirthDateTemplate, dateTime);
            }

            return dateTime;
        }
    }
}
