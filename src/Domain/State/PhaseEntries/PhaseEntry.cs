﻿using EnduranceJudge.Domain.Aggregates.Manager.ResultsInPhases;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Domain.State.Phases;
using System;

namespace EnduranceJudge.Domain.State.PhaseEntries
{
    public class PhaseEntry : DomainObjectBase<PhaseEntryException>
    {
        private PhaseEntry() {}
        public PhaseEntry(Phase phase, DateTime startTime)
        {
            this.Phase = phase;
            this.StartTime = startTime;
        }

        public Phase Phase { get; }
        public DateTime StartTime { get; }
        public DateTime? ArrivalTime { get; internal set; }
        public DateTime? InspectionTime { get; internal set; }
        public DateTime? ReInspectionTime { get; internal set; }
        // TODO: Rename
        public ResultInPhase Result { get; internal set; }
    }
}