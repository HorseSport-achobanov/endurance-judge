﻿using EnduranceJudge.Gateways.Desktop.Core;
using EnduranceJudge.Gateways.Desktop.Core.Static;
using System.Windows.Controls;
using System.Windows.Input;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Roots.Participants
{
    public partial class ParticipantView : UserControl, IScrollableView
    {
        private readonly IMouseInputService mouseInput;

        public ParticipantView()
        {
            InitializeComponent();
        }

        public ParticipantView(IMouseInputService mouseInput) : this()
        {
            this.mouseInput = mouseInput;
        }

        public string RegionName { get; } = Regions.CONTENT_LEFT;

        public void HandleScroll(object sender, MouseWheelEventArgs mouseEvent)
        {
            this.mouseInput.HandleScroll(sender, mouseEvent);
        }
    }
}