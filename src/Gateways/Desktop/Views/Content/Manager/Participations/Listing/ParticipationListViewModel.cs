using EnduranceJudge.Application.Contracts;
using EnduranceJudge.Application.Core.Models;
using EnduranceJudge.Gateways.Desktop.Core.ViewModels;
using EnduranceJudge.Gateways.Desktop.Services;
using System.Collections.Generic;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Manager.Participations.Listing
{
    public class ParticipationListViewModel : SearchableListViewModelBase<ManagerView>
    {
        public ParticipationListViewModel(
            IPersistence persistence,
            INavigationService navigation,
            IDomainHandler domainHandler)
            : base(navigation, domainHandler, persistence)
        {
        }

        protected override IEnumerable<ListItemModel> LoadData() => throw new System.NotImplementedException();
        protected override void RemoveDomain(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
