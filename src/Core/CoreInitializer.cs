using AutoMapper;
using EnduranceJudge.Core.Services;
using EnduranceJudge.Core.Mappings;
using System;
using System.Threading.Tasks;

namespace EnduranceJudge.Core
{
    public class CoreInitializer : IInitializer
    {
        private readonly IMapper mapper;

        public CoreInitializer(IMapper mapper)
            => this.mapper = mapper;

        public int RunningOrder => 0;

        public void Run(IServiceProvider serviceProvider)
        {
            MappingApi.Initialize(this.mapper);
        }
    }
}
