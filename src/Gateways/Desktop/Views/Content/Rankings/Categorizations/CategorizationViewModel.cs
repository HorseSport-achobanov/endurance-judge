using EnduranceJudge.Domain.Aggregates.Rankings.Participations;
using EnduranceJudge.Domain.Aggregates.Rankings.Stateless.Categorizations;
using EnduranceJudge.Domain.Aggregates.Rankings.Stateless.Classifications;
using EnduranceJudge.Gateways.Desktop.Core;
using EnduranceJudge.Gateways.Desktop.Core.Extensions;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Rankings.Categorizations
{
    public class CategorizationViewModel : ViewModelBase
    {
        private Categorization categorization;

        public CategorizationViewModel()
        {
            this.SelectKidsCategory = new DelegateCommand(this.SelectKidsCategoryAction);
            this.SelectAdultsCategory = new DelegateCommand(this.SelectAdultsCategoryAction);
        }

        public DelegateCommand SelectKidsCategory { get; }
        public DelegateCommand SelectAdultsCategory { get; }

        public ObservableCollection<Participation> RankList { get; } = new();
        private string totalLengthInKm;
        private string categoryName;
        private bool hasKidsClassification;
        private bool hasAdultsClassification;

        public override void OnNavigatedTo(NavigationContext context)
        {
            base.OnNavigatedTo(context);
            var data = context.GetData();
            if (data is not Categorization categorization)
            {
                throw new InvalidOperationException(
                    $"Data is of type '{data.GetType()}'. Expected type is 'Classification'");
            }
            this.categorization = categorization;

            this.HasAdultsClassification = categorization.AdultsClassification != null;
            this.HasKidsClassification = categorization.KidsClassification != null;
            this.SelectDefault();
        }

        public string TotalLengthInKm
        {
            get => this.totalLengthInKm;
            set => this.SetProperty(ref this.totalLengthInKm, value);
        }
        public string CategoryName
        {
            get => this.categoryName;
            set => this.SetProperty(ref this.categoryName, value);
        }
        public bool HasKidsClassification
        {
            get => this.hasKidsClassification;
            set => this.SetProperty(ref this.hasKidsClassification, value);
        }
        public bool HasAdultsClassification
        {
            get => this.hasAdultsClassification;
            set => this.SetProperty(ref this.hasAdultsClassification, value);
        }

        private void SelectKidsCategoryAction()
        {
            this.Select(this.categorization.KidsClassification);
        }
        private void SelectAdultsCategoryAction()
        {
            this.Select(this.categorization.AdultsClassification);
        }

        private void SelectDefault()
        {
            var defaultClassification = this.categorization.AdultsClassification
                ?? this.categorization.KidsClassification;
            this.Select(defaultClassification);
        }

        private void Select(Classification classification)
        {
            this.TotalLengthInKm = classification.TotalLengthInKm.ToString();
            this.CategoryName = classification.Category.ToString();
            this.RankList.Clear();
            this.RankList.AddRange(classification.RankList);
        }
    }
}
