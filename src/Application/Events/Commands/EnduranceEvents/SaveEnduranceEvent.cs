using EnduranceJudge.Application.Core.Contracts;
using EnduranceJudge.Application.Core.Handlers;
using EnduranceJudge.Application.Events.Common;
using EnduranceJudge.Application.Events.Factories;
using EnduranceJudge.Application.Events.Queries.GetEvent;
using EnduranceJudge.Domain.Aggregates.Event.EnduranceEvents;
using EnduranceJudge.Domain.States;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EnduranceJudge.Application.Events.Commands.EnduranceEvents
{
    public class SaveEnduranceEvent : IRequest, IEnduranceEventState
    {
        public int Id { get; set;  }
        public string Name { get; set; }
        public string PopulatedPlace { get; set; }
        public string CountryIsoCode { get; set; }
        public IEnumerable<PersonnelDependantModel> Personnel { get; set; }
        public IEnumerable<CompetitionDependantModel> Competitions { get; set;}

        public class SaveEnduranceEventHandler : Handler<SaveEnduranceEvent>
        {
            private readonly IEnduranceEventFactory enduranceEventFactory;
            private readonly ICommands<EnduranceEvent> eventCommands;

            public SaveEnduranceEventHandler(
                IEnduranceEventFactory enduranceEventFactory,
                ICommands<EnduranceEvent> eventCommands)
            {
                this.enduranceEventFactory = enduranceEventFactory;
                this.eventCommands = eventCommands;
            }

            public override async Task DoHandle(SaveEnduranceEvent request, CancellationToken token)
            {
                var enduranceEvent = this.enduranceEventFactory.Create(request);
                await this.eventCommands.Save<EnduranceEventRootModel>(enduranceEvent, token);
            }
        }
    }
}
