using EnduranceJudge.Domain.Core.Exceptions;
using System.Collections.Generic;
using System.Linq;
using static EnduranceJudge.Localization.Strings.Domain;

namespace EnduranceJudge.Domain.Core.Validation
{
    public static class ValidationExtensions
    {
        public static TValue IsRequired<TValue>(this TValue value, string name)
        {
            if (value?.Equals(default(TValue)) ?? true)
            {
                throw new DomainException(IsRequiredTemplate, name, value);
            }

            return value;
        }

        public static TValue IsNotDefault<TValue>(this TValue value, string name)
        {
            if (value?.Equals(default(TValue)) ?? true)
            {
                throw new DomainException(IsRequiredTemplate, name);
            }

            return value;
        }

        public static TValue IsDefault<TValue>(this TValue value, string message)
        {
            if (!value?.Equals(default(TValue)) ?? false)
            {
                throw new DomainException(message);
            }

            return value;
        }

        public static void IsNotEmpty<TValue>(this IEnumerable<TValue> enumerable, string message)
        {
            if (!enumerable.Any())
            {
                throw new DomainException(message);
            }
        }

        public static void IsEmpty<TValue>(this IEnumerable<TValue> enumerable, string message)
        {
            if (enumerable.Any())
            {
                throw new DomainException(message);
            }
        }
    }
}
