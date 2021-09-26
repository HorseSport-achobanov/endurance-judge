using EnduranceJudge.Core.ConventionalServices;
using EnduranceJudge.Gateways.Desktop.Core;
using System;

namespace EnduranceJudge.Gateways.Desktop.Services
{
    public interface INavigationService : ISingletonService
    {
        void NavigateToImport();
        void NavigateToEvent();
        void NavigateToManager();
        void NavigateToRanking();
        void ChangeTo<T>()
            where T : IView;
        void ChangeTo<T>(int entityId);
        void ChangeTo<T>(params NavigationParameter[] parameters);
        void ChangeTo(Type view, params NavigationParameter[] parameters);
    }
}
