using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Maanaim.Model
{
    public class Calendar
    {
        [BsonId()]
        public ObjectId Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("StartDate")]
        public DateTime StartDate { get; set; }

        [BsonElement("EndDate")]
        public DateTime EndDate { get; set; }

        [BsonElement("Ministry")]
        public string Ministry { get; set; }

        [BsonElement("BackgroundColor")]
        public string BackgroundColor { get; set; }

        [BsonElement("TextColor")]
        public string TextColor { get; set; }
    }
}
