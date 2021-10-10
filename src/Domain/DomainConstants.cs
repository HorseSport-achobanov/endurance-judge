using EnduranceJudge.Core.Utilities;
using System.Reflection;

namespace EnduranceJudge.Domain
{
    public static class DomainConstants
    {
        public const string EVENT_DEFAULT_NAME = "Event";
        public const string COUNTRY_DEFAULT_CODE = "BUL";

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
    }
}
