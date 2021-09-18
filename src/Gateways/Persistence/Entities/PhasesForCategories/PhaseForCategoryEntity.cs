using EnduranceJudge.Application.Events.Common;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.Aggregates.Event.PhasesForCategory;
using EnduranceJudge.Domain.Aggregates.Manager.DTOs;
using EnduranceJudge.Domain.Enums;
using EnduranceJudge.Domain.States;
using EnduranceJudge.Gateways.Persistence.Core;
using EnduranceJudge.Gateways.Persistence.Entities.Phases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EnduranceJudge.Gateways.Persistence.Entities.PhasesForCategories
{
    public class PhaseForCategoryEntity : EntityBase, IPhaseForCategoryState,
        IMap<PhaseForCategory>,
        IMapTo<PhaseForCategoryDependantModel>,
        IMapTo<PhaseDto>
    {
        private static readonly Type Domain = typeof(PhaseForCategory);

        public int MaxRecoveryTimeInMinutes { get; set; }
        public int RestTimeInMinutes { get; set; }
        public Category Category { get; set; }

        [JsonIgnore]
        public PhaseEntity Phase { get; set; }
        public int PhaseId { get; set; }

        public override IEnumerable<Type> DomainTypes { get; } = new[] { Domain };
    }
}
