using EnduranceJudge.Core.Services;
using EnduranceJudge.Core.Utilities;
using EnduranceJudge.Domain.State.Athletes;
using EnduranceJudge.Domain.State.Horses;
using EnduranceJudge.Domain.State.Competitions;
using EnduranceJudge.Domain.State.Participants;
using EnduranceJudge.Domain.State.Personnels;
using EnduranceJudge.Domain.State.PhaseEntries;
using EnduranceJudge.Domain.State.Phases;
using EnduranceJudge.Domain.Enums;
using EnduranceJudge.Domain.State.EnduranceEvents;
using EnduranceJudge.Gateways.Desktop.Startup;
using EnduranceJudge.Gateways.Desktop.Views;
using EnduranceJudge.Gateways.Desktop.Core;
using EnduranceJudge.Gateways.Desktop.Core.Static;
using Microsoft.Extensions.DependencyInjection;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace EnduranceJudge.Gateways.Desktop
{
    public partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry container)
            => container.AddServices();

        protected override Window CreateShell()
        {
            this.InitializeApplication();

            return this.Container.Resolve<ShellWindow>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            base.ConfigureModuleCatalog(moduleCatalog);

            var moduleDescriptors = ReflectionUtilities.GetDescriptors<ModuleBase>(Assembly.GetExecutingAssembly());
            foreach (var descriptor in moduleDescriptors)
            {
                moduleCatalog.AddModule(descriptor.Type);
            }
        }

        private void InitializeApplication()
        {
            var aspNetProvider = this.Container.Resolve<IServiceProvider>();
            InitializeStaticServices(aspNetProvider);
            // this.BigDick();

            var initializers = aspNetProvider.GetServices<IInitializer>();
            foreach (var initializer in initializers.OrderBy(x => x.RunningOrder))
            {
                initializer.Run(aspNetProvider);
            }
        }

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();

            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                var viewNamespace = viewType.Namespace;
                var assembly = viewType.Assembly;

                var typesInNamespace = assembly
                    .GetExportedTypes()
                    .Where(t => t.Namespace == viewNamespace)
                    .ToList();

                var viewModelType = typesInNamespace.FirstOrDefault(t =>
                    typeof(ViewModelBase).IsAssignableFrom(t));

                return viewModelType;
            });
        }

        private static void InitializeStaticServices(IServiceProvider provider)
        {
            StaticProvider.Initialize(provider);
            ErrorHandler.Initialize();
        }

        // private void BigDick()
        // {
        //     var serializer = ServiceProvider.GetService<IJsonSerializationService>();
        //
        //     var eventState = new EnduranceEvent("name", "place", "BUL");
        //     var president = new Personnel("Pesho Goshov", PersonnelRole.PresidentGroundJury);
        //     var steward1 = new Personnel("Stew Stew", PersonnelRole.Steward);
        //     var steward2 = new Personnel("Two two", PersonnelRole.Steward);
        //     eventState.Add(president);
        //     eventState.Add(steward1);
        //     eventState.Add(steward2);
        //     var horse1 = new Horse("feiId", "name", "breed", "club");
        //     var horse2 = new Horse("feiId2", "name2", "breed", "club");
        //     var athlete1 = new Athlete("feiId", "name1", "name2", "country", Category.Kids);
        //     var athlete2 = new Athlete("feiId", "name1", "name2", "country", Category.Adults);
        //     var participant1 = new Participant(horse1, athlete1, 10, "rfId", 16);
        //     var participant2 = new Participant(horse2, athlete2, 11, "rfId", 16);
        //     // eventState.Add(participant1);
        //     // eventState.Add(participant2);
        //
        //     var competition1 = new Competition(CompetitionType.National, "Name", DateTime.Now.AddDays(1));
        //     eventState.Add(competition1);
        //     participant1.ParticipateIn(competition1);
        //     participant2.ParticipateIn(competition1);
        //
        //     var phase1 = new Phase(false, 10, 15, 10, 20);
        //     competition1.Add(phase1);
        //     var phaseEntry1 = new PhaseEntry(phase1, competition1.StartTime);
        //     var phaseEntry2 = new PhaseEntry(phase1, competition1.StartTime);
        //     participant1.Participation.PhaseEntries.Add(phaseEntry1);
        //     participant2.Participation.PhaseEntries.Add(phaseEntry2);
        //
        //     var phase2 = new Phase(true, 20, 30, 10, 20);
        //     competition1.Add(phase2);
        //
        //     var competition2 = new Competition(CompetitionType.International, "Name", DateTime.Now.AddDays(1));
        //     eventState.Add(competition2);
        //     participant1.ParticipateIn(competition2);
        //
        //     var phase3 = new Phase(false, 10, 15, 10, 20);
        //     competition2.Add(phase3);
        //     var phaseEntry3 = new PhaseEntry(phase1, competition1.StartTime);
        //     participant1.Participation.PhaseEntries.Add(phaseEntry3);
        //
        //     var phase4 = new Phase(true, 20, 30, 10, 20);
        //     competition2.Add(phase4);
        //
        //     var json = serializer.Serialize(eventState);
        //     var result = serializer.Deserialize<EnduranceEvent>(json);
        // }
    }
}
