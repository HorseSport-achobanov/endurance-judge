using EnduranceJudge.Domain.Core.Validation;
using EnduranceJudge.Domain.Core.Models;
using static EnduranceJudge.Localization.DesktopStrings;

namespace EnduranceJudge.Domain.State.Phases
{
    public class Phase : DomainObjectBase<PhaseException>, IPhaseState
    {
        private Phase() {}
        public Phase(IPhaseState state) : base(true)
            => this.Validate(() =>
        {
            this.IsFinal = state.IsFinal;
            this.OrderBy = state.OrderBy.IsRequired(ORDER);
            this.LengthInKm = state.LengthInKm.IsRequired(LENGTH_IN_KM);
            this.MaxRecoveryTimeInMins = state.MaxRecoveryTimeInMins.IsRequired(RECOVERY_IN_MINUTES_TEXT);
            this.RestTimeInMins = state.RestTimeInMins.IsRequired(REST_TIME_IN_MINS);
        });

        public bool IsFinal { get; private set; }
        public int OrderBy { get; private set; }
        public int LengthInKm { get; private set; }
        public int MaxRecoveryTimeInMins { get; private set; }
        public int RestTimeInMins { get; private set; }
    }
}
