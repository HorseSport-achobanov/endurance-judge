using EnduranceJudge.Application.Aggregates.Configurations;
using EnduranceJudge.Domain.State;
using EnduranceJudge.Domain.State.Personnels;
using EnduranceJudge.Gateways.Persistence.Core;
using System.Collections.Generic;

namespace EnduranceJudge.Gateways.Persistence.Contracts.Queries
{
    public class PersonnelQueries : QueriesBase<Personnel>
    {
        private List<Personnel> personnel;

        public PersonnelQueries(IState state) : base(state)
        {
        }

        protected override List<Personnel> Set
            => this.personnel
                ?? (this.personnel = PersonnelAggregator.Aggregate(this.State.Event));
    }
}
