using AutoMapper;
using EnduranceJudge.Application.Events.Common;
using EnduranceJudge.Core.Extensions;
using EnduranceJudge.Core.Mappings;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Children.PhasesForCategory.Configuration
{
    public class PhaseForCategoryMaps : ICustomMapConfiguration
    {
        public void AddFromMaps(IProfileExpression profile)
        {
            profile.CreateMap<PhaseForCategoryDependantModel, PhaseForCategoryViewModel>()
                .MapMember(x => x.CategoryId, y => (int)y.Category);
        }

        public void AddToMaps(IProfileExpression profile)
        {
            profile.CreateMap<PhaseForCategoryViewModel, PhaseForCategoryDependantModel>();
        }
    }
}
