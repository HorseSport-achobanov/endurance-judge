using EnduranceJudge.Gateways.Desktop.Core;
using System.Windows.Controls;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Rankings.Categorizations.Listing
{
    public partial class CategorizationListView : UserControl, IView
    {
        public CategorizationListView()
        {
            InitializeComponent();
        }

        public string RegionName { get; } = Regions.CONTENT_RIGHT;
    }
}
