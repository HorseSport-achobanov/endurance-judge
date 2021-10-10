using EnduranceJudge.Domain.State;
using EnduranceJudge.Domain.State.Phases;
using EnduranceJudge.Gateways.Persistence.Core;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Gateways.Persistence.Contracts.Queries
{
    public class PhaseQueries : QueriesBase<Phase>
    {
        public PhaseQueries(IState state) : base(state)
        {
        }
        protected override List<Phase> Set
            => this.State
                .Event
                .Competitions
                .SelectMany(x => x.Phases)
                .ToList();
    }
}
