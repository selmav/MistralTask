using System;
using System.Linq;
using System.Linq.Expressions;

namespace Common
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> DoPaging<T>(this IQueryable<T> source, Pagination pagination)
        {
            if (pagination == null)
            {
                return source;
            }

            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            int takeElements = pagination.PageSize;
            int skipElements = pagination.Page > 0 ? (pagination.Page - 1) * pagination.PageSize : 0;

            pagination.TotalPages = (int)Math.Ceiling(((double)source.Count()) / pagination.PageSize);

            return source is IOrderedQueryable ?
                source.Skip(skipElements).Take(takeElements) :
                source.OrderBy(p => 0).Skip(skipElements).Take(takeElements);
        }

        public static IQueryable<T> DoOrdering<T>(this IQueryable<T> source, Expression<System.Func<T, string>> exp, Direction direction)
        {
            if (exp == null)
            {
                return source;
            }

            return direction == Direction.Asc ? source.OrderBy(exp) : source.OrderByDescending(exp);
        }
    }
}
