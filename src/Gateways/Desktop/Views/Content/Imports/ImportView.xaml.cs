using EnduranceJudge.Gateways.Desktop.Core;
using System.Windows.Controls;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Imports
{
    public partial class ImportView : UserControl, IView
    {
        public string RegionName { get; } = Regions.CONTENT_LEFT;

        public ImportView()
        {
            InitializeComponent();
        }
    }
}
