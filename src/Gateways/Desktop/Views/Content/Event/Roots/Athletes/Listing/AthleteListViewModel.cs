using EnduranceJudge.Application.Aggregates.Configurations.Contracts;
using EnduranceJudge.Application.Contracts;
using EnduranceJudge.Application.Core.Models;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.Aggregates.Configuration;
using EnduranceJudge.Domain.State.Athletes;
using EnduranceJudge.Gateways.Desktop.Core.ViewModels;
using EnduranceJudge.Gateways.Desktop.Services;
using System.Collections.Generic;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.Athletes.Listing
{
    public class AthleteListViewModel : SearchableListViewModelBase<AthleteView>
    {
        private readonly IQueries<Athlete> athletes;
        public AthleteListViewModel(
            IPersistence persistence,
            IQueries<Athlete> athletes,
            INavigationService navigation,
            IDomainHandler domainHandler) : base(navigation, domainHandler, persistence)
        {
            this.athletes = athletes;
        }

        protected override IEnumerable<ListItemModel> LoadData()
        {
            var athletes = this.athletes
                .GetAll()
                .MapEnumerable<ListItemModel>();
            return athletes;
        }
        protected override void RemoveDomain(int id)
        {
            var configuration = new ConfigurationManager();
            configuration.Athletes.Remove(id);
        }
    }
}
