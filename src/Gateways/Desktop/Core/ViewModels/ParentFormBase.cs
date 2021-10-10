using EnduranceJudge.Application.Aggregates.Configurations.Contracts;
using EnduranceJudge.Domain.Core.Models;

namespace EnduranceJudge.Gateways.Desktop.Core.ViewModels
{
    public abstract class ParentFormBase<TView, TDomain> : FormBase<TView, TDomain>
        where TView : IView
        where TDomain : IDomainObject
    {
        protected ParentFormBase(IQueries<TDomain> queries) : base(queries)
        {
        }

        protected void NewForm<T>()
            where T : IView
        {
            this.Navigation.ChangeToNewForm<T>(this.Id);
        }
    }
}
