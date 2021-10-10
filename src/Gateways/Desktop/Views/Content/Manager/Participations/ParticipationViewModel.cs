using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.Aggregates.Manager.Participations;
using EnduranceJudge.Gateways.Desktop.Core;
using EnduranceJudge.Gateways.Desktop.Views.Content.Manager.ParticipationsInPhases;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Manager.Participations
{
    public class ParticipationViewModel : ViewModelBase
    {
        public ParticipationViewModel(int number, Participation participation)
        {
            this.number = number;
            this.Participation = participation;
            this.UpdatePhases(participation);
        }

        private int number;
        private Visibility visibility = Visibility.Visible;
        public ObservableCollection<ParticipationInPhaseViewModel> ParticipationsInPhases { get; } = new();
        public ObservableCollection<int> PhaseLengths { get; } = new();

        public Participation Participation { get; }
        public int Number
        {
            get => this.number;
            private set => this.SetProperty(ref this.number, value);
        }
        public Visibility Visibility
        {
            get => this.visibility;
            set => this.SetProperty(ref this.visibility, value);
        }

        private void UpdatePhases(Participation participation)
        {
            var participationInCompetition = participation.ParticipationsInCompetitions[0];
            var lengths = participationInCompetition.Phases.Select(x => x.LengthInKm);
            var viewModels = participationInCompetition
                .ParticipationsInPhases
                .MapEnumerable<ParticipationInPhaseViewModel>();

            this.ParticipationsInPhases.AddRange(viewModels);
            this.PhaseLengths.AddRange(lengths);
        }
    }
}
