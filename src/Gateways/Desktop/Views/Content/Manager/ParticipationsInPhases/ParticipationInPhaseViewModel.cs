using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.Aggregates.Manager.ParticipationsInPhases;
using EnduranceJudge.Gateways.Desktop.Core;
using System;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Manager.ParticipationsInPhases
{
    public class ParticipationInPhaseViewModel : ViewModelBase, IMapFrom<PhaseEntryManager>
    {
        public int OrderBy { get; private set; }

        private DateTime startTime;
        private DateTime? arrivalTime;
        private DateTime? inspectionTime;
        private DateTime? reInspectionTime;
        private int phaseLengthInKm;
        private TimeSpan loopSpan;
        private TimeSpan phaseSpan;
        private double? averageSpeedForLoopInKpH;
        public double? averageSpeedForPhaseInKpH;
        public bool isComplete;

        public DateTime StartTime
        {
            get => this.startTime;
            private set => this.SetProperty(ref this.startTime, value);
        }
        public DateTime? ArrivalTime
        {
            get => this.arrivalTime;
            private set => this.SetProperty(ref this.arrivalTime, value);
        }
        public DateTime? InspectionTime
        {
            get => this.inspectionTime;
            private set => this.SetProperty(ref this.inspectionTime, value);
        }
        public DateTime? ReInspectionTime
        {
            get => this.reInspectionTime;
            private set => this.SetProperty(ref this.reInspectionTime, value);
        }
        public int PhaseLengthInKm
        {
            get => this.phaseLengthInKm;
            private set => this.SetProperty(ref this.phaseLengthInKm, value);
        }

        public TimeSpan LoopSpan
        {
            get => this.loopSpan;
            private set => this.SetProperty(ref this.loopSpan, value);
        }
        public TimeSpan PhaseSpan
        {
            get => this.phaseSpan;
            private set => this.SetProperty(ref this.phaseSpan, value);
        }
        public double? AverageSpeedForLoopInKpH
        {
            get => this.averageSpeedForLoopInKpH;
            private set => this.SetProperty(ref this.averageSpeedForLoopInKpH, value);
        }
        public double? AverageSpeedForPhaseInKpH
        {
            get => this.averageSpeedForPhaseInKpH;
            private set => this.SetProperty(ref this.averageSpeedForPhaseInKpH, value);
        }
        public bool IsComplete
        {
            get => this.isComplete;
            private set => this.SetProperty(ref this.isComplete, value);
        }
    }
}
