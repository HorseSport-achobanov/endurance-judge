using AutoMapper;
using EnduranceJudge.Application.Events.Common;
using EnduranceJudge.Core.Extensions;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.Aggregates.Manager.Participations;
using System.Linq;

namespace EnduranceJudge.Gateways.Persistence.Entities.Participants
{
    public class ParticipantEntityMaps : ICustomMapConfiguration
    {
        public void AddFromMaps(IProfileExpression profile)
        {
            profile.CreateMap<Domain.Aggregates.Event.Participants.Participant, ParticipantEntity>();
            profile.CreateMap<Domain.Aggregates.Import.Participants.Participant, ParticipantEntity>()
                .ForMember(x => x.HorseId, opt => opt.Condition(p => p.Horse != null))
                .ForMember(x => x.Horse, opt => opt.Condition(p => p.Horse != null))
                .ForMember(x => x.AthleteId, opt => opt.Condition(p => p.Athlete != null))
                .ForMember(x => x.Athlete, opt => opt.Condition(p => p.Athlete != null));

            this.MapFromManagerAggregate(profile);
        }

        private void MapFromManagerAggregate(IProfileExpression profile)
        {
            profile.CreateMap<Participation, ParticipantEntity>()
                .MapMember(s => s.ParticipantsInCompetitions, d => d.ParticipationsInCompetitions)
                .MapMember(
                    d => d.ParticipationsInPhases,
                    s => s.ParticipationsInCompetitions.SelectMany(pic => pic.ParticipationsInPhases));
        }

        public void AddToMaps(IProfileExpression profile)
        {
            profile.CreateMap<ParticipantEntity, Domain.Aggregates.Event.Participants.Participant>();
            profile.CreateMap<ParticipantEntity, Domain.Aggregates.Import.Participants.Participant>();
            profile.CreateMap<ParticipantEntity, ParticipantDependantModel>()
                .MapMember(x => x.CategoryId, y => (int)y.Athlete.Category)
                .MapMember(x => x.HorseId, y => y.Horse.Id)
                .MapMember(x => x.AthleteId, y => y.Athlete.Id)
                .MapMember(x => x.Name, y => $"{y.Athlete.FirstName} {y.Athlete.LastName} - {y.Horse.Name}");
            profile.CreateMap<ParticipantEntity, ListItemModel>()
                .MapMember(x => x.Name, y => y.Number);

            this.MapToManagerAggregate(profile);
            this.MapToRankingAggregate(profile);
        }

        private void MapToManagerAggregate(IProfileExpression profile)
        {
            profile.CreateMap<ParticipantEntity, Participation>()
                .MapMember(d => d.ParticipationsInCompetitions, s => s.ParticipantsInCompetitions);
        }

        private void MapToRankingAggregate(IProfileExpression profile)
        {
            profile.CreateMap<ParticipantEntity, Domain.Aggregates.Rankings.Participations.Participation>()
                .ForMember(x => x.Category, opt => opt.MapFrom(y => y.Athlete.Category));
        }
    }
}
