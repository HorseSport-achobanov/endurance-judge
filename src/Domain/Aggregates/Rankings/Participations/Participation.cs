using EnduranceJudge.Domain.Core.Validation;
using EnduranceJudge.Domain.Aggregates.Rankings.ParticipationsInPhases;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Domain.Aggregates.Rankings.Participations
{
    public class Participation : DomainBase<RankingParticipationException>
    {
        private const string NOT_RANKED_MESSAGE = "cannot be classified as they are not qualified for ranking.";

        internal Participation()
        {
        }

        public int Number { get; private init; }
        public string AthleteFirstName { get; private init; }
        public string AthleteLastName { get; private init; }
        public string HorseName { get; private init; }
        public Category Category { get; private init; }
        public IReadOnlyList<ParticipationInPhase> ParticipationsInPhases { get; private init; }

        public bool IsNotComplete
            => this.ParticipationsInPhases.Any(pip => pip.IsNotComplete);
        public bool IsQualified
            => this.ParticipationsInPhases.All(participation => participation.ResultInPhase.IsRanked);

        public DateTime FinalTime
        {
            get
            {
                this.IsQualified.IsNotDefault(NOT_RANKED_MESSAGE);

                var finalPhase = this.ParticipationsInPhases.Single(participation => participation.Phase.IsFinal);
                return finalPhase.ArrivalTime;
            }
        }

        public long RecoverySpanInTicks
        {
            get
            {
                this.IsQualified.IsNotDefault(NOT_RANKED_MESSAGE);

                var recoverySum = this.ParticipationsInPhases.Aggregate(
                    TimeSpan.Zero,
                    (sum, participation) => sum.Add(participation.RecoverySpan));

                return recoverySum.Ticks;
            }
        }
    }
}
