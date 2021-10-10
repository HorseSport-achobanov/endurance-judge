using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.Enums;

namespace EnduranceJudge.Domain.State.Athletes
{
    public interface IAthleteState : IDomainModelState
    {
        public string FeiId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Club { get; }
        public Category Category { get; }
    }
}
