using EnduranceJudge.Gateways.Desktop.Core;
using System.Windows.Controls;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.ConfigurationMenu
{
    public partial class ConfigurationMenuView : UserControl, IView
    {
        public ConfigurationMenuView()
        {
            InitializeComponent();
        }

        public string RegionName { get; } = Regions.CONTENT_RIGHT;
    }
}
