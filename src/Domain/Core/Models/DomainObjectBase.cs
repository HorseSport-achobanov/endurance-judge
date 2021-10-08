﻿using EnduranceJudge.Core.Exceptions;
using EnduranceJudge.Core.Models;
using EnduranceJudge.Domain.Core.Exceptions;
using System;

namespace EnduranceJudge.Domain.Core.Models
{
    public abstract class DomainObjectBase<TException> : ObjectBase, IDomainModel
        where TException : DomainException, new()
    {
        protected DomainObjectBase()
        {
        }

        protected DomainObjectBase(int id)
        {
            this.Id = id;
        }

        public int Id { get; init; }

        internal void Validate(Action action)
        {
            try
            {
                action();
            }
            catch (CoreException exception)
            {
                this.Throw(exception.Message);
            }
        }
        internal void Validate<TCustomException>(Action action)
            where TCustomException : DomainException, new()
        {
            try
            {
                action();
            }
            catch (CoreException exception)
            {
                this.Throw<TCustomException>(exception.Message);
            }
        }

        internal void Throw(string message)
            => Thrower.Throw<TException>(message);
        internal void Throw<TCustomException>(string message)
            where TCustomException : DomainException, new()
            => Thrower.Throw<TCustomException>(message);

        public override bool Equals(object other)
        {
            return this.IsEqual(other);
        }

        public bool Equals(IIdentifiable identifiable)
        {
            if (identifiable == null)
            {
                return false;
            }
            if (this.Id != default &&  identifiable.Id != default)
            {
                return this.Id == identifiable.Id;
            }

            return base.Equals(identifiable);
        }

        public override int GetHashCode()
            => base.GetHashCode() + this.Id;

        private bool IsEqual(object other)
        {
            if (other is not IDomainModel domainModel)
            {
                return false;
            }
            if (this.GetType() != domainModel.GetType())
            {
                return false;
            }

            return this.Equals(domainModel);
        }
    }
}