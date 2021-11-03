using EnduranceJudge.Domain.Core.Validation;
using EnduranceJudge.Domain.Validation;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.Enums;

namespace EnduranceJudge.Domain.State.Personnels
{
    public class Personnel : DomainObjectBase<PersonnelException>, IPersonnelState
    {
        private Personnel() {}
        public Personnel(IPersonnelState state) : base(GENERATE_ID)
            => this.Validate(() =>
            {
                this.Name = state.Name.CheckPersonName();
                this.Role = state.Role.IsRequired(nameof(state.Role));
            });

        public string Name { get; private set; }
        public PersonnelRole Role { get; private set; }
    }
}
