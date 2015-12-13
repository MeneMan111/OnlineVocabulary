using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineVocabulary.Domain.Entities
{
    public interface IRepository<T> where T : class, IEntity
    {
        T GetById(Guid Id);

        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);

        IQueryable<T> GetMatchesBy<TKey>(
            TKey keyFilter, 
            params Expression<Func<T, TKey>>[] keySelectors);

        IQueryable<T> GetAll();

        void Insert(T entity);

        void InsertMany(IEnumerable<T> entitys);

        void Update(T entity);

        void Delete(T entity);

        PaginatedList<T> Paginate<TKey>(
            int pageIndex, int pageSize,
            Expression<Func<T, TKey>> keySelector);

        PaginatedList<T> Paginate<TKey>(
            int pageIndex, int pageSize,
            Expression<Func<T, TKey>> keySelector,
            Expression<Func<T, bool>> predicate);

    }
}
