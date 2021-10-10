using System;

namespace EnduranceJudge.Domain.Core.Exceptions
{
    public abstract class DomainObjectException : Exception
    {
        public string DomainMessage { private get; init; }

        public override string Message
            => string.Format($"{this.Entity} {this.DomainMessage}");

        protected abstract string Entity { get; }
    }
}
