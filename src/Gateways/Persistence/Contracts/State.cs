using EnduranceJudge.Core.ConventionalServices;
using EnduranceJudge.Domain.State;
using EnduranceJudge.Domain.State.Athletes;
using EnduranceJudge.Domain.State.Countries;
using EnduranceJudge.Domain.State.EnduranceEvents;
using EnduranceJudge.Domain.State.Horses;
using EnduranceJudge.Domain.State.Participants;
using System.Collections.Generic;

namespace EnduranceJudge.Gateways.Persistence.Contracts
{
    public class State : IState, ISingletonService
    {
        public EnduranceEvent Event { get; set; }
        public List<Horse> Horses { get; } = new();
        public List<Athlete> Athletes { get; } = new();
        public List<Participant> Participants { get; } = new();
        public List<Country> Countries { get; } = new();
    }
}
