using EnduranceJudge.Domain.Core.Validation;
using EnduranceJudge.Domain.Aggregates.Manager.DTOs;
using EnduranceJudge.Domain.Aggregates.Manager.ParticipationsInPhases;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Domain.Aggregates.Manager.ParticipationsInCompetitions
{
    public class ParticipationInCompetition : DomainObjectBase<ParticipationInCompetitionObjectException>
    {
        private readonly List<PhaseEntryManager> participationsInPhases = new();
        private readonly List<PhaseDto> phases = new();

        private ParticipationInCompetition()
        {
        }

        public DateTime StartTime { get; private init; }
        public CompetitionType Type { get; private init; }
        public Category Category { get; private init; }
        public int? MaxAverageSpeedInKpH { get; private init; }
        public IReadOnlyList<PhaseDto> Phases
        {
            get => this.phases;
            private init => this.phases = value
                .OrderBy(x => x.OrderBy)
                .ToList();
        }
        public IReadOnlyList<PhaseEntryManager> ParticipationsInPhases
        {
            get => this.participationsInPhases.AsReadOnly();
            private init => this.participationsInPhases = value
                .OrderBy(x => x.PhaseOrderBy)
                .ToList();
        }

        public PhaseEntryManager CurrentPhaseEntry
            => this.participationsInPhases.SingleOrDefault(participation => !participation.IsComplete);
        public bool IsNotComplete
            => this.Phases?.Count != this.participationsInPhases?.Count
                || this.ParticipationsInPhases.Any(x => !x.IsComplete);
        public bool HasExceededSpeedRestriction => this.AverageSpeedInKpH > this.MaxAverageSpeedInKpH;
        public double? AverageSpeedInKpH
        {
            get
            {
                var completedPhases = this.participationsInPhases
                    .Where(x => x.IsComplete)
                    .ToList();

                if (!completedPhases.Any())
                {
                    return null;
                }

                var averageSpeedInPhases = this.Type == CompetitionType.International
                    ? completedPhases.Select(x => x.AverageSpeedForPhaseInKpH!.Value)
                    : completedPhases.Select(x => x.AverageSpeedForLoopInKpH!.Value);

                var averageSpeedSum = averageSpeedInPhases.Aggregate(0d, (sum, average) => sum + average);
                var phasesCount = this.participationsInPhases.Count;

                return averageSpeedSum / phasesCount;
            }
        }

        internal void StartPhase()
            => this.Validate(() =>
            {
                var nextPhase = this.Phases
                    .Skip(this.participationsInPhases.Count)
                    .First();

                DateTime startTime;
                if (this.ParticipationsInPhases.Any())
                {
                    var lastPhase = this.ParticipationsInPhases.Last();
                    var restTime = lastPhase
                        .RestTimeInMinutes;
                    startTime = lastPhase.ReInspectionTime?.AddMinutes(restTime)
                        ?? lastPhase.InspectionTime!.Value.AddMinutes(restTime);
                }
                else
                {
                    startTime = this.StartTime;
                }

                var participation = new PhaseEntryManager(nextPhase, startTime);
                this.participationsInPhases.Add(participation);
            });
        internal void CompleteSuccessful()
            => this.Validate(() =>
            {
                if (this.HasExceededSpeedRestriction)
                {
                    this.CompleteUnsuccessful("speedyGonzalez");
                }
                else
                {
                    this.CurrentPhaseEntry.CompleteSuccessful();
                    if (this.IsNotComplete)
                    {
                        this.StartPhase();
                    }
                }
            });
        internal void CompleteUnsuccessful(string code)
            => this.Validate(() =>
            {
                this.CurrentPhaseEntry.CompleteUnsuccessful(code);
            });

    }
}
