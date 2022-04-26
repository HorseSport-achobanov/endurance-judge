﻿using EnduranceJudge.Domain.AggregateRoots.Ranking.Aggregates;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace EnduranceJudge.Gateways.Desktop.Controls.Ranking;

public partial class RanklistControl
{
    public RanklistControl(RankList rankList) : this()
    {
        this.Populate(rankList);
    }
    public RanklistControl()
    {
        this.InitializeComponent();
    }

    public RankList Ranklist
    {
        get => (RankList)this.GetValue(RANKLIST_PROPERTY);
        set => this.SetValue(RANKLIST_PROPERTY, value);
    }

    public static readonly DependencyProperty RANKLIST_PROPERTY =
        DependencyProperty.Register(
            nameof(Ranklist),
            typeof(RankList),
            typeof(RanklistControl),
            new PropertyMetadata(OnRanklistChanged));

    private static void OnRanklistChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        var control = (RanklistControl)sender;
        var participation = (RankList)args.NewValue;
        control.Populate(participation);
    }

    private void Populate(RankList rankList)
    {
        for (var i = 0; i < rankList.Count; i++)
        {
            var rank = i + 1;
            var participation = rankList[i];
            var result = new ParticipationResultModel(rank, participation);
            var control = new ParticipationResultControl(result);
            this.Children.Add(control);
        }
    }
}