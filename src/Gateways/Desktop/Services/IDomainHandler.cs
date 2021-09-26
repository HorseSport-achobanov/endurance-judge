using EnduranceJudge.Core.ConventionalServices;
using System;
using System.Threading.Tasks;

namespace EnduranceJudge.Gateways.Desktop.Services
{
    public interface IDomainHandler : IService
    {
        Task<T> Handle<T>(Func<T> action);
        public void Handle(Action action);
    }
}
