using EnduranceJudge.Application.Aggregates.Configurations.Contracts;
using EnduranceJudge.Application.Contracts;
using EnduranceJudge.Application.Core.Models;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.Aggregates.Configuration;
using EnduranceJudge.Domain.State.Participants;
using EnduranceJudge.Gateways.Desktop.Core.ViewModels;
using EnduranceJudge.Gateways.Desktop.Services;
using System.Collections.Generic;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.Participants.Listing
{
    public class ParticipantListViewModel : SearchableListViewModelBase<ParticipantView>
    {
        private readonly IQueries<Participant> participants;

        public ParticipantListViewModel(
            IQueries<Participant> participants,
            IPersistence persistence,
            INavigationService navigation,
            IDomainHandler domainHandler) : base(navigation, domainHandler, persistence)
        {
            this.participants = participants;
        }

        protected override IEnumerable<ListItemModel> LoadData()
        {
            var participants = this.participants
                .GetAll()
                .MapEnumerable<ListItemModel>();
            return participants;
        }

        protected override void RemoveDomain(int id)
        {
            var configurations = new ConfigurationManager();
            configurations.Participants.Remove(id);
        }
    }
}
