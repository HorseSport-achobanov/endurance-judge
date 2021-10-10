using EnduranceJudge.Application.Aggregates.Configurations.Contracts;
using EnduranceJudge.Application.Contracts;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Core.Utilities;
using EnduranceJudge.Domain.Core.Models;
using EnduranceJudge.Gateways.Desktop.Core.Components.Templates.SimpleListItem;
using EnduranceJudge.Gateways.Desktop.Core.Extensions;
using EnduranceJudge.Gateways.Desktop.Services;
using Prism.Commands;
using System.Collections.Generic;

namespace EnduranceJudge.Gateways.Desktop.Core.ViewModels
{
    public abstract class FormBase<TView, TDomain> : ViewModelBase
        where TView : IView
        where TDomain : IDomainObject
    {
        private readonly IQueries<TDomain> queries;
        private readonly IDomainHandler domainHandler;
        private readonly IPersistence persistence;
        protected INavigationService Navigation { get; }
        protected int? PrincipalId { get; private set; }

        protected FormBase(IQueries<TDomain> queries)
        {
            this.queries = queries;
            this.Navigation = StaticProvider.GetService<INavigationService>();
            this.domainHandler = StaticProvider.GetService<IDomainHandler>();
            this.persistence = StaticProvider.GetService<IPersistence>();

            this.BoolItems = SimpleListItemViewModel.FromBool();

            this.Submit = new DelegateCommand(this.SubmitAction);
            this.NavigateToUpdate = new DelegateCommand(() => Navigation.ChangeToUpdateForm<TView>(this.Id));
        }

        public DelegateCommand Submit { get; }
        public DelegateCommand NavigateToUpdate { get; }

        private readonly int id;
        public List<SimpleListItemViewModel> BoolItems { get; }

        protected abstract void ActOnSubmit();

        public override void OnNavigatedTo(Prism.Regions.NavigationContext context)
        {
            var id = context.GetId();
            if (id.HasValue)
            {
                this.Load(id.Value);
            }
            else
            {
                this.PrincipalId = context.GetPrincipalId();
            }
            base.OnNavigatedTo(context);
        }

        protected virtual void Load(int id)
        {
            var domainObject = this.queries.GetOne(id);
            this.MapFrom(domainObject);
        }
        private void SubmitAction() => this.domainHandler.Handle(() =>
        {
            this.ActOnSubmit();
            this.persistence.Snapshot();
            this.NavigateBackAction();
        });

        public int Id
        {
            get => this.id;
            init => this.SetProperty(ref this.id, value);
        }
    }
}
