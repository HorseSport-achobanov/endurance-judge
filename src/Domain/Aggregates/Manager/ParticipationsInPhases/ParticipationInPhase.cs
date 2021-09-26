using EnduranceJudge.Domain.Core.Validation;
using EnduranceJudge.Domain.Validation;
using EnduranceJudge.Domain.Aggregates.Manager.DTOs;
using EnduranceJudge.Domain.Aggregates.Manager.ResultsInPhases;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.States;
using System;
using System.Collections.Generic;

namespace EnduranceJudge.Domain.Aggregates.Manager.ParticipationsInPhases
{
    public class ParticipationInPhase : DomainBase<ParticipationInPhaseException>, IParticipationInPhaseState
    {
        private static readonly string ArrivalTimeIsNullMessage = $"cannot complete: ArrivalTime cannot be null.";
        private static readonly string InspectionTimeIsNullMessage = $"cannot complete: InspectionTime cannot be null";

        private ParticipationInPhase()
        {
        }

        internal ParticipationInPhase(PhaseDto phase, DateTime startTime)
        {
            this.Validate(() =>
            {
                this.StartTime = startTime
                    .IsRequired(nameof(startTime))
                    .IsFutureDate();

                phase.IsRequired(nameof(phase));
            });
            this.PhaseId = phase.Id;
            this.PhaseOrderBy = phase.OrderBy;
            this.PhaseLengthInKm = phase.LengthInKm;
            this.PhasesForCategories = phase.PhasesForCategories;
        }

        public DateTime StartTime { get; private set; }
        public DateTime? ArrivalTime { get; private set; }
        public DateTime? InspectionTime { get; private set; }
        public DateTime? ReInspectionTime { get; private set; }
        public ResultInPhase ResultInPhase { get; private set; }

        public int PhaseId { get; private init; }
        public int PhaseOrderBy { get; private init; }
        public int PhaseLengthInKm { get; private init; }
        public IEnumerable<PhaseForCategoryDto> PhasesForCategories { get; private init; }

        public TimeSpan? LoopSpan => this.ArrivalTime - this.StartTime;
        public TimeSpan? PhaseSpan => (this.ReInspectionTime ?? this.InspectionTime) - this.StartTime;
        public double? AverageSpeedForLoopInKpH
        {
            get
            {
                if (this.LoopSpan == null)
                {
                    return null;
                }
                return this.GetAverageSpeed(this.LoopSpan.Value);
            }
        }
        public double? AverageSpeedForPhaseInKpH
        {
            get
            {
                if (this.PhaseSpan == null)
                {
                    return null;
                }
                return this.GetAverageSpeed(this.PhaseSpan.Value);
            }
        }
        public bool IsComplete => this.ResultInPhase != null;
        public bool CanArrive
            => !this.IsComplete
                && !this.ArrivalTime.HasValue;
        public bool CanInspect
            => !this.IsComplete
                && this.ArrivalTime.HasValue
                && !this.InspectionTime.HasValue;
        public bool CanReInspect
            => !this.IsComplete
                && this.InspectionTime.HasValue
                && !this.ReInspectionTime.HasValue;
        public bool CanComplete
            => !this.IsComplete
                && (this.ReInspectionTime.HasValue || this.InspectionTime.HasValue);

        internal void Arrive(DateTime time)
            => this.Validate(() =>
            {
                if (!this.CanArrive)
                {
                    return;
                }
                this.ArrivalTime = time.IsRequired(nameof(time));
            });
        internal void Inspect(DateTime time)
            => this.Validate(() =>
            {
                if (!this.CanInspect)
                {
                    return;
                }
                this.InspectionTime = time.IsRequired(nameof(time));
            });
        internal void ReInspect(DateTime time)
            => this.Validate(() =>
            {
                if (!this.CanReInspect)
                {
                    return;
                }
                this.ReInspectionTime = time.IsRequired(nameof(time));
            });
        internal void CompleteSuccessful()
        {
            this.Complete();
        }
        internal void CompleteUnsuccessful(string code)
        {
            this.Complete(code);
        }

        private void Complete(string code = null)
            => this.Validate(() =>
            {
                if (!this.CanComplete)
                {
                    return;
                }
                this.ArrivalTime.IsNotDefault(ArrivalTimeIsNullMessage);
                this.InspectionTime.IsNotDefault(InspectionTimeIsNullMessage);

                this.ResultInPhase = code == null
                    ? new ResultInPhase()
                    : new ResultInPhase(code);
            });

        private double GetAverageSpeed(TimeSpan timeSpan)
        {
            var phaseLengthInKm = this.PhaseLengthInKm;
            var totalHours = timeSpan.TotalHours;
            return  phaseLengthInKm / totalHours;
        }
    }
}
