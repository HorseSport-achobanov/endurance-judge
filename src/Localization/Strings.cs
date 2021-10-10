namespace EnduranceJudge.Localization
{
    public class Strings
    {
        public static class Domain
        {
            public const string IsRequiredTemplate = "property '{0}' is required.";
            public const string CannotRemoveNullItemTemplate = "cannot remove 'null' from collection..";
            public const string CannotRemoveItemIsNotFoundTemplate = "cannot remove '{0}' - it is not found.";
            public const string CannotAddNullItemTemplate = "cannot add 'null' to a collection.";
            public const string CannotAddItemExistsTemplate = "cannot add '{0}' because entity with Id '{1}' already exists.";
            public const string INVALID_FUTURE_DATE_TEMPLATE = "Date '{0}' is not future date.";

            public static class Manager
            {
                public static class Participation
                {
                    public const string HAS_NOT_STARTED = "has not started yet";
                    public const string CAN_ONLY_BE_COMPLETED = "can only be completed.";
                    public const string CANNOT_BE_COMPLETED = "cannot be completed";
                }
            }

            public static class Ranking
            {
                public const string INCOMPLETE_PARTICIPATIONS = " is invalid - contains uncomplete competitions";
            }

            public static class Countries
            {
                public const string BULGARIA = "Bulgaria";
            }
        }

        public static class Application
        {
            public const string UNSUPPORTED_IMPORT_FILE_TEMPLATE =  "Unsupported file. Please use '{0}' or '{1}'.";
            public const string COMPETITIONS_WITH_DIFFERENT_START_TIMES =
                "Invalid configuration. Competitions have different starttimes.";
            public const string NO_COMPETITIONS = "There are no competitions";
        }

        public static class Desktop
        {
            public const string PARTICIPANT_NOT_FOUND = "Participant not found.";
            public const string PARTICIPANT_HAS_COMPLETED_THE_PHASE = "Participant has completed the phase.";
        }
    }
}
