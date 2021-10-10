namespace EnduranceJudge.Domain.State.Countries
{
    public interface ICountryState
    {
        string IsoCode { get; }

        string Name { get; }
    }
}
