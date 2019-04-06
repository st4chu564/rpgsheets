using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;

namespace RPGSheets.Models
{
    [BsonIgnoreExtraElements]
    public class Player
    {  
        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId InternalId { get; set; } 
        [BsonElement("login")]
        public string login { get; set; }
        [BsonElement("password")]
        public string password { get; set; }
        [BsonElement("sessionList")]
        public List<string> sessionList { get; set; }
        [BsonElement("extras")]
        public List<string> extras { get; set; }
    }
}