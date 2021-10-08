﻿using EnduranceJudge.Domain.Core.Exceptions;

namespace EnduranceJudge.Domain.Aggregates.Import
{
    public class ImportException : DomainException
    {
        protected override string Entity { get; } = nameof(ImportManager);
    }
}