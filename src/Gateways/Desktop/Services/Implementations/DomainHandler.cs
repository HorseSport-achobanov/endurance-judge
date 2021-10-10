using EnduranceJudge.Gateways.Desktop.Core.Objects;
using Prism.Events;
using System;

namespace EnduranceJudge.Gateways.Desktop.Services.Implementations
{
    public class DomainHandler : IDomainHandler
    {
        private readonly IEventAggregator eventAggregator;

        public DomainHandler(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public bool Handle(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                this.Handle(exception);
                return false;
            }
            return true;
        }

        private void Handle(Exception exception)
        {
            exception = GetInnermostException(exception);
            this.eventAggregator
                .GetEvent<ErrorEvent>()
                .Publish(exception.ToString());
        }

        private static Exception GetInnermostException(Exception exception)
        {
            if (exception.InnerException != null)
            {
                return GetInnermostException(exception.InnerException);
            }

            return exception;
        }
    }
}
