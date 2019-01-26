using Maanaim.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Maanaim.Data
{
    public class DataCalendar
    {
        private string _connectionString;
        private string databaseName;
        public DataCalendar(string connectionString)
        {
            _connectionString = connectionString;
            databaseName = MongoUrl.Create(_connectionString).DatabaseName;
        }

        public IEnumerable<Calendar> List()
        {
            IMongoClient client = new MongoClient(_connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            IMongoCollection<Calendar> colCalendar = database.GetCollection<Calendar>("Calendario");
            return colCalendar.AsQueryable<Calendar>();
        }

        public Calendar Find(string Id)
        {
            IMongoClient client = new MongoClient(_connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            IMongoCollection<Calendar> colCalendar = database.GetCollection<Calendar>("Calendario");
            Expression<Func<Calendar, bool>> filter = x => x.Id.Equals(ObjectId.Parse(Id));
            return colCalendar.Find(filter).FirstOrDefault();
        }

        public ReplaceOneResult Update(Calendar calendar)
        {
            IMongoClient client = new MongoClient(_connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            IMongoCollection<Calendar> colCalendar = database.GetCollection<Calendar>("Calendario");
            Expression<Func<Calendar, bool>> filter = x => x.Id.Equals(calendar.Id);
            ReplaceOneResult result = colCalendar.ReplaceOne(filter, calendar);
            return result;
        }

        public DeleteResult Delete(Calendar calendar)
        {
            IMongoClient client = new MongoClient(_connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            IMongoCollection<Calendar> colCalendar = database.GetCollection<Calendar>("Calendario");
            Expression<Func<Calendar, bool>> filter = x => x.Id.Equals(calendar.Id);
            DeleteResult result = colCalendar.DeleteOne(filter);
            return result;
        }

        public void Insert(Calendar calendar)
        {
            IMongoClient client = new MongoClient(_connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            IMongoCollection<Calendar> colCalendar = database.GetCollection<Calendar>("Calendario");
            colCalendar.InsertOne(calendar);
        }
    }
}
