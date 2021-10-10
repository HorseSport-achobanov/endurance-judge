using EnduranceJudge.Domain.Core.Exceptions;
using EnduranceJudge.Core.Mappings;
using EnduranceJudge.Domain.Core.Models;
using System.Collections.Generic;
using System.Linq;
using static EnduranceJudge.Localization.Strings.Domain;

namespace EnduranceJudge.Domain.Core.Extensions
{
    public static class DomainObjectExtensions
    {
        public static void RemoveDomain<T>(this ICollection<T> collection, T model)
            where T : IDomainObject
        {
            if (model == null)
            {
                throw new DomainException(CannotRemoveNullItemTemplate);
            }

            if (!collection.Contains(model))
            {
                throw new DomainException(CannotRemoveItemIsNotFoundTemplate, model.GetType().Name);
            }

            collection.Remove(model);
        }

        public static void AddDomain<T>(this ICollection<T> collection, T model)
            where T : IDomainObject
        {
            if (model == null)
            {
                throw new DomainException(CannotAddNullItemTemplate);
            }

            if (collection.Contains(model))
            {
                throw new DomainException(CannotAddItemExistsTemplate, model.GetType().Name);
            }

            collection.Add(model);
        }

        public static void Save<T>(this ICollection<T> collection, T domainObject)
            where T : IDomainObject
        {
            if (domainObject == null)
            {
                throw new DomainException(CannotAddNullItemTemplate);
            }

            if (collection.Contains(domainObject))
            {
                var match = collection.MatchDomain(domainObject);
                match.MapFrom(domainObject);
                return;
            }

            collection.Add(domainObject);
        }

        public static T MatchDomain<T>(this IEnumerable<T> collection, T domainObject)
            where T : IDomainObject
        {
            var result = collection.FirstOrDefault(x => x.Id == domainObject.Id);
            return result;
        }

        public static T FindDomain<T>(this IEnumerable<T> collection, int id)
            where T : IDomainObject
        {
            var result = collection.FirstOrDefault(x => x.Id == id);
            return result;
        }

        public static bool AnyMatch<T>(this IEnumerable<T> collection, T domainObject)
            where T : IDomainObject
        {
            var result = collection.Any(x => x.Id == domainObject.Id);
            return result;
        }
    }
}
