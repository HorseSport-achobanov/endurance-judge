using AutoMapper;
using EnduranceJudge.Application.Events.Common;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.Aggregates.Event.Competitions;
using EnduranceJudge.Domain.Aggregates.Manager.DTOs;
using System.Linq;
using ImportCompetition = EnduranceJudge.Domain.Aggregates.Import.Competitions.Competition;

namespace EnduranceJudge.Gateways.Persistence.Entities.Competitions
{
    public class CompetitionEntityMaps : ICustomMapConfiguration
    {
        public void AddFromMaps(IProfileExpression profile)
        {
            profile.CreateMap<Competition, CompetitionEntity>()
                .ForMember(ce => ce.ParticipantsInCompetitions, opt => opt.MapFrom(c => c.Participants))
                .AfterMap((c, ce) =>
                {
                    foreach (var participantInCompetition in ce.ParticipantsInCompetitions
                        .Where(pic => pic.CompetitionId == default))
                    {
                        participantInCompetition.Competition = ce;
                        participantInCompetition.CompetitionId = ce.Id;
                    }
                });
            profile.CreateMap<ImportCompetition, CompetitionEntity>()
                .ForMember(ce => ce.ParticipantsInCompetitions, opt => opt.MapFrom(c => c.Participants))
                .AfterMap((c, ce) =>
                {
                    foreach (var participantInCompetition in ce.ParticipantsInCompetitions
                        .Where(pic => pic.CompetitionId == default))
                    {
                        participantInCompetition.Competition = ce;
                        participantInCompetition.CompetitionId = ce.Id;
                    }
                });
        }

        public void AddToMaps(IProfileExpression profile)
        {
            profile.CreateMap<CompetitionEntity, Competition>()
                .ForMember(c => c.Participants, opt => opt.MapFrom(ce => ce.ParticipantsInCompetitions));
            profile.CreateMap<CompetitionEntity, ListItemModel>()
                .ForMember(x => x.Name, opt => opt.MapFrom(y => y.Type.ToString()));
            profile.CreateMap<CompetitionEntity, CompetitionDependantModel>()
                .ForMember(
                    d => d.Participants,
                    opt => opt.MapFrom(y => y.ParticipantsInCompetitions.Select(pic => pic.Participant)));
            profile.CreateMap<CompetitionEntity, CompetitionDto>();

            this.MapToRankingAggregate(profile);
        }

        private void MapToRankingAggregate(IProfileExpression profile)
        {
            profile.CreateMap<CompetitionEntity, Domain.Aggregates.Rankings.Competitions.Competition>()
                .ForMember(
                    x => x.PhaseLengthsInKm,
                    opt => opt.MapFrom(y => y.Phases.Select(x => x.LengthInKm)))
                .ForMember(
                    x => x.Participations,
                    opt => opt.MapFrom(y => y.ParticipantsInCompetitions.Select(x => x.Participant)));
        }
    }
}
