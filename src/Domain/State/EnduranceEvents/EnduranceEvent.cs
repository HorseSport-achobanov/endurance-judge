using EnduranceJudge.Domain.Core.Extensions;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.Enums;
using EnduranceJudge.Domain.State.Competitions;
using EnduranceJudge.Domain.State.Countries;
using EnduranceJudge.Domain.State.Participations;
using EnduranceJudge.Domain.State.Personnels;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Domain.State.EnduranceEvents
{
    public class EnduranceEvent : DomainObjectBase<EnduranceEventException>, IEnduranceEventState
    {
        private EnduranceEvent()  {}

        internal EnduranceEvent(string name, Country country, string populatedPlace) : base(true)
            => this.Validate(() =>
        {
            this.Name = name;
            this.PopulatedPlace = populatedPlace;
            this.Country = country;
        });

        private List<Personnel> membersOfVetCommittee = new();
        private List<Personnel> membersOfJudgeCommittee = new();
        private List<Personnel> stewards = new();
        private List<Competition> competitions = new();
        private List<Participation> participations = new();

        public string Name { get; internal set; }
        public string PopulatedPlace { get; internal set; }
        public Country Country { get; internal set; }
        public Personnel PresidentGroundJury { get; internal set; }
        public Personnel PresidentVetCommission { get; internal set; }
        public Personnel ForeignJudge { get; internal set; }
        public Personnel FeiTechDelegate { get; internal set; }
        public Personnel FeiVetDelegate { get; internal set; }
        public Personnel ActiveVet { get; internal set; }

        public void Save(Competition competition) => this.Validate(() =>
        {
            this.competitions.Save(competition);
        });
        public void Save(Participation participation) => this.Validate(() =>
        {
            this.participations.Save(participation);
        });
        public void Save(Personnel personnel) => this.Validate(() =>
        {
            switch (personnel.Role)
            {
                case PersonnelRole.PresidentGroundJury:
                    this.PresidentGroundJury = personnel;
                    break;
                case PersonnelRole.PresidentVetCommission:
                    this.PresidentVetCommission = personnel;
                    break;
                case PersonnelRole.ForeignJudge:
                    this.ForeignJudge = personnel;
                    break;
                case PersonnelRole.FeiTechDelegate:
                    this.FeiTechDelegate = personnel;
                    break;
                case PersonnelRole.FeiVetDelegate:
                    this.FeiVetDelegate = personnel;
                    break;
                case PersonnelRole.ActiveVet:
                    this.ActiveVet = personnel;
                    break;
                case PersonnelRole.MemberOfVetCommittee:
                    this.membersOfVetCommittee.Save(personnel);
                    break;
                case PersonnelRole.MemberOfJudgeCommittee:
                    this.membersOfJudgeCommittee.Save(personnel);
                    break;
                case PersonnelRole.Steward:
                    this.stewards.Save(personnel);
                    break;
            }
        });

        public IReadOnlyList<Personnel> MembersOfVetCommittee
        {
            get => this.membersOfVetCommittee.AsReadOnly();
            private set => this.membersOfVetCommittee = value.ToList();
        }
        public IReadOnlyList<Personnel> MembersOfJudgeCommittee
        {
            get => this.membersOfJudgeCommittee.AsReadOnly();
            private set => this.membersOfJudgeCommittee = value.ToList();
        }
        public IReadOnlyList<Personnel> Stewards
        {
            get => this.stewards.AsReadOnly();
            private set => this.stewards = value.ToList();
        }
        public IReadOnlyList<Competition> Competitions
        {
            get => this.competitions.AsReadOnly();
            private set => this.competitions = value.ToList();
        }
        public IReadOnlyList<Participation> Participations
        {
            get => this.participations.AsReadOnly();
            private set => this.participations = value.ToList();
        }
    }
}
