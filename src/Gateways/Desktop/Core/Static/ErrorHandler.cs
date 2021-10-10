using EnduranceJudge.Core.Utilities;
using EnduranceJudge.Gateways.Desktop.Core.Objects;
using EnduranceJudge.Gateways.Desktop.Services;
using EnduranceJudge.Gateways.Desktop.Views.Error;
using Prism.Events;

namespace EnduranceJudge.Gateways.Desktop.Core.Static
{
    public static class ErrorHandler
    {
        private static INavigationService navigation;

        public static void Initialize()
        {
            Subscribe();
        }

        private static void Subscribe()
        {
            var eventAggregator = StaticProvider.GetService<IEventAggregator>();
            navigation = navigation ?? StaticProvider.GetService<INavigationService>();

            eventAggregator
                .GetEvent<ErrorEvent>()
                .Subscribe(NavigateToError);
        }

        private static void NavigateToError(string message)
        {
            var parameter = new NavigationParameter(DesktopConstants.MESSAGE_PARAMETER, message);
            navigation.ChangeTo<ErrorView>(parameter);
        }
    }
}
