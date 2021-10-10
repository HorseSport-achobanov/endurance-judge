using EnduranceJudge.Core.Utilities;
using EnduranceJudge.Gateways.Desktop.Core.Components.Templates.ListItem;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.Reflection;

namespace EnduranceJudge.Gateways.Desktop
{
    public static class DesktopConstants
    {
        public const string True = "True";
        public const string False = "False";

        public static Assembly[] Assemblies
        {
            get
            {
                var assemblies = ReflectionUtilities.GetAssemblies("EnduranceJudge.Gateways.Desktop");
                return assemblies;
            }
        }

        public static class Parameters
        {
            public const string ID = "Id";
            public const string PRINCIPAL_ID = "PrincipalId";
        }

        public const string DataParameter = "Parameter1";
        public const string CHILD_DATA_PARAMETER = "Parameter2";
        public const string NewChildIdParameter = "Parameter3";
        public const string UPDATE_PARAMETER = "Parameter4";
        public const string REMOVE_PARAMETER = "Parameter6";
        public const string MESSAGE_PARAMETER = "Parameter5";

        public static class Types
        {
            public static readonly Type DelegateCommand = typeof(DelegateCommand);
            public static readonly Type ObservableListItems = typeof(ObservableCollection<ListItemViewModel>);
        }
    }

    public static class Regions
    {
        public const string HEADER_RIGHT = "Region0";
        public const string HEADER_LEFT = "Region1";
        public const string CONTENT_RIGHT = "Region2";
        public const string CONTENT_LEFT = "Region3";
    }
}
