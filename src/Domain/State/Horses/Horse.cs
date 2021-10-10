using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.Core.Validation;
using static EnduranceJudge.Localization.DesktopStrings;

namespace EnduranceJudge.Domain.State.Horses
{
    public class Horse : DomainObjectBase<HorseException>, IHorseState
    {
        private Horse() {}
        public Horse(string feiId, string name, string breed, string club) : base(true)
            => this.Validate(() =>
        {
            this.Name = name;
            this.Breed = breed;
            this.FeiId = feiId;
            this.Club = club;
        });

        public Horse(
            string feiId,
            string name,
            bool isStallion,
            string breed,
            string trainerFeiId,
            string trainerFirstName,
            string trainerLastName)
            : base(true)
            => this.Validate(() =>
        {
            this.Name = name;
            this.Breed = breed;
            this.IsStallion = isStallion;
            this.FeiId = feiId;
            this.TrainerFeiId = trainerFeiId;
            this.TrainerFirstName = trainerFirstName;
            this.TrainerLastName = trainerLastName;
        });

        public Horse(IHorseState state)
        {
            this.FeiId = state.FeiId;
            this.Club = state.Club;
            this.IsStallion = state.IsStallion;
            this.Breed = state.Breed;
            this.TrainerFeiId = state.TrainerFeiId;
            this.TrainerFirstName = state.TrainerFirstName;
            this.TrainerLastName = state.TrainerLastName;
            this.Name = state.Name.IsRequired(NAME);
        }

        public string FeiId { get; private set; }
        public string Name { get; private set; }
        public string Club { get; private set; }
        public bool IsStallion { get; private set; }
        public string Breed { get; private set; }
        public string TrainerFeiId { get; private set; }
        public string TrainerFirstName { get; private set; }
        public string TrainerLastName { get; private set; }
    }
}
