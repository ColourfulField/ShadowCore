using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using ShadowBox.Utilities.Models;

namespace ShadowBox.Utilities.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, SortOptions sortOptions)
        {
            sortOptions = sortOptions ?? new SortOptions { PageNumber = 0, PageSize = 999 };
            sortOptions.PageSize = sortOptions.PageSize <= 0 ? 50 : sortOptions.PageSize;
            return query.Skip(sortOptions.PageSize * sortOptions.PageNumber)
                        .Take(sortOptions.PageSize);
        }

        public static IQueryable<T> ApplySorting<T>(this IQueryable<T> query, SortOptions sortOptions, Dictionary<string, string> fieldNameMap)
        {
            string sortField = sortOptions.SortField ?? "";

            if (!fieldNameMap.ContainsKey(sortField))
            {
                sortField = fieldNameMap.First().Value;
            }

            return sortOptions.IsSortDescending
                ? query.OrderBy(sortField)
                : query.OrderBy(sortField + " descending");
        }

        public static IQueryable<T> ApplySortingAndPagination<T>(this IQueryable<T> query, SortOptions sortOptions,
                                                                 Dictionary<string, string> fieldNameMap)
        {
            return query.ApplySorting(sortOptions, fieldNameMap).ApplyPagination(sortOptions);
        }
    }
}
