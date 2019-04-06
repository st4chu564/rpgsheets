using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

namespace RPGSheets.Models
{
    [BsonIgnoreExtraElements]
    public class Class
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId InternalId { get; set; } 
        [BsonElement("Klasa")]
        public string Klasa { get; set; }       
        [BsonElement("Poziomy")]
        public Level[] Poziomy { get; set;} 
    }

    public class Level{
        [BsonElement("bazowaAtak")]
        public int bazowa;
        [BsonElement("rzuty")]
        public int[] rzuty;
        [BsonElement("Specjalne")]
        public string[] specjalne;
    }
}