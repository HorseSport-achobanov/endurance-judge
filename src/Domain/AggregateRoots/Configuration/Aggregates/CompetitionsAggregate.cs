using EnduranceJudge.Domain.AggregateRoots.Configuration.Extensions;
using EnduranceJudge.Domain.Core.Exceptions;
using EnduranceJudge.Domain.Core.Extensions;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.Validation;
using EnduranceJudge.Domain.State;
using EnduranceJudge.Domain.State.Competitions;
using EnduranceJudge.Domain.State.Participants;
using System.Linq;
using static EnduranceJudge.Localization.Strings;

namespace EnduranceJudge.Domain.AggregateRoots.Configuration.Aggregates;

public class CompetitionsAggregate : IAggregate
{
    private readonly IState state;
    private readonly Validator<CompetitionException> validator;

    internal CompetitionsAggregate(IState state)
    {
        this.state = state;
        this.validator = new Validator<CompetitionException>();
    }

    public Competition Save(ICompetitionState competitionState)
    {
        this.state.ValidateThatEventHasNotStarted();
        this.validator.IsRequired(competitionState.Type, TYPE);
        this.validator.IsRequired(competitionState.Name, NAME);
        this.validator.IsRequired(competitionState.StartTime, START_TIME);

        var competition = this.state.Event.Competitions.FindDomain(competitionState.Id);
        if (competition == null)
        {
            competition = new Competition(competitionState);
            this.state.Event.Save(competition);
        }
        else
        {
            competition.Name = competitionState.Name;
            competition.Type = competitionState.Type;
            competition.StartTime = competitionState.StartTime;
        }

        return competition;
    }

    public void RemoveParticipation(int competitionId, int id)
    {
        this.state.ValidateThatEventHasNotStarted();
        var participation = this.state.Participations.FindDomain(id);
        if (participation == null)
        {
            throw Helper.Create<ParticipantException>(NOT_FOUND_BY_ID_MESSAGE);
        }
        participation.Remove(competitionId);
        if (!participation.CompetitionsIds.Any())
        {
            this.state.Participations.Remove(participation);
        }
    }
}
