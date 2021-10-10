using EnduranceJudge.Domain.Core.Models;

namespace EnduranceJudge.Domain.States
{
    public interface IResultInPhaseState : IDomainModelState
    {
        bool IsRanked { get; }

        string Code { get; }
    }
}
