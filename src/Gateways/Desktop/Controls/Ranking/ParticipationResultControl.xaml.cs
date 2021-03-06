using System.Windows;

namespace EnduranceJudge.Gateways.Desktop.Controls.Ranking;

public partial class ParticipationResultControl
{
    public ParticipationResultControl(ParticipationResultModel participation) : this()
    {
        this.Populate(participation);
    }
    public ParticipationResultControl()
    {
        InitializeComponent();
    }

    public ParticipationResultModel Participation
    {
        get => (ParticipationResultModel)this.GetValue(PARTICIPATION_PROPERTY);
        set => this.SetValue(PARTICIPATION_PROPERTY, value);
    }

    public static readonly DependencyProperty PARTICIPATION_PROPERTY =
        DependencyProperty.Register(
            nameof(Participation),
            typeof(ParticipationResultModel),
            typeof(ParticipationResultControl),
            new PropertyMetadata(OnParticipationChanged));

    private static void OnParticipationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        var grid = (ParticipationResultControl)sender;
        var participation = (ParticipationResultModel) args.NewValue;
        grid.Populate(participation);
    }

    private void Populate(ParticipationResultModel participation)
    {
        this.NumberText.Text = participation.Participant.Number.ToString();
        this.AthleteNameText.Text = participation.Participant.Athlete.Name;
        this.AthleteFeiIdText.Text = participation.Participant.Athlete.FeiId;
        this.CountryText.Text = participation.Participant.Athlete.Country.Name;
        this.HorseNameText.Text = participation.Participant.Horse.Name;
        this.HorseFeiIdText.Text = participation.Participant.Horse.FeiId;
        this.RankText.Text = participation.Rank.ToString();
        this.TotalTime.Text = participation.TotalTime;
        this.TotalAverageSpeedString.Text = participation.TotalAverageSpeed;
        this.NotQualifiedText.Text = participation.NotQualifiedText;
        this.DisqualifiedContainer.Visibility = participation.DisqualifiedVisibility;
        this.ParticipationGrid.Participation = participation;
    }
}
