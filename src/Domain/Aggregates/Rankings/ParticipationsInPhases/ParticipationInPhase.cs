using EnduranceJudge.Domain.Core.Validation;
using EnduranceJudge.Domain.State.ResultsInPhases;
using EnduranceJudge.Domain.Aggregates.Rankings.DTOs;
using EnduranceJudge.Domain.Core.Models;
using System;

namespace EnduranceJudge.Domain.Aggregates.Rankings.ParticipationsInPhases
{
    public class ParticipationInPhase : DomainObjectBase<RankingParticipationInPhaseObjectException>
    {
        private ParticipationInPhase()
        {
        }

        public DateTime ArrivalTime { get; private set; }
        public DateTime InspectionTime { get; private set; }
        public DateTime? ReInspectionTime { get; private set; }
        public ResultInPhase ResultInPhase { get; private set; }
        public PhaseForRanking Phase { get; private set; }

        public TimeSpan RecoverySpan
        {
            get
            {
                this.Validate(() =>
                {
                    this.ArrivalTime.IsNotDefault(nameof(this.ArrivalTime));
                    this.InspectionTime.IsNotDefault(nameof(this.ArrivalTime));
                });

                var inspectionTime = this.ReInspectionTime ?? this.InspectionTime;
                return this.ArrivalTime - inspectionTime;
            }
        }
        public bool IsNotComplete => this.ResultInPhase == null;
    }
}
