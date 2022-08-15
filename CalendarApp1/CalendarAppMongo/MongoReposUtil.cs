using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MongoRepos
{
    public class MongoReposUtil
    {

        private readonly string _connectionString;
        private readonly IMongoDatabase _database = null;

        public MongoReposUtil()
        //: this(Util.GetDefaultConnectionString())
        {
            this._connectionString = "mongodb://127.0.0.1:27017";// "mongodb://localhost/CalendarDb"; //;connectionString;  "mongodb://127.0.0.1:27017"
        }

        public MongoReposUtil(string connectionString)
        {
            this._connectionString = "mongodb://127.0.0.1:27017";// "mongodb://localhost/CalendarDb"; //;connectionString;   "mongodb://127.0.0.1:27017"
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return Util.GetCollectionFromConnectionString<TEntity>(_connectionString);
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return Util.GetCollectionFromConnectionString<T>(_connectionString, collectionName);
        }

        public virtual TEntity GetById<TEntity>(string id)
        {
            var collection = GetCollection<TEntity>();
            return collection.Find<TEntity>(Builders<TEntity>.Filter.Eq<BsonValue>("_id", BsonValue.Create(id))).Single();
        }

        public virtual TEntity Get<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            var collection = GetCollection<TEntity>();
            return collection.AsQueryable<TEntity>().FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> GetAll<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            var collection = GetCollection<TEntity>();
            return collection.Find(predicate).ToEnumerable();
        }

        public virtual TEntity Add<TEntity>(TEntity entity)
        {
            var collection = GetCollection<TEntity>();
            collection.InsertOne(entity);
            return entity;
        }

        public virtual void AddAll<TEntity>(IEnumerable<TEntity> entities)
        {
            var collection = GetCollection<TEntity>();
            collection.InsertMany(entities);
        }

        public virtual TEntity Update<TEntity>(Expression<Func<TEntity, bool>> predicate, TEntity entity)
        {
            var collection = GetCollection<TEntity>();
            collection.ReplaceOne<TEntity>(predicate, entity, new UpdateOptions { IsUpsert = true });
            return entity;
        }

        public CalendarMongo Update1(CalendarMongo cal)
        {
                var collection = GetCollection<CalendarMongo>();
                var filter = Builders<CalendarMongo>.Filter.Eq("eventId", cal.eventId);

            var update = Builders<CalendarMongo>.Update
                        .Set(u => u.start, cal.start) // update 1
                        .Set(u => u.end, cal.end) // update 2
                        .Set(u => u.title, cal.title) // update 1
                        .Set(u => u.allDay, cal.allDay) // update 2
                        .Set(u => u.eventId, cal.eventId);
            var res = collection.UpdateOne(filter, update);
            return cal;
        }

            public async Task UpdateOne<TEntity, TField>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TField>> field, TField value)
        {
            var collection = GetCollection<TEntity>();
            var update = Builders<TEntity>.Update.Set(field, value);
            await collection.UpdateOneAsync<TEntity>(predicate, update);
        }

        public virtual void Delete<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            var collection = GetCollection<TEntity>();
            collection.DeleteOne<TEntity>(predicate);
        }
        public virtual void DeleteAll<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            var collection = GetCollection<TEntity>();
            collection.DeleteMany<TEntity>(predicate);
        }

        public virtual long Count<TEntity>()
        {
            var collection = GetCollection<TEntity>();
            return collection.Count(Builders<TEntity>.Filter.Empty);
        }

        public virtual long Max<TEntity>(string field)
        {
            var collection = GetCollection<TEntity>();

            var param = Expression.Parameter(typeof(TEntity));
            var body = Expression.Convert(Expression.Property(param, field), typeof(object));
            var l = Expression.Lambda<Func<TEntity, object>>(body, param);


            var maxObject = collection.Find(x => true).SortByDescending(l).Limit(1).FirstOrDefault();

            if (maxObject == null) return 0;


            Type t = maxObject.GetType();
            return (long)t.GetProperty(field).GetValue(maxObject);
        }

        public virtual TEntity Last<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            var collection = GetCollection<TEntity>();
            var doc = collection.Find(predicate).Sort(Builders<TEntity>.Sort.Descending("_id")).Limit(1);
            return doc.FirstOrDefault();
        }

        public virtual TEntity First<TEntity>(Expression<Func<TEntity, bool>> predicate)
        {
            var collection = GetCollection<TEntity>();
            var doc = collection.Find(predicate).Sort(Builders<TEntity>.Sort.Ascending("_id")).Limit(1);
            return doc.FirstOrDefault();
        }

        public IEnumerable<TEntity> GetAll<TEntity>(string collectionName, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            var collection = Util.GetCollectionFromConnectionString<TEntity>(_connectionString, collectionName);
            return collection.Find(predicate).ToEnumerable();
        }

        public void AddToCollection<TEntity>(string collectionName, TEntity entity)
        {
            var collection = Util.GetCollectionFromConnectionString<TEntity>(_connectionString, collectionName);
            collection.InsertOne(entity);
        }

        public IEnumerable<T> GetCollection<T>(string collectionName, Expression<Func<T, bool>> predicate)
        {
            var collection = Util.GetCollectionFromConnectionString<T>(_connectionString, collectionName);
            return collection.Find(predicate).ToEnumerable();
        }


    }
}
