using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.Core.Validation;
using EnduranceJudge.Domain.Enums;
using EnduranceJudge.Domain.State.Countries;
using Newtonsoft.Json;
using System;
using static EnduranceJudge.Localization.DesktopStrings;

namespace EnduranceJudge.Domain.State.Athletes
{
    public class Athlete : DomainObjectBase<AthleteException>, IAthleteState
    {
        private const int ADULT_AGE_IN_YEARS = 18;

        private Athlete() {}
        public Athlete(string feiId, string firstName, string lastName, Country country, DateTime birthDate)
            : base(true) => this.Validate(() =>
        {
            this.FeiId = feiId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Country = country;
            this.Category = GetCategory(birthDate);
        });

        public Athlete(IAthleteState state) : base(true)
        {
            this.FeiId = state.FeiId;
            this.Club = state.Club;
            this.FirstName = state.FirstName.IsRequired(FIRST_NAME);
            this.LastName = state.LastName.IsRequired(LAST_NAME);
            this.Category = state.Category.IsRequired(CATEGORY);
        }

        public string FeiId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Club { get; private set; }
        public Category Category { get; private set; }
        public Country Country { get; internal set; }

        private static Category GetCategory(DateTime birthDate)
        {
            var category = birthDate.AddYears(ADULT_AGE_IN_YEARS) <= DateTime.Now
                ? Category.Adults
                : Category.Kids;
            return category;
        }

        public string Name => $"{this.FirstName} {this.LastName}";
    }
}
