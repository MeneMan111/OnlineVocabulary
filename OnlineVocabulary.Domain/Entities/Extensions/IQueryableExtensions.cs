using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace OnlineVocabulary.Domain.Entities
{
    public static class IQueryableExtensions
    {

        public static PaginatedList<T> ToPaginatedList<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {

            var totalCount = query.Count();
            var collection = query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return new PaginatedList<T>(pageIndex, pageSize, totalCount, collection);
        }

        public static IQueryable<T> Matches<T>(
            this IQueryable<T> query, string keyFilter, 
            Expression<Func<T, string>> keySelector)
        {

            Func<T, string> deleg = keySelector.Compile();
            var result = query.Where(t => Regex.IsMatch(deleg(t), keyFilter));
            return result;
        }
    }
}
