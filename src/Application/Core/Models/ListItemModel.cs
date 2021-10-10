using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.State.Horses;
using EnduranceJudge.Domain.State.Participants;

namespace EnduranceJudge.Application.Core.Models
{
    public class ListItemModel
        : IMapFrom<Horse>,
        IMapFrom<Participant>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
