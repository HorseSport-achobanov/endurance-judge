using EnduranceJudge.Domain.Core.Exceptions;
using System;

namespace EnduranceJudge.Domain.Core.Models
{
    public abstract class ManagerObjectBase
    {
        protected void Validate<TCustomException>(Action action)
            where TCustomException : DomainObjectException, new()
        {
            try
            {
                action();
            }
            catch (DomainException exception)
            {
                this.Throw<TCustomException>(exception.Message);
            }
        }


        protected void Throw<TCustomException>(string message)
            where TCustomException : DomainObjectException, new()
            => Thrower.Throw<TCustomException>(message);
    }
}
