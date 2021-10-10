using EnduranceJudge.Domain.State.Athletes;
using Prism.Events;

namespace EnduranceJudge.Gateways.Desktop.Events.Athletes
{
    public class AthleteUpdatedEvent : PubSubEvent<IAthleteState>
    {
    }
}
