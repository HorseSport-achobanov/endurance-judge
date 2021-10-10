using EnduranceJudge.Domain.State;
using EnduranceJudge.Domain.State.Athletes;
using EnduranceJudge.Gateways.Persistence.Core;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Gateways.Persistence.Contracts.Queries
{
    public class AthleteQueries : QueriesBase<Athlete>
    {
        public AthleteQueries(IState state) : base(state)
        {
        }

        protected override List<Athlete> Set => this.State.Athletes.ToList();
    }
}
