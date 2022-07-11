using EnduranceJudge.Application.Core;
using EnduranceJudge.Domain.State;
using EnduranceJudge.Domain.State.Participations;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Application.Queries;

public class ParticipationQueries : QueriesBase<Participation>
{
    public ParticipationQueries(IStateContext context) : base(context)
    {
    }

    protected override List<Participation> Set => this.State.Participations.ToList(); // TODO: order
}
