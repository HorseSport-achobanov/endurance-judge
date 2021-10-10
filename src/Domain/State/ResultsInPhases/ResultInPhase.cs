using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.States;

namespace EnduranceJudge.Domain.State.ResultsInPhases
{
    public class ResultInPhase : DomainObjectBase<ManagerResultInPhaseObjectException>, IResultInPhaseState
    {
        private ResultInPhase() {}
        internal ResultInPhase(string code = null) : base(true)
        {
            this.IsRanked = code == null;
            this.Code = code;
        }

        public bool IsRanked { get; private set; }
        public string Code { get; private set; }
    }
}
