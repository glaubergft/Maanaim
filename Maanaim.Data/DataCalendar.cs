using Maanaim.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace Maanaim.Data
{
    public class DataCalendar
    {
        private string _connectionString;
        public DataCalendar(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Calendar> List()
        {
            IMongoClient client = new MongoClient(_connectionString);
            IMongoDatabase database = client.GetDatabase("MyFirstDatabase");
            IMongoCollection<Calendar> colCalendar = database.GetCollection<Calendar>("MyFirstCollectionName");
            return colCalendar.AsQueryable<Calendar>();
        }

        public void Insert(Calendar calendar)
        {
            IMongoClient client = new MongoClient(_connectionString);
            IMongoDatabase database = client.GetDatabase("MyFirstDatabase");
            IMongoCollection<Calendar> colCalendar = database.GetCollection<Calendar>("MyFirstCollectionName");
            colCalendar.InsertOne(calendar);
        }
    }
}
