using EnduranceJudge.Application.Core.Contracts;
using EnduranceJudge.Application.Core.Handlers;
using EnduranceJudge.Domain.Aggregates.Rankings.Competitions;
using MediatR;
using System.Collections.Generic;

namespace EnduranceJudge.Application.Actions.Ranking.Queries.GetCompetitionList
{
    public class GetCompetitionList : IRequest<IEnumerable<Competition>>
    {
        public class GetCompetitionListHandler : GetAllHandler<GetCompetitionList, Competition, Competition>
        {
            public GetCompetitionListHandler(IQueries<Competition> queries) : base(queries)
            {
            }
        }
    }
}
