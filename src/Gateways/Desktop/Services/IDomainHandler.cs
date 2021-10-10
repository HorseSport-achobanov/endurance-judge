using EnduranceJudge.Core.ConventionalServices;
using System;
using System.Threading.Tasks;

namespace EnduranceJudge.Gateways.Desktop.Services
{
    public interface IDomainHandler : IService
    {
        public bool Handle(Action action);
    }
}
