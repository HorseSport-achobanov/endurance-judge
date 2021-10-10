using EnduranceJudge.Domain.State.Phases;

namespace EnduranceJudge.Domain.Aggregates.Manager.DTOs
{
    public class PhaseDto : IPhaseState
    {
        public int Id { get; private init; }
        public int OrderBy { get; private init; }
        public int LengthInKm { get; private init; }
        public bool IsFinal { get; private init; }
        public int MaxRecoveryTimeInMins { get; private init; }
        public int RestTimeInMins { get; private init; }
    }
}
