using EnduranceJudge.Gateways.Desktop.Core;
using System.Windows.Controls;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Manager
{
    public partial class ManagerView : UserControl, IView
    {
        public ManagerView()
        {
            InitializeComponent();
        }

        public string RegionName { get; } = Regions.CONTENT_LEFT;
    }
}
