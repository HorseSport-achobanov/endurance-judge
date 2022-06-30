using EnduranceJudge.Core.Models;
using EnduranceJudge.Domain.Core.Exceptions;
using EnduranceJudge.Domain.Validation;
using System;

namespace EnduranceJudge.Domain.Core.Models;

public abstract class DomainBase<TException> : IDomain, IEquatable<DomainBase<TException>>
    where TException : DomainExceptionBase, new()
{
    protected const string GENERATE_ID = "GenerateIdFlag";

    // Empty constructor is used by mapping for existing (in the database) entries
    protected DomainBase()
    {
    }
    // Unused variable is needed mark the constructor which generates Id
    // That constructor should ONLY be used when creating NEW (no database entry) objects
    protected DomainBase(string generateIdFlag)
    {
        this.Id = DomainIdProvider.Generate();
    }

    public int Id { get; protected init; } // Keep setter for mapping

    public override bool Equals(object other)
        => this.IsEqual(other);

    public bool Equals(IIdentifiable identifiable)
        => this.IsEqual(identifiable);

    public bool Equals(DomainBase<TException> domain)
        => this.IsEqual(domain);

    public static bool operator ==(DomainBase<TException> one, DomainBase<TException> two)
    {
        if (ReferenceEquals(one, null))
        {
            return ReferenceEquals(two, null);
        }
        return one.Equals(two);
    }

    public static bool operator !=(DomainBase<TException> one, DomainBase<TException> two)
        => !(one == two);

    private bool IsEqual(object other)
    {
        if (other is not IIdentifiable identifiable)
        {
            return false;
        }
        if (ReferenceEquals(this, other))
        {
            return true;
        }
        if (this.Id == identifiable.Id)
        {
            return true;
        }
        if (this.GetType() != other.GetType())
        {
            return false;
        }

        return this.GetHashCode().Equals(other.GetHashCode());
    }

    public override int GetHashCode()
        => this.Id;

    protected Validator<TException> Validator { get; } = new();
}
