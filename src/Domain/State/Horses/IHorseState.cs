using EnduranceJudge.Domain.Core.Models;

namespace EnduranceJudge.Domain.State.Horses
{
    public interface IHorseState : IDomainModelState
    {
        string FeiId { get; }
        string Name { get; }
        string Club { get; }
        bool IsStallion { get; }
        string Breed { get; }
        string TrainerFeiId { get; }
        string TrainerFirstName { get; }
        string TrainerLastName { get; }
    }
}
