using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.State.Competitions;
using EnduranceJudge.Domain.State.PhaseEntries;
using EnduranceJudge.Domain.State.Phases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Domain.State.Participations
{
    public class Participation : DomainObjectBase<ParticipationException>
    {
        private Participation() {}
        public Participation(Competition competition) : base(true)
        {
            this.competitionIds.Add(competition.Id);
        }

        private List<int> competitionIds = new();
        public List<PhaseEntry> PhaseEntries { get; } = new();

        internal void Add(Competition competition) => this.Validate(() =>
        {
            if (this.CompetitionIds.Any())
            {
                var first = this.competitionIds.First();
                // TODO if first.Configuration != competition.Configuration -> throw
            }
            this.competitionIds.Add(competition.Id);
        });

        public void StartPhase(Phase phase, DateTime startTime)
        {
            var phaseEntry = new PhaseEntry(phase, startTime);
            this.PhaseEntries.Add(phaseEntry);
        }

        public IReadOnlyList<int> CompetitionIds
        {
            get => this.competitionIds.AsReadOnly();
            private set => this.competitionIds = value.ToList();
        }
    }
}
