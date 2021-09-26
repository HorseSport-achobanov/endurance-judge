using EnduranceJudge.Gateways.Desktop.Core;
using System.Windows.Controls;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Rankings.Categorizations
{
    public partial class CategorizationView : UserControl, IView
    {
        public CategorizationView()
        {
            InitializeComponent();
        }
        public string RegionName { get; } = Regions.CONTENT_LEFT;
    }
}
