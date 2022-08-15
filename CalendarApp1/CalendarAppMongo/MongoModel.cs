using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoRepos
{

    public class CalendarMongo
        {
            [BsonId]
            [BsonElement(elementName: "_id")]
            [BsonRepresentation(BsonType.ObjectId)]
            public string Id { get; set; }
            public string eventId { get; set; }
            public string title { get; set; }
            public DateTime start { get; set; }
            public DateTime end { get; set; }
            public bool allDay { get; set; }

    }
    
}
