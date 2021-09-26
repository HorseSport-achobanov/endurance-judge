using AutoMapper;
using EnduranceJudge.Core.Extensions;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.Aggregates.Event.Participants;
using EnduranceJudge.Domain.Aggregates.Manager.ParticipationsInCompetitions;
using EnduranceJudge.Domain.Aggregates.Rankings.Competitions;
using EnduranceJudge.Gateways.Persistence.Entities.Competitions;
using System.Linq;

namespace EnduranceJudge.Gateways.Persistence.Entities.ParticipantsInCompetitions
{
    public class ParticipantInCompetitionEntityMaps : ICustomMapConfiguration
    {
        public void AddFromMaps(IProfileExpression profile)
        {
            profile.CreateMap<Participant, ParticipantInCompetitionEntity>()
                .ForMember(pic => pic.Id, opt => opt.Ignore())
                .ForMember(pic => pic.Competition, opt => opt.Ignore())
                .ForMember(pic => pic.CompetitionId, opt => opt.Ignore())
                .ForMember(pic => pic.ParticipantId, opt => opt.MapFrom(p => p.Id))
                .ForMember(pic => pic.Participant, opt => opt.MapFrom(p => p));
            profile.CreateMap<Domain.Aggregates.Import.Participants.Participant, ParticipantInCompetitionEntity>()
                .ForMember(pic => pic.Id, opt => opt.Ignore())
                .ForMember(pic => pic.Competition, opt => opt.Ignore())
                .ForMember(pic => pic.CompetitionId, opt => opt.Ignore())
                .ForMember(pic => pic.ParticipantId, opt => opt.MapFrom(p => p.Id))
                .ForMember(pic => pic.Participant, opt => opt.MapFrom(p => p));

            this.MapFromManagerAggregate(profile);
        }

        private void MapFromManagerAggregate(IProfileExpression profile)
        {
            profile.CreateMap<ParticipationInCompetition, ParticipantInCompetitionEntity>();
        }

        public void AddToMaps(IProfileExpression profile)
        {
            profile.CreateMap<ParticipantInCompetitionEntity, Participant>()
                .ForMember(p => p.Id, opt => opt.MapFrom(pic => pic.ParticipantId))
                .ForMember(p => p.Number, opt => opt.MapFrom(pic => pic.Participant.Number))
                .ForMember(p => p.MaxAverageSpeedInKmPh, opt => opt.MapFrom(pic => pic.Participant.MaxAverageSpeedInKmPh))
                .ForMember(p => p.RfId, opt => opt.MapFrom(pic => pic.Participant.RfId));

            this.MapToManagerAggregate(profile);
        }

        private void MapToManagerAggregate(IProfileExpression profile)
        {
            profile.CreateMap<ParticipantInCompetitionEntity, ParticipationInCompetition>()
                .MapMember(d => d.Category, s => s.Participant.Athlete.Category)
                .MapMember(d => d.MaxAverageSpeedInKpH, s => s.Participant.MaxAverageSpeedInKmPh)
                .ForMember(d => d.Type, opt => opt.MapFrom(y => y.Competition.Type))
                .ForMember(d => d.Phases, opt => opt.MapFrom(y => y.Competition.Phases))
                .ForMember(d => d.StartTime, opt => opt.MapFrom(y => y.Competition.StartTime))
                .MapMember(
                    d => d.ParticipationsInPhases,
                    s => s.Participant.ParticipationsInPhases
                        .Where(pip => pip.Phase.CompetitionId == s.CompetitionId));
        }
    }
}
