using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Serializers;
using Newtonsoft.Json;

namespace RPGSheets.Models
{
    public class Session
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId InternalId { get; set; }

        [BsonElement("playersList")]
        public Dictionary<string, string> playersList { get; set; }

        [BsonElement("sessionMaster")]
        public ObjectId  sessionMaster { get; set; }

        [BsonElement("sessionName")]
        public string sessionName { get; set; }

        [BsonElement("createdDate")]
        public DateTime createdDate { get; set; }

        [BsonElement("finishedDate")]
        public DateTime finishedDate { get; set; }

        public Session(){
            InternalId = new ObjectId();
            playersList = new Dictionary<string, string>();
            sessionName = null;
            sessionMaster = new ObjectId();
            createdDate = DateTime.Now;
        }
        
        public Session(string _sessionName, ObjectId _sessionMaster){
            InternalId = new ObjectId();
            playersList = new Dictionary<string, string>();
            sessionName = _sessionName;
            sessionMaster = _sessionMaster;
            createdDate = DateTime.Now.AddHours(1);
        }
    }
}