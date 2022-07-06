using EnduranceJudge.Gateways.Desktop.Core;
using EnduranceJudge.Gateways.Desktop.Core.Services;
using EnduranceJudge.Gateways.Desktop.Events;
using Prism.Events;
using System.Windows.Controls;
using System.Windows.Input;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.PoC;

public partial class PoCView : IView
{
    private readonly IInputHandler inputInput;
    
    public PoCView(IInputHandler inputInput, IEventAggregator eventAggregator) : this()
    {
        this.inputInput = inputInput;
        eventAggregator.GetEvent<SelectTabEvent>().Subscribe(item =>
        {
            ((TabControl) this.FindName("Participations")!).SelectedItem = item;
        });
    }
    public PoCView()
    {
        InitializeComponent();
    }

    public string RegionName => Regions.CONTENT_LEFT;

    public void HandleScroll(object sender, MouseWheelEventArgs args)
    {
        this.inputInput.HandleScroll(sender, args);
    }
}
