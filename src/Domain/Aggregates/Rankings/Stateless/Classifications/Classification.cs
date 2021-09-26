using EnduranceJudge.Domain.Core.Validation;
using EnduranceJudge.Domain.Aggregates.Rankings.Competitions;
using EnduranceJudge.Domain.Aggregates.Rankings.Participations;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.Enums;
using System.Collections.Generic;
using System.Linq;
using static EnduranceJudge.Localization.Strings.Domain;

namespace EnduranceJudge.Domain.Aggregates.Rankings.Stateless.Classifications
{
    public class Classification : DomainBase<ClassificationException>
    {
        internal Classification(Category category, Competition competition)
        {
            this.Validate(() =>
            {
                category.IsRequired(nameof(category));
                competition.IsRequired(nameof(competition));
            });

            this.Category = category;
            this.TotalLengthInKm = competition.TotalLengthInKm;
            this.RankList = this.Classify(competition.Participations);
        }

        public int TotalLengthInKm { get; }
        public Category Category { get; }
        public IReadOnlyList<Participation> RankList { get; }

        private List<Participation> Classify(IReadOnlyList<Participation> participations)
        {
            IEnumerable<Participation> ranked = null;
            this.Validate(() =>
            {
                if (participations.Any(x => x.IsNotComplete))
                {
                    throw new ClassificationException
                    {
                        DomainMessage = Ranking.INCOMPLETE_PARTICIPATIONS,
                    };
                }
                var qualified = participations.Where(x =>
                    x.Category == this.Category
                    && x.IsQualified);

                ranked = this.Category == Category.Kids
                    ? qualified.OrderBy(participation => participation.RecoverySpanInTicks)
                    : qualified.OrderBy(participation => participation.FinalTime);
            });

            return ranked?.ToList();
        }
    }
}
