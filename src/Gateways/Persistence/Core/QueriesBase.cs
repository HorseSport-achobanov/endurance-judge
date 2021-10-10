using EnduranceJudge.Application.Aggregates.Configurations.Contracts;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.State;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Gateways.Persistence.Core
{
    public abstract class QueriesBase<T> : IQueries<T>, IRepository
        where T : IDomainObject
    {
        protected QueriesBase(IState state)
        {
            this.State = state;
        }

        protected IState State { get; }

        protected abstract List<T> Set { get; }

        public T GetOne(Predicate<T> predicate) => this.Set.Find(predicate);
        public virtual T GetOne(int id) => this.Set.Find(x => x.Id == id);
        public virtual List<T> GetAll() => this.Set.ToList();
    }
}
