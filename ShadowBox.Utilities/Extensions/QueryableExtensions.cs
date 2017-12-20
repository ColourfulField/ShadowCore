using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using ShadowBox.Utilities.Models;

namespace ShadowBox.Utilities.Extensions
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Returns a page from collection based on pagination options
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">Collection to sort</param>
        /// <param name="baseFilter">Pagination options - page number and page size</param>
        /// <returns></returns>
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, BaseFilter baseFilter)
        {
            baseFilter = baseFilter ?? new BaseFilter { PageNumber = 0, PageSize = 999 };
            baseFilter.PageSize = baseFilter.PageSize <= 0 ? 50 : baseFilter.PageSize;
            return query.Skip(baseFilter.PageSize * baseFilter.PageNumber)
                        .Take(baseFilter.PageSize);
        }

        /// <summary>
        /// Returns the collection ordered by a certain field
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">Collection to order</param>
        /// <param name="baseFilter">Sorting options - field name and sort order</param>
        /// <param name="fieldNameMap">A map between DB entity field names and sort field names</param>
        /// <returns></returns>
        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, BaseFilter baseFilter, Dictionary<string, string> fieldNameMap)
        {
            string sortField = baseFilter.SortField ?? "";

            if (fieldNameMap == null)
            {
                return query;
            }

            if (!fieldNameMap.ContainsKey(sortField))
            {
                sortField = fieldNameMap.First().Value;
            }

            return baseFilter.IsSortDescending
                ? query.OrderBy(sortField)
                : query.OrderBy(sortField + " descending");
        }

        /// <summary>
        /// Combines the effects of ApplySorting and ApplyPagination
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query">Collection to sort and paginate</param>
        /// <param name="baseFilter">Sorting and pagination options</param>
        /// <param name="fieldNameMap">A map between DB entity field names and sort field names</param>
        /// <returns></returns>
        public static IQueryable<T> ApplySortingAndPagination<T>(this IQueryable<T> query, BaseFilter baseFilter,
                                                                 Dictionary<string, string> fieldNameMap)
        {
            return query.ApplySorting(baseFilter, fieldNameMap).ApplyPagination(baseFilter);
        }
    }
}
