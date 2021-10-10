using EnduranceJudge.Domain.State;
using EnduranceJudge.Domain.State.Countries;
using EnduranceJudge.Gateways.Persistence.Core;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Gateways.Persistence.Contracts.Queries
{
    public class CountryQueries : QueriesBase<Country>
    {
        public CountryQueries(IState state) : base(state)
        {
        }

        protected override List<Country> Set => this.State.Countries.ToList();
    }
}
