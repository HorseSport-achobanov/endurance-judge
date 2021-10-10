using EnduranceJudge.Application.Aggregates.Configurations.Contracts;
using EnduranceJudge.Application.Core.Models;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.Aggregates.Configuration;
using EnduranceJudge.Domain.State.Countries;
using EnduranceJudge.Domain.State.EnduranceEvents;
using EnduranceJudge.Gateways.Desktop.Core.ViewModels;
using EnduranceJudge.Gateways.Desktop.Views.Content.Event.Children.Competitions;
using EnduranceJudge.Gateways.Desktop.Views.Content.Event.Children.Personnel;
using Prism.Commands;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.EnduranceEvents
{
    public class EnduranceEventViewModel : ParentFormBase<EnduranceEventView, EnduranceEvent>
    {
        private readonly IEnduranceEventQuery enduranceEventQuery;
        private readonly IQueries<Country> countryQueries;

        public EnduranceEventViewModel(IEnduranceEventQuery enduranceEventQuery, IQueries<Country> countryQueries)
            : base (enduranceEventQuery)
        {
            this.enduranceEventQuery = enduranceEventQuery;
            this.countryQueries = countryQueries;
            this.CreateCompetition = new DelegateCommand(this.NewForm<CompetitionView>);
            this.CreatePersonnel = new DelegateCommand(this.NewForm<PersonnelView>);
        }

        public DelegateCommand CreatePersonnel { get; }
        public DelegateCommand CreateCompetition { get; }
        public ObservableCollection<ListItemModel> CountryItems { get; } = new();
        public ObservableCollection<PersonnelViewModel> Personnel { get; } = new();
        public ObservableCollection<CompetitionViewModel> Competitions { get; } = new();

        private string name;
        private string populatedPlace;
        private int countryId;

        public override void OnNavigatedTo(NavigationContext context)
        {
            this.Load(default); // Only one Endurance event per state.
            this.LoadCountries();
        }

        protected override void Load(int id)
        {
            var enduranceEvent = this.enduranceEventQuery.Get();
            this.MapFrom(enduranceEvent);
        }
        protected override void ActOnSubmit()
        {
            var configuration = new ConfigurationManager();
            configuration.Update(this.Name, this.CountryId, this.PopulatedPlace);
        }

        public string Name
        {
            get => this.name;
            set => this.SetProperty(ref this.name, value);
        }
        public string PopulatedPlace
        {
            get => this.populatedPlace;
            set => this.SetProperty(ref this.populatedPlace, value);
        }
        public int CountryId
        {
            get => this.countryId;
            set => this.SetProperty(ref this.countryId, value);
        }

        private void LoadCountries()
        {
            var countries = this.countryQueries.GetAll();
            var viewModels = countries.Select(x => new ListItemModel { Id = x.Id, Name = x.Name });
            this.CountryItems.AddRange(viewModels);
        }
    }
}
