using System;
using System.Threading.Tasks;

namespace EnduranceJudge.Core.Services
{
    public interface IInitializer
    {
        int RunningOrder { get; }

        void Run(IServiceProvider serviceProvider);
    }
}
