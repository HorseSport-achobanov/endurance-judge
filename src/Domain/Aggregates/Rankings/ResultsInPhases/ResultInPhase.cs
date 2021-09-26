using EnduranceJudge.Domain.Core.Models;

namespace EnduranceJudge.Domain.Aggregates.Rankings.ResultsInPhases
{
    public class ResultInPhase : DomainBase<RankingResultInPhaseException>
    {
        internal ResultInPhase() : base(default)
        {
        }

        public bool IsRanked { get; private set; }
    }
}
