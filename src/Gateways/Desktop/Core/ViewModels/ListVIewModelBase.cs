using EnduranceJudge.Application.Contracts;
using EnduranceJudge.Application.Core.Models;
using EnduranceJudge.Gateways.Desktop.Core.Components.Templates.ListItem;
using EnduranceJudge.Gateways.Desktop.Services;
using Prism.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EnduranceJudge.Gateways.Desktop.Core.ViewModels
{
    public abstract class ListViewModelBase<TView> : ViewModelBase
        where TView : IView
    {
        private readonly IDomainHandler domainHandler;
        private readonly IPersistence persistence;
        protected ListViewModelBase(
            INavigationService navigation,
            IDomainHandler domainHandler,
            IPersistence persistence)
        {
            this.domainHandler = domainHandler;
            this.persistence = persistence;
            this.Navigation = navigation;
            this.Create = new DelegateCommand(this.CreateAction);
        }

        protected INavigationService Navigation { get; }

        public ObservableCollection<ListItemViewModel> ListItems { get; protected init; }
            = new (Enumerable.Empty<ListItemViewModel>());

        public DelegateCommand Create { get; }

        public override void OnNavigatedTo(Prism.Regions.NavigationContext context)
        {
            base.OnNavigatedTo(context);
            this.Load();
        }

        protected abstract IEnumerable<ListItemModel> LoadData();
        protected abstract void RemoveDomain(int id);

        protected virtual void CreateAction()
        {
            this.Navigation.ChangeTo<TView>();
        }
        protected virtual void UpdateAction(int? id)
        {
            this.Navigation.ChangeToUpdateForm<TView>(id!.Value);
        }

        protected virtual void Load() => this.domainHandler.Handle(() =>
        {
            var eventsList = this.LoadData();

            var viewModels = eventsList
                .Select(this.ToViewModel)
                .ToList();

            this.ListItems.Clear();
            this.ListItems.AddRange(viewModels);
        });
        protected virtual void RemoveAction(int? id) => this.domainHandler.Handle(() =>
        {
            this.RemoveDomain(id!.Value);
            var item = this.ListItems.FirstOrDefault(i => i.Id == id!.Value);
            this.ListItems.Remove(item);
            this.persistence.Snapshot();
        });

        private ListItemViewModel ToViewModel(ListItemModel listable)
        {
            var update = new DelegateCommand<int?>(this.UpdateAction);
            var remove = new DelegateCommand<int?>(this.RemoveAction);
            return new ListItemViewModel(listable.Id, listable.Name, update, remove);
        }
    }
}
