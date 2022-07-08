using EnduranceJudge.Domain.State.Participations;
using System.Windows;

namespace EnduranceJudge.Gateways.Desktop.Controls.Manager;

public partial class PerformanceLabelsControl
{
    public PerformanceLabelsControl()
    {
        InitializeComponent();
    }

    public string Number
    {
        get => (string) this.GetValue(NUMBER_PROPERTY);
        set => this.SetValue(NUMBER_PROPERTY, value);
    }

    public static readonly DependencyProperty NUMBER_PROPERTY =
        DependencyProperty.Register(
            nameof(Number),
            typeof(string),
            typeof(PerformanceLabelsControl),
            new PropertyMetadata(null));
}
