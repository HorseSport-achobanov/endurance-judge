using EnduranceJudge.Core.Models;
using EnduranceJudge.Gateways.Desktop.Core.Components.Templates.SimpleListItem;
using EnduranceJudge.Gateways.Desktop.Services;
using Prism.Commands;
using System.Collections.Generic;
using ServiceProvider = EnduranceJudge.Gateways.Desktop.Core.Static.ServiceProvider;

namespace EnduranceJudge.Gateways.Desktop.Core.ViewModels
{
    public abstract class FormBase<TView> : ViewModelBase, IIdentifiable
        where TView : IView
    {
        protected FormBase()
        {
            this.Navigation = ServiceProvider.GetService<INavigationService>();
            this.BoolItems = SimpleListItemViewModel.FromBool();
            this.NavigateToUpdate = new DelegateCommand(
                () => Navigation.ChangeTo<TView>(new NavigationParameter(DesktopConstants.DataParameter, this)));
        }

        protected INavigationService Navigation { get; }

        public DelegateCommand NavigateToUpdate { get; }
        public List<SimpleListItemViewModel> BoolItems { get; }
        private readonly int id;

        public int Id
        {
            get => this.id;
            init => this.SetProperty(ref this.id, value);
        }

        public bool Equals(IIdentifiable identifiable)
        {
            if (identifiable == null)
            {
                return false;
            }
            if (this.Id != default &&  identifiable.Id != default)
            {
                return this.Id == identifiable.Id;
            }

            return false;
        }

        public override bool Equals(object other)
        {
            if (this.Equals(other as IIdentifiable))
            {
                return true;
            }

            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            // Do not snapshot this as Id can change (not readonly).
            return (base.GetHashCode().ToString() + this.Id).GetHashCode();
        }
    }
}
