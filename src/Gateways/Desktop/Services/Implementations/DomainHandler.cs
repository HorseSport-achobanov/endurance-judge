using EnduranceJudge.Gateways.Desktop.Core.Objects;
using Prism.Events;
using System;
using System.Threading.Tasks;

namespace EnduranceJudge.Gateways.Desktop.Services.Implementations
{
    public class DomainHandler : IDomainHandler
    {
        private readonly IEventAggregator eventAggregator;

        public DomainHandler(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
        }

        public async Task<T> Handle<T>(Func<T> action)
        {
            try
            {
                var result = action();
                return result;
            }
            catch (Exception exception)
            {
                this.Handle(exception);
                return default;
            }
        }

        public void Handle(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                this.Handle(exception);
            }
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
