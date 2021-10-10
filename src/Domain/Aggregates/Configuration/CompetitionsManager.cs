using EnduranceJudge.Domain.Core.Extensions;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.Core.Validation;
using EnduranceJudge.Domain.State.Competitions;
using EnduranceJudge.Domain.Validation;
using System.Collections.Generic;
using System.Linq;
using static EnduranceJudge.Localization.DesktopStrings;

namespace EnduranceJudge.Domain.Aggregates.Configuration
{
    public class CompetitionsManager : ManagerObjectBase
    {
        private readonly List<Competition> competitions;

        internal CompetitionsManager(IEnumerable<Competition> competitions)
        {
            this.competitions = competitions.ToList();
        }

        public void Save(ICompetitionState state) => this.Validate<CompetitionException>(() =>
        {
            state.Type.IsRequired(TYPE);
            state.Name.IsRequired(NAME);
            state.StartTime.IsRequired(START_TIME).IsFutureDate();

            var competition = new Competition(state.Type, state.Name, state.StartTime)
            {
                Id = state.Id,
            };
            this.competitions.Save(competition);
        });
    }
}
