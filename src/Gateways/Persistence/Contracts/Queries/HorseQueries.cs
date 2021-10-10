using EnduranceJudge.Domain.State;
using EnduranceJudge.Domain.State.Horses;
using EnduranceJudge.Gateways.Persistence.Core;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Gateways.Persistence.Contracts.Queries
{
    public class HorseQueries : QueriesBase<Horse>
    {
        public HorseQueries(IState state) : base(state)
        {
        }

        protected override List<Horse> Set => this.State.Horses.ToList();
    }
}
