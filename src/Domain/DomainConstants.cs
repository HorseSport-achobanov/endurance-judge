using EnduranceJudge.Core.Utilities;
using EnduranceJudge.Domain.State.Competitions;
using EnduranceJudge.Domain.State.Participants;
using EnduranceJudge.Domain.State.Participations;
using EnduranceJudge.Domain.AggregateRoots.Common.Performances;
using EnduranceJudge.Domain.State.Laps;
using System.Reflection;

namespace EnduranceJudge.Domain;

public static class DomainConstants
{
    public const string EVENT_DEFAULT_NAME = "Event";
    public const string COUNTRY_DEFAULT_CODE = "BUL";
    public const string GENERIC_VALIDATION_KEY = "domain_generic";

    public static class Gender
    {
        public const string Female = "F";
        public const string Male = "M";
    }

    public static Assembly[] Assemblies
    {
        get
        {
            var assemblies = ReflectionUtilities.GetAssemblies("EnduranceJudge.Domain");
            return assemblies;
        }
    }

    public static class ErrorMessages
    {
        public const string CANNOT_START_NEXT_PERFORMANCE_NO_LAST_PERFORMANCE = $"Cannot start next {nameof(Performance)}. {nameof(Participation)}s have not started or data is invalid.";
        public const string CANNOT_START_PERFORMANCE_NO_START_TIME = $"cannot start next {nameof(Performance)} - no Start Time.";
        public const string CANNOT_START_PERFORMANCE_NO_PHASE = $"cannot start {nameof(Performance)} - no {nameof(Lap)} found.";
        public const string CANNOT_START_COMPETITION_WITHOUT_PHASES = $"{nameof(Participant)}s cannot start - {nameof(Competition)} has no laps.";
        public const string PARTICIPANT_CANNOT_START_NO_COMPETITION_TEMPLATE = $"{nameof(Participant)} '{{0}}' cannot start, because they don't participate in any {nameof(Competition)}";
        public const string PARTICIPANT_HAS_ALREADY_STARTED = $"{nameof(Participant)} has already started";
        public const string CANNOT_CREATE_PHASE_COMPETITION_DOES_NOT_EXIST = $"Cannot save {nameof(Lap)} - competition with id '{{0}}' does not exit";
        public const string CANNOT_CREATE_PHASE_IT_ALREADY_EXISTS = $"Cannot create {nameof(Lap)}. A {nameof(Lap)} with Id '{{0}}' already exists";
        public const string CANNOT_UPDATE_PHASE_IT_DOES_NOT_EXIST = $"Cannot update {nameof(Lap)} with Id '{{0}}' does not exist";
        public const string PERFORMANCE_INVALID_COMPLETE = $"Performance has invalid state and cannot complete.";
        public const string ARGUMENT_DEFAULT_VALUE = "Value cannot be default";
    }
}
