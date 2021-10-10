using EnduranceJudge.Domain.Core.Extensions;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.State.Competitions;
using EnduranceJudge.Domain.State.Phases;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Domain.Aggregates.Configuration
{
    public class PhasesManager : ManagerObjectBase
    {
        private readonly List<Competition> competitions;

        internal PhasesManager(IEnumerable<Competition> competitions)
        {
            this.competitions = competitions.ToList();
        }

        public void Create(int competitionId, IPhaseState state)
        {
            var phase = new Phase(state);
            var competition = this.competitions.FindDomain(competitionId);
            competition.Save(phase);
        }

        public void Update(IPhaseState state)
        {
            var phase = new Phase(state);
            foreach (var competition in this.competitions)
            {
                if (competition.Phases.AnyMatch(phase))
                {
                    competition.Save(phase);
                }
            }
        }
    }
}
