using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGSheets.Models
{
    public class SessionContext
    {
        private readonly IMongoDatabase _database = null;

        public SessionContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Session> Session
        {
            get
            {
                return _database.GetCollection<Session>("Sessions");
            }
        }
    }
}