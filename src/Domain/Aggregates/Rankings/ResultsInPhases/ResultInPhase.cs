using EnduranceJudge.Domain.Core.Models;

namespace EnduranceJudge.Domain.Aggregates.Rankings.ResultsInPhases
{
    public class ResultInPhase : DomainObjectBase<RankingResultInPhaseObjectException>
    {
        internal ResultInPhase() : base(default)
        {
        }

        public bool IsRanked { get; private set; }
    }
}
