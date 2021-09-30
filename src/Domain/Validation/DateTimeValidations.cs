using EnduranceJudge.Core.Exceptions;
using System;
using static EnduranceJudge.Localization.Strings.Domain;

namespace EnduranceJudge.Domain.Validation
{
    public static class DateTimeValidations
    {
        public static DateTime IsFutureDate(this DateTime dateTime)
        {
            if (dateTime <= DateTime.Now)
            {
                throw new CoreException(INVALID_FUTURE_DATE_TEMPLATE, dateTime);
            }

            return dateTime;
        }
    }
}
