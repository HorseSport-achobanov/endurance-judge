using EnduranceJudge.Gateways.Desktop.Core;
using EnduranceJudge.Gateways.Desktop.Views.Content.Event.ConfigurationMenu;
using EnduranceJudge.Gateways.Desktop.Views.Content.Imports;
using EnduranceJudge.Gateways.Desktop.Core.Services.Implementations;
using EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.EnduranceEvents;
using EnduranceJudge.Gateways.Desktop.Views.Content.Manager;
using EnduranceJudge.Gateways.Desktop.Views.Content.Manager.Participations.Listing;
using EnduranceJudge.Gateways.Desktop.Views.Content.Rankings.Categorizations.Listing;
using Prism.Regions;
using System;
using static EnduranceJudge.Gateways.Desktop.DesktopConstants;

namespace EnduranceJudge.Gateways.Desktop.Services.Implementations
{
    public class NavigationService : NavigationServiceBase, INavigationService
    {
        public NavigationService(IRegionManager regionManager) : base(regionManager)
        {
        }

        public void NavigateToImport()
        {
            this.ChangeTo<ImportView>();
            this.ClearRegion(Regions.CONTENT_RIGHT);
        }

        public void NavigateToEvent()
        {
            this.ChangeTo<EnduranceEventView>();
            this.ChangeTo<ConfigurationMenuView>(Regions.CONTENT_RIGHT);
        }

        public void NavigateToManager()
        {
            this.ChangeTo<ManagerView>();
            this.ChangeTo<ParticipationListView>(Regions.CONTENT_RIGHT);
        }
        public void NavigateToRanking()
        {
            this.ChangeTo<CategorizationListView>(Regions.CONTENT_RIGHT);
            this.ClearRegion(Regions.CONTENT_LEFT);
        }

        public void ChangeTo<T>()
            where T : IView
        {
            this.ChangeTo(typeof(T));
        }

        public void ChangeToNewForm<T>(int principalId)
            where T : IView
        {
            var parameter = new NavigationParameter(Parameters.PRINCIPAL_ID, principalId);
            this.ChangeTo(typeof(T), parameter);
        }

        public void ChangeToUpdateForm<T>(int id)
            where T : IView
        {
            var parameter = new NavigationParameter(Parameters.ID, id);
            this.ChangeTo(typeof(T), parameter);
        }

        public void ChangeTo<T>(params NavigationParameter[] parameters)
        {
            this.ChangeTo(typeof(T), parameters);
        }

        public void ChangeTo(Type view, params NavigationParameter[] parameters)
        {
            var navigationParameters = new NavigationParameters();
            foreach (var (key, value) in parameters)
            {
                navigationParameters.Add(key, value);
            }

            this.ChangeTo(Regions.CONTENT_LEFT, view, navigationParameters);
        }
    }
}
