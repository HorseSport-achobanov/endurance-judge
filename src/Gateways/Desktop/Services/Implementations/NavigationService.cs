using EnduranceJudge.Gateways.Desktop.Core;
using EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.EnduranceEvents.Listing;
using EnduranceJudge.Gateways.Desktop.Views.Content.Event.NavigationStrip;
using EnduranceJudge.Gateways.Desktop.Views.Content.Import;
using EnduranceJudge.Gateways.Desktop.Core.Services.Implementations;
using EnduranceJudge.Gateways.Desktop.Views.Content.Manager.Participations;
using EnduranceJudge.Gateways.Desktop.Views.Content.Manager.Participations.Listing;
using EnduranceJudge.Gateways.Desktop.Views.Content.Rankings.Categorizations.Listing;
using Prism.Regions;
using System;

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
            this.ChangeTo<EnduranceEventListView>();
            this.ChangeTo<EventNavigationStripView>(Regions.CONTENT_RIGHT);
       }

        public void NavigateToManager()
        {
            this.ChangeTo<ParticipationView>();
            this.ChangeTo<ParticipationListView>(Regions.CONTENT_RIGHT);
        }
        public void NavigateToRanking()
        {
            this.ChangeTo<CategorizationListView>(Regions.CONTENT_RIGHT);
            this.ClearRegion(Regions.CONTENT_LEFT);
        }

        public void ChangeTo<T>() where T : IView
        {
            this.ChangeTo(Regions.CONTENT_LEFT, typeof(T), null);
        }

        public void ChangeTo<T>(int entityId)
        {
            var parameter = new NavigationParameter(DesktopConstants.EntityIdParameter, entityId);
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
