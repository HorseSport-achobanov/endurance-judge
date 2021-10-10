using EnduranceJudge.Gateways.Desktop.Core;
using EnduranceJudge.Gateways.Desktop.Services;
using EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.Athletes.Listing;
using EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.Participants.Listing;
using EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.EnduranceEvents;
using EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.EnduranceEvents.Listing;
using EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.Horses.Listing;
using Prism.Commands;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.ConfigurationMenu
{
    public class EventNavigationStripViewModel : ViewModelBase
    {
        public EventNavigationStripViewModel(INavigationService navigation)
        {
            this.Event = new DelegateCommand(navigation.ChangeTo<EnduranceEventView>);
            this.AthleteList = new DelegateCommand(navigation.ChangeTo<AthleteScrollableView>);
            this.HorseList = new DelegateCommand(navigation.ChangeTo<HorseListView>);
            this.ParticipantList = new DelegateCommand(navigation.ChangeTo<ParticipantListView>);
        }

        public DelegateCommand Event { get; }
        public DelegateCommand AthleteList { get; }
        public DelegateCommand HorseList { get; }
        public DelegateCommand ParticipantList { get; }
    }
}
