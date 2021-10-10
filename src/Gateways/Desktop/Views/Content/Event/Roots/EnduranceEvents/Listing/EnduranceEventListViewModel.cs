using EnduranceJudge.Application.Contracts;
using EnduranceJudge.Application.Core.Models;
using EnduranceJudge.Gateways.Desktop.Core.Components.Templates.ListItem;
using EnduranceJudge.Gateways.Desktop.Core.ViewModels;
using EnduranceJudge.Gateways.Desktop.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.EnduranceEvents.Listing
{
    public class EnduranceEventListViewModel : ListViewModelBase<EnduranceEventView>
    {
        public EnduranceEventListViewModel(
            IPersistence persistence,
            INavigationService navigation,
            IDomainHandler domainHandler)
            : base (navigation, domainHandler, persistence)
        {
        }

        public ObservableCollection<ListItemViewModel> EnduranceEvents => this.ListItems;
        protected override IEnumerable<ListItemModel> LoadData() => throw new System.NotImplementedException();
        protected override void RemoveDomain(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
