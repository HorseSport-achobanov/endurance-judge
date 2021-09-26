using EnduranceJudge.Gateways.Desktop.Core;
using EnduranceJudge.Gateways.Desktop.Services;
using EnduranceJudge.Gateways.Desktop.Views.Content.PrintExample;
using Prism.Commands;

namespace EnduranceJudge.Gateways.Desktop.Views.Navigation
{
    public class NavigationViewModel : ViewModelBase
    {
        public NavigationViewModel(INavigationService navigation)
        {
            this.NavigateToImport = new DelegateCommand(navigation.NavigateToImport);
            this.NavigateToEvent = new DelegateCommand(navigation.NavigateToEvent);
            this.NavigateToManager = new DelegateCommand(navigation.NavigateToManager);
            this.NavigateToRanking = new DelegateCommand(navigation.NavigateToRanking);

            this.NavigateToPrintExample = new DelegateCommand(navigation.ChangeTo<PrintExampleView>);
        }

        public DelegateCommand NavigateToImport { get; }
        public DelegateCommand NavigateToEvent { get; }
        public DelegateCommand NavigateToManager { get; }
        public DelegateCommand NavigateToPrintExample { get; }
        public DelegateCommand NavigateToRanking { get; }

    }
}
