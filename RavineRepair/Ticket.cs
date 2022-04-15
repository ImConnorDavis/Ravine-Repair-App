using System;
using System.Collections.ObjectModel;
using MongoDB.Bson.Serialization.Attributes;

namespace RavineRepair
{
    public class Ticket
    {
        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("Address")]
        public string Address { get; set; }

        [BsonElement("isComplete")]
        public string IsComplete { get; set; }
    }

}
