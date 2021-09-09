﻿using EnduranceJudge.Gateways.Desktop.Core;
using EnduranceJudge.Gateways.Desktop.Core.Services;
using System.Windows.Controls;
using System.Windows.Input;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Children.Phases
{
    public partial class PhaseView : UserControl, IScrollableView
    {
        private readonly IMouseInputService mouseInput;

        public PhaseView()
        {
            InitializeComponent();
        }

        public PhaseView(IMouseInputService mouseInput) : this()
        {
            this.mouseInput = mouseInput;
        }

        public string RegionName { get; } = Regions.Content;

        public void HandleScroll(object sender, MouseWheelEventArgs mouseEvent)
        {
            this.mouseInput.HandleScroll(sender, mouseEvent);
        }
    }
}