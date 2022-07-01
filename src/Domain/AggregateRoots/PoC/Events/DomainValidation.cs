using EnduranceJudge.Domain.Annotations;
using System.Collections.Generic;

namespace EnduranceJudge.Domain.AggregateRoots.PoC.Events;

public delegate void DomainValidationHandler([CanBeNull] object sender, DomainValidationArguments arguments);

public class DomainValidationArguments : Dictionary<string, string>
{
}
