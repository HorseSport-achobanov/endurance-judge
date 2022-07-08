using EnduranceJudge.Domain.State.Participants;
using System.Windows;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.PoC.Controls;

public partial class PocGridControl
{
    public PocGridControl()
    {
        InitializeComponent();
    }
    
    public NewParticipant Participant
    {
        get => (NewParticipant)GetValue(PARTICIPANT_PROPERTY);
        set => SetValue(PARTICIPANT_PROPERTY, value);
    }
    
    public static readonly DependencyProperty PARTICIPANT_PROPERTY
        = DependencyProperty.Register(
            nameof(PocGridControl.Participant),
            typeof(NewParticipant),
            typeof(PocGridControl));
}
