using EnduranceJudge.Application.Aggregates.Configurations.Contracts;
using EnduranceJudge.Application.Core.Models;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Core.Models;
using EnduranceJudge.Domain.Aggregates.Configuration;
using EnduranceJudge.Domain.State.Athletes;
using EnduranceJudge.Domain.Enums;
using EnduranceJudge.Domain.State.Countries;
using EnduranceJudge.Gateways.Desktop.Core.Components.Templates.SimpleListItem;
using EnduranceJudge.Gateways.Desktop.Core.ViewModels;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;
using static EnduranceJudge.Localization.Constants;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.Athletes
{
    public class AthleteViewModel : FormBase<AthleteView, Athlete>, IAthleteState, IListable
    {
        private readonly IQueries<Country> countries;
        private readonly IQueries<Athlete> athletes;

        private AthleteViewModel(IQueries<Country> countries, IQueries<Athlete> athletes) : base(athletes)
        {
            this.countries = countries;
            this.athletes = athletes;
            this.CategoryId = (int)Category.Adults;
        }

        public ObservableCollection<SimpleListItemViewModel> CategoryItems { get; }
            = new(SimpleListItemViewModel.FromEnum<Category>());
        public ObservableCollection<ListItemModel> CountryItems { get; } = new();

        private string feiId;
        private string firstName;
        private string lastName;
        private int countryId;
        private int categoryId;
        private string club;

        public override void OnNavigatedTo(NavigationContext context)
        {
            base.OnNavigatedTo(context);
            this.LoadCountries();
        }

        protected override void Load(int id)
        {
            var athlete = this.athletes.GetOne(id);
            this.MapFrom(athlete);
        }
        protected override void ActOnSubmit()
        {
            var configuration = new ConfigurationManager();
            configuration.Athletes.Save(this, this.CountryId);
        }
        private void LoadCountries()
        {
            var countries = this.countries.GetAll();

            var listItems = countries.MapEnumerable<ListItemModel>();
            this.CountryItems.AddRange(listItems);
            this.CountryId = countries
                .First(x => x.IsoCode == DEFAULT_COUNTRY_CODE)
                .Id;
        }

        public string FeiId
        {
            get => this.feiId;
            set => this.SetProperty(ref this.feiId, value);
        }
        public string FirstName
        {
            get => this.firstName;
            set => this.SetProperty(ref this.firstName, value);
        }
        public string LastName
        {
            get => this.lastName;
            set => this.SetProperty(ref this.lastName, value);
        }
        public int CountryId
        {
            get => this.countryId;
            set => this.SetProperty(ref this.countryId, value);
        }
        public int CategoryId
        {
            get => this.categoryId;
            set => this.SetProperty(ref this.categoryId, value);
        }
        public string Club
        {
            get => this.club;
            set => this.SetProperty(ref this.club, value);
        }
        public Category Category => (Category)this.CategoryId;
        public string Name => $"{this.FirstName} {this.LastName}";
    }
}
