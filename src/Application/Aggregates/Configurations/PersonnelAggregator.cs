using EnduranceJudge.Domain.State.EnduranceEvents;
using EnduranceJudge.Domain.State.Personnels;
using System.Collections.Generic;
using System.Linq;

namespace EnduranceJudge.Application.Aggregates.Configurations
{
    public static class PersonnelAggregator
    {
        public static List<Personnel> Aggregate(EnduranceEvent enduranceEvent)
        {
            var personnelList = new List<Personnel>
            {
                enduranceEvent.PresidentGroundJury,
                enduranceEvent.PresidentVetCommission,
                enduranceEvent.ActiveVet,
                enduranceEvent.ForeignJudge,
                enduranceEvent.FeiTechDelegate,
                enduranceEvent.FeiVetDelegate
            };

            personnelList.AddRange(enduranceEvent.MembersOfJudgeCommittee);
            personnelList.AddRange(enduranceEvent.MembersOfVetCommittee);
            personnelList.AddRange(enduranceEvent.Stewards);

            return personnelList
                .Where(x => x != null)
                .ToList();
        }
    }
}
