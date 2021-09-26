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

            public static class Ranking
            {
                public const string INCOMPLETE_PARTICIPATIONS = " is invalid - contains uncomplete competitions";
            }
        }

        public static class Application
        {
            public const string UnsupportedImportFileTemplate =  "Unsupported file. Please use '{0}' or '{1}'.";
        }
    }
}
