using EnduranceJudge.Domain.State.Horses;
using Prism.Events;

namespace EnduranceJudge.Gateways.Desktop.Events.Horses
{
    public class HorseUpdatedEvent : PubSubEvent<IHorseState>
    {
    }
}
