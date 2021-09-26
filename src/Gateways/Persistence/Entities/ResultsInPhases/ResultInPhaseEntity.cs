using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.Aggregates.Manager.ResultsInPhases;
using EnduranceJudge.Domain.States;
using EnduranceJudge.Gateways.Persistence.Core;
using EnduranceJudge.Gateways.Persistence.Entities.ParticipationsInPhases;
using Newtonsoft.Json;

namespace EnduranceJudge.Gateways.Persistence.Entities.ResultsInPhases
{
    public class ResultInPhaseEntity : EntityBase, IResultInPhaseState,
        IMap<ResultInPhase>,
        IMapTo<Domain.Aggregates.Rankings.ResultsInPhases.ResultInPhase>
    {
        public bool IsRanked { get; set; }
        public string Code { get; set; }

        [JsonIgnore]
        public ParticipationInPhaseEntity ParticipationInPhase { get; set; }
    }
}
