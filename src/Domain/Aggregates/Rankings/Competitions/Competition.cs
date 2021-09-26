using EnduranceJudge.Domain.Aggregates.Rankings.Participations;
using EnduranceJudge.Domain.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Domain.Aggregates.Rankings.Competitions
{
    public class Competition : DomainBase<RankingCompetitionException>, IAggregateRoot
    {
        private List<Participation> participations = new();

        private Competition()
        {
        }

        public int TotalLengthInKm => this.PhaseLengthsInKm.Aggregate(0, (total, value) => total + value);
        public int[] PhaseLengthsInKm { get; private init; }

        public IReadOnlyList<Participation> Participations
        {
            get => this.participations.AsReadOnly();
            private set => this.participations = value.ToList();
        }
    }
}
