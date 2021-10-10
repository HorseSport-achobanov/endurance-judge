using EnduranceJudge.Domain.Core.Models;

namespace EnduranceJudge.Domain.State.EnduranceEvents
{
    public interface IEnduranceEventState : IDomainModelState
    {
        public string Name { get; }
        public string PopulatedPlace { get; }
    }
}
