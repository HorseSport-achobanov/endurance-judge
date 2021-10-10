using EnduranceJudge.Application.Aggregates.Configurations.Contracts;
using EnduranceJudge.Domain.Aggregates.Configuration;
using EnduranceJudge.Domain.Enums;
using EnduranceJudge.Domain.State.Personnels;
using EnduranceJudge.Gateways.Desktop.Core.Components.Templates.SimpleListItem;
using EnduranceJudge.Gateways.Desktop.Core.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace EnduranceJudge.Gateways.Desktop.Views.Content.Event.Children.Personnel
{
    public class PersonnelViewModel : FormBase<PersonnelView, Domain.State.Personnels.Personnel>, IPersonnelState
    {
        private PersonnelViewModel() : this(null) {}
        public PersonnelViewModel(IQueries<Domain.State.Personnels.Personnel> personnel) : base(personnel)
        {
        }

        public ObservableCollection<SimpleListItemViewModel> RoleItems { get; }
            = new(SimpleListItemViewModel.FromEnum<PersonnelRole>());

        private string name;
        private int roleId;

        protected override void ActOnSubmit()
        {
            var configuration = new ConfigurationManager();
            configuration.Save(this);
        }

        public string Name
        {
            get => this.name;
            set => this.SetProperty(ref this.name, value);
        }
        public int RoleId
        {
            get => this.roleId;
            set => this.SetProperty(ref this.roleId, value);
        }

        public PersonnelRole Role => (PersonnelRole)this.RoleId;
        public string RoleName => this.RoleItems.First(x => x.Id == this.RoleId).Name;
    }
}
