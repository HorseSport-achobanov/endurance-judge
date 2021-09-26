using EnduranceJudge.Domain.Aggregates.Rankings.Competitions;
using EnduranceJudge.Domain.Enums;
using EnduranceJudge.Domain.States;
using EnduranceJudge.Gateways.Persistence.Core;
using EnduranceJudge.Gateways.Persistence.Entities.EnduranceEvents;
using EnduranceJudge.Gateways.Persistence.Entities.ParticipantsInCompetitions;
using EnduranceJudge.Gateways.Persistence.Entities.Phases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EnduranceJudge.Gateways.Persistence.Entities.Competitions
{
    public class CompetitionEntity : AggregateRootEntityBase, ICompetitionState
    {
        private static readonly Type Ranking = typeof(Competition);

        public CompetitionType Type { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }

        [JsonIgnore]
        public EnduranceEventEntity EnduranceEvent { get; set; }
        public int EnduranceEventId { get; set; }

        [JsonIgnore]
        public IList<PhaseEntity> Phases { get; set; }

        [JsonIgnore]
        public IList<ParticipantInCompetitionEntity> ParticipantsInCompetitions { get; set; }

        public override IEnumerable<Type> DomainTypes { get; } = new[] { Ranking };
    }
}
