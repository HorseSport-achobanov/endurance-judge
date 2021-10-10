using EnduranceJudge.Core.Extensions;
using EnduranceJudge.Domain.Core.Extensions;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.Enums;
using EnduranceJudge.Domain.State.Phases;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Domain.State.Competitions
{
    public class Competition : DomainObjectBase<CompetitionException>, ICompetitionState
    {
        private Competition() {}
        public Competition(CompetitionType type, string name, DateTime? startTime = null) : base(true)
            => this.Validate(() =>
        {
            this.Type = type;
            this.Name = name;
            this.StartTime = startTime ?? DateTime.Today;
        });

        private List<Phase> phases = new();
        public CompetitionType Type { get; private set; }
        public string Name { get; private set; }
        public DateTime StartTime { get; private set; }

        public void Save(Phase phase) => this.Validate(() =>
        {
            this.phases.Save(phase);
        });

        public IReadOnlyList<Phase> Phases
        {
            get => this.phases.AsReadOnly();
            private set => this.phases = value.ToList();
        }
    }
}
