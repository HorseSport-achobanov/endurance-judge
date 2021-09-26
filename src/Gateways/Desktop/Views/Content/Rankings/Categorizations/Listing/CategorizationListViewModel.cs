using EnduranceJudge.Application.Actions.Ranking.Queries.GetCompetitionList;
using EnduranceJudge.Gateways.Desktop.Core;
using EnduranceJudge.Domain.Aggregates.Rankings.Stateless.Rankings;
using EnduranceJudge.Gateways.Desktop.Core.Components.Templates.ListItem;
using EnduranceJudge.Gateways.Desktop.Core.Static;
using EnduranceJudge.Gateways.Desktop.Services;
using Prism.Commands;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Rankings.Categorizations.Listing
{
    public class CategorizationListViewModel : ViewModelBase
    {
        private readonly IApplicationService application;
        private readonly INavigationService navigation;
        private Ranking ranking;

        public CategorizationListViewModel(IApplicationService application, INavigationService navigation)
        {
            this.application = application;
            this.navigation = navigation;
        }

        public ObservableCollection<ListItemViewModel> CategorizationItems { get; } = new();

        public override void OnNavigatedTo(NavigationContext context)
        {
            base.OnNavigatedTo(context);
            this.Load();
        }

        private async Task Load()
        {
            var query = new GetCompetitionList();
            var competitions = await this.application.Execute(query);
            this.ranking = new Ranking(competitions);
            var items = ranking.Categorizations.Select(this.ToListItem);
            this.CategorizationItems.AddRange(items);
        }

        private ListItemViewModel ToListItem(Domain.Aggregates.Rankings.Stateless.Categorizations.Categorization categorization)
        {
            var command = new DelegateCommand<int?>(this.NavigateToClassification);
            var listItem = new ListItemViewModel(categorization.Length, command);
            return listItem;
        }

        private void NavigateToClassification(int? length)
        {
            var categorization = this.ranking.Categorizations.First(x => x.Length == length!.Value);
            var dataParameter = new NavigationParameter(DesktopConstants.DataParameter, categorization);
            this.navigation.ChangeTo<CategorizationView>(dataParameter);
        }
    }
}
