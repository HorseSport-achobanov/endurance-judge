using EnduranceJudge.Core.Extensions;
using EnduranceJudge.Domain.Core.Validation;
using EnduranceJudge.Domain.Aggregates.Event.Participants;
using EnduranceJudge.Domain.Aggregates.Event.Phases;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.Enums;
using EnduranceJudge.Domain.States;
using EnduranceJudge.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Domain.Aggregates.Event.Competitions
{
    public class Competition : DomainBase<CompetitionException>, ICompetitionState
    {
        private Competition()
        {
        }

        public Competition(ICompetitionState state) : base(state.Id)
            => this.Validate(() =>
            {
                this.Type = state.Type.IsRequired(nameof(state.Type));
                this.Name = state.Name.IsRequired(nameof(state.Name));
                this.StartTime = state.StartTime
                    .IsRequired(nameof(state.StartTime))
                    .IsFutureDate();
            });

        public CompetitionType Type { get; private set; }
        public string Name { get; private set; }
        public DateTime StartTime { get; private set; }

        private List<Phase> phases = new();
        public IReadOnlyList<Phase> Phases
        {
            get => this.phases.AsReadOnly();
            private set => this.phases = value.ToList();
        }
        public Competition Add(Phase phase)
        {
            this.phases.AddOrUpdateObject(phase);
            return this;
        }

        private List<Participant> participants = new();
        public IReadOnlyList<Participant> Participants
        {
            get => this.participants.AsReadOnly();
            private set => this.participants = value.ToList();
        }
        public Competition Add(Participant participant)
        {
            this.participants.AddOrUpdateObject(participant);
            return this;
        }
        public Competition Remove(Participant participant)
        {
            this.participants.RemoveObject(participant);
            return this;
        }
    }
}
