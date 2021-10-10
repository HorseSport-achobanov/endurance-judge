using EnduranceJudge.Domain.State;
using EnduranceJudge.Domain.State.Participants;
using EnduranceJudge.Gateways.Persistence.Core;
using System.Collections.Generic;

namespace EnduranceJudge.Gateways.Persistence.Contracts.Queries
{
    public class CompetitorQueries : QueriesBase<Participant>
    {
        public CompetitorQueries(IState state) : base(state)
        {
        }

        protected override List<Participant> Set => this.State.Participants;
    }
}
