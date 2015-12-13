using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OnlineVocabulary.Domain.Entities
{
    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly IMongoClient client;
        private readonly IMongoDatabase database;
        private readonly IMongoCollection<T> collection;

        public  MongoRepository(string ConnectionString, string DatabaseName)
        {
            client = new MongoClient(ConnectionString);
            database = client.GetDatabase(DatabaseName);
            collection = database.GetCollection<T>(typeof(T).Name);
        }


        #region IRepository Members



        public T GetById(Guid Id) 
        {
            var result = collection.AsQueryable()
                            .FirstOrDefault(x => x.Id.Equals(Id));
            return result;
        }

        public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate) 
        {
            var result = collection.AsQueryable()
                            .Where(predicate).AsQueryable();
            return result;
        }

        public IQueryable<T> GetMatchesBy(string keyFilter, params Expression<Func<T, string>>[] keySelectors)
        {
            var result = Enumerable.Empty<T>().AsQueryable();
            foreach (var keySelector in keySelectors)
            {
                var query = collection.AsQueryable().Matches(keyFilter,keySelector);
                result = result.Union(query);
            }
            return result;
        }

        public IQueryable<T> GetAll()
        {
            var result = collection.AsQueryable()
                            .Where(x => x.Id != default(Guid)).AsQueryable();
            return result;
        }

        void Insert(T entity)
        {
            Task.Run(async () => 
            {
                await collection.InsertOneAsync(entity);
            });
        }

        void InsertMany(IEnumerable<T> entitys) 
        {
            Task.Run(async () =>
            {
                await collection.InsertManyAsync(entitys);
            });
        }

        void Update(T entity) 
        {
            Task.Run(async () =>
            {
                await collection.ReplaceOneAsync(x => x.Id.Equals(entity.Id), entity);
            });
        }

        void Delete(T entity) 
        {
            Task.Run(async () =>
            {
                await collection.DeleteOneAsync(x => x.Id.Equals(entity.Id));
            });
        }

        public PaginatedList<T> Paginate<TKey>(
            int pageIndex, int pageSize,
            Expression<Func<T, TKey>> keySelector) 
        {
            return Paginate(pageIndex, pageSize, keySelector, null);
        }

        public PaginatedList<T> Paginate<TKey>(
            int pageIndex, int pageSize,
            Expression<Func<T, TKey>> keySelector,
            Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = collection.AsQueryable()
                                    .AsQueryable().OrderBy(keySelector);

            query = (predicate == null)
                ? query
                : query.Where(predicate);

            return query.ToPaginatedList(pageIndex, pageSize);
        }

        #endregion

    
    }
}
