using EnduranceJudge.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace EnduranceJudge.Application.Aggregates.Configurations.Contracts
{
    public interface IQueries<T>
        where T : IDomainObject
    {
        T GetOne(Predicate<T> predicate);
        T GetOne(int id);
        List<T> GetAll();
    }
}
