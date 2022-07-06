using EnduranceJudge.Domain.State.LapRecords;
using System.Windows;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.PoC.Controls;

public partial class PocLapControl
{
    public PocLapControl()
    {
        InitializeComponent();
    }
    
    public NewLapRecord Lap
    {
        get => (NewLapRecord)GetValue(PERFORMANCE_PROPERTY);
        set => SetValue(PERFORMANCE_PROPERTY, value);
    }
    
    public static readonly DependencyProperty PERFORMANCE_PROPERTY
        = DependencyProperty.Register(nameof(NewLapRecord), typeof(PocLapControl), typeof(NewLapRecord));
}
