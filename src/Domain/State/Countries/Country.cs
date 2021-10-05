using EnduranceJudge.Domain.Core.Models;

namespace EnduranceJudge.Domain.State.Countries
{
    public class Country : DomainObjectBase<CountryException>, ICountryState
    {
        private Country() {}
        internal Country(string isoCode, string name) : base(default)
        {
            this.IsoCode = isoCode;
            this.Name = name;
        }

        public string IsoCode { get; }
        public string Name { get; }
    }
}
