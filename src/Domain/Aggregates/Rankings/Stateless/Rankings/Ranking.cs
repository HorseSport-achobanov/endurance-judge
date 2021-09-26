using EnduranceJudge.Domain.Aggregates.Rankings.Competitions;
using EnduranceJudge.Domain.Aggregates.Rankings.Stateless.Categorizations;
using EnduranceJudge.Domain.Core.Models;
using System.Collections.Generic;

namespace EnduranceJudge.Domain.Aggregates.Rankings.Stateless.Rankings
{
    public class Ranking : DomainBase<RankingException>
    {
        private readonly List<Categorization> categorizations = new();

        public Ranking(IEnumerable<Competition> competitions)
        {
            foreach (var competition in competitions)
            {
                var listing = new Categorization(competition);
                this.categorizations.Add(listing);
            }
        }

        public IReadOnlyList<Categorization> Categorizations => this.categorizations.AsReadOnly();
    }
}
