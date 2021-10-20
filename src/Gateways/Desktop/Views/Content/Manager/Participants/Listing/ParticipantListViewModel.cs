﻿using EnduranceJudge.Application.Aggregates.Configurations.Contracts;
using EnduranceJudge.Application.Contracts;
using EnduranceJudge.Application.Core.Models;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.State.Participants;
using EnduranceJudge.Gateways.Desktop.Core.ViewModels;
using EnduranceJudge.Gateways.Desktop.Services;
using System.Collections.Generic;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Manager.Participants.Listing
{
    public class ParticipantListViewModel : SearchableListViewModelBase<ManagerView>
    {
        private readonly IQueries<Participant> participants;
        public ParticipantListViewModel(
            IQueries<Participant> participants,
            IPersistence persistence,
            INavigationService navigation,
            IDomainHandler domainHandler)
            : base(navigation, domainHandler, persistence)
        {
            this.AllowCreate = false;
            this.AllowDelete = false;
            this.participants = participants;
        }

        protected override IEnumerable<ListItemModel> LoadData()
        {
            var participants = this.participants.GetAll();
            var viewModels = participants.MapEnumerable<ListItemModel>();
            return viewModels;
        }
    }
}