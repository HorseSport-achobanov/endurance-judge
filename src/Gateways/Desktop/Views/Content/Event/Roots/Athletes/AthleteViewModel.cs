﻿ using EnduranceJudge.Application.Events.Commands.Athletes;
using EnduranceJudge.Application.Events.Models;
using EnduranceJudge.Application.Events.Queries.GetAthlete;
using EnduranceJudge.Application.Events.Queries.GetCountriesList;
using EnduranceJudge.Core.Models;
using EnduranceJudge.Domain.Enums;
using EnduranceJudge.Domain.States;
using EnduranceJudge.Gateways.Desktop.Core.Components.Templates.SimpleListItem;
using EnduranceJudge.Gateways.Desktop.Core.Static;
using EnduranceJudge.Gateways.Desktop.Core.ViewModels;
 using EnduranceJudge.Gateways.Desktop.Events.Athletes;
 using Prism.Events;
 using Prism.Regions;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.Athletes
{
    public class AthleteViewModel : RootFormBase<GetAthlete, UpdateAthlete, AthleteRootModel, AthleteView>,
        IAthleteState,
        IListable
    {
        private readonly IEventAggregator eventAggregator;
        private AthleteViewModel(IApplicationService application, IEventAggregator eventAggregator) : base(application)
        {
            this.eventAggregator = eventAggregator;
            this.CategoryId = (int)Category.Adults;
            this.CountryIsoCode = "BUL";
        }

        public ObservableCollection<SimpleListItemViewModel> CategoryItems { get; }
            = new(SimpleListItemViewModel.FromEnum<Category>());
        public ObservableCollection<CountryListModel> CountryItems { get; }
            = new(Enumerable.Empty<CountryListModel>());

        private string feiId;
        private string firstName;
        private string lastName;
        private string countryIsoCode;
        private int categoryId;
        private string club;

        public override void OnNavigatedTo(NavigationContext context)
        {
            base.OnNavigatedTo(context);
            this.LoadCountries();
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
        public string CountryIsoCode
        {
            get => this.countryIsoCode;
            set => this.SetProperty(ref this.countryIsoCode, value);
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

        private async Task LoadCountries()
        {
            var countries = await this.Application.Execute(new GetCountriesList());
            this.CountryItems.AddRange(countries);
        }

        public Category Category => (Category)this.CategoryId;
        public string Name => $"{this.FirstName} {this.LastName}";

        protected override async Task SubmitAction()
        {
            await base.SubmitAction();
            this.eventAggregator
                .GetEvent<AthleteUpdatedEvent>()
                .Publish(this);
        }
    }
}
