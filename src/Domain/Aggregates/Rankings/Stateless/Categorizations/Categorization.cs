using EnduranceJudge.Domain.Aggregates.Rankings.Competitions;
using EnduranceJudge.Domain.Aggregates.Rankings.Stateless.Classifications;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.Enums;
using System.Linq;

namespace EnduranceJudge.Domain.Aggregates.Rankings.Stateless.Categorizations
{
    public class Categorization : DomainObjectBase<CategorizationObjectException>
    {
        internal Categorization(Competition competition)
        {
            var kidsClassification = new Classification(Category.Kids, competition);
            var adultsClassification = new Classification(Category.Adults, competition);

            if (kidsClassification.RankList.Any())
            {
                this.KidsClassification = kidsClassification;
            }
            if (adultsClassification.RankList.Any())
            {
                this.AdultsClassification = adultsClassification;
            }
        }

        public int Length => this.KidsClassification?.TotalLengthInKm
            ?? this.AdultsClassification.TotalLengthInKm;

        public Classification KidsClassification { get; }
        public Classification AdultsClassification { get; }
    }
}
