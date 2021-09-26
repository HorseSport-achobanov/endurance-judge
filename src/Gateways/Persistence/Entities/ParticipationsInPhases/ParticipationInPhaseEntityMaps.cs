using AutoMapper;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.Aggregates.Manager.ParticipationsInPhases;

namespace EnduranceJudge.Gateways.Persistence.Entities.ParticipationsInPhases
{
    public class ParticipationInPhaseEntityMaps : ICustomMapConfiguration
    {
        public void AddFromMaps(IProfileExpression profile)
        {
            profile.CreateMap<ParticipationInPhase, ParticipationInPhaseEntity>();
        }

        public void AddToMaps(IProfileExpression profile)
        {
            profile.CreateMap<ParticipationInPhaseEntity, ParticipationInPhase>()
                .ForMember(x => x.PhasesForCategories, opt => opt.MapFrom(y => y.Phase.PhasesForCategories));

            this.MapToRankingAggregate(profile);
        }

        private void MapToRankingAggregate(IProfileExpression profile)
        {
            profile.CreateMap<
                ParticipationInPhaseEntity,
                Domain.Aggregates.Rankings.ParticipationsInPhases.ParticipationInPhase>();
        }
    }
}
