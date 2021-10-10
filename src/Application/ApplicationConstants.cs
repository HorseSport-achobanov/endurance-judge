using EnduranceJudge.Core.Utilities;
using System.Reflection;

namespace EnduranceJudge.Application
{
    public static class ApplicationConstants
    {
        public static Assembly[] Assemblies
        {
            get
            {
                var assemblies = ReflectionUtilities.GetAssemblies("EnduranceJudge.Application");
                return assemblies;
            }
        }

        public const string STORAGE_FILE_NAME = "endurance-judge-data";

        public static class FileExtensions
        {
            public const string Xml = ".xml";
            public const string SupportedExcel = ".xlsx";
        }

        public static class ExcelMaps
        {
            public static class ImportNational
            {
                public const int FIRST_ENTRY_ROW = 4;
                public const int FEI_ID_COLUMN = 27;
                public const int NAME_COLUMN = 2;
                public const int BREED_COLUMN = 22;
                public const int CLUB_COLUMN = 30;
            }
        }
    }
}
