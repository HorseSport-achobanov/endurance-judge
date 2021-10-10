using EnduranceJudge.Domain.State;
using EnduranceJudge.Domain.State.Competitions;
using EnduranceJudge.Gateways.Persistence.Core;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Gateways.Persistence.Contracts.Queries
{
    public class CompetitionQueries : QueriesBase<Competition>
    {
        public CompetitionQueries(IState state) : base(state)
        {
        }
        protected override List<Competition> Set => this.State.Event.Competitions.ToList();
    }
}
