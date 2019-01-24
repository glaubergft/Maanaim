using Maanaim.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

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

        public void Insert(Calendar calendar)
        {
            IMongoClient client = new MongoClient(_connectionString);
            IMongoDatabase database = client.GetDatabase(databaseName);
            IMongoCollection<Calendar> colCalendar = database.GetCollection<Calendar>("Calendario");
            colCalendar.InsertOne(calendar);
        }
    }
}
