using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPGSheets.Models;

namespace RPGSheets.Interfaces{

    public interface ISessionRepository
    {
        Task<string> AddSession(Session toAdd);
        Task<Session> GetSession(ObjectId sessionId);
        Task<List<Session>> GetPlayerSessions(ObjectId playerId);
        Task<bool> AddPlayerToSession(Session toUpdate);
        Task<bool> DeleteSession(ObjectId sessionId);


    }

    public class SessionRepository : ISessionRepository
    {
        private readonly SessionContext _context = null;

        public SessionRepository(IOptions<Settings> settings)
        {
            _context = new SessionContext(settings);
        }
        
        public async Task<string> AddSession(Session toAdd){
            try{
                await _context.Session.InsertOneAsync(toAdd);
                return toAdd.InternalId.ToString();
            }
            catch(Exception ex){
                throw ex;
            }
        }

        public async Task<Session> GetSession(ObjectId sessionId){
            try{
                return await _context.Session.Find(x => x.InternalId == sessionId)
                            .FirstOrDefaultAsync();
            }
            catch(Exception ex){
                throw ex;
            }
        }

        public async Task<List<Session>> GetPlayerSessions(ObjectId playerId){
            try{
                string stringedId = playerId.ToString();
                return await _context.Session
                            .Find(x => x.playersList.ContainsKey(stringedId) || x.sessionMaster == playerId)
                            .ToListAsync();
            }
            catch(Exception ex){
                throw ex;
            }
        }

        public async Task<bool> AddPlayerToSession(Session toUpdate){              
                try{
                    var filter = Builders<Session>.Filter.Eq(s => s.InternalId, toUpdate.InternalId);                    
                    await _context.Session.ReplaceOneAsync(filter, toUpdate);    
                    return true;
                }
                catch(Exception ex){
                    throw ex;
                }
        }

        public async Task<bool> DeleteSession(ObjectId sessionId){
            try
            {
                DeleteResult actionResult
                    = await _context.Session.DeleteOneAsync(
                        Builders<Session>.Filter.Eq("InternalId", sessionId));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}