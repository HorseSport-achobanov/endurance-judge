using System;

namespace EnduranceJudge.Domain.Core.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message)
            : base(message)
        {
        }

        public DomainException(string template, params object[] arguments)
            : base(string.Format(template, arguments))
        {
        }
    }
}
