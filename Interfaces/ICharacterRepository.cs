using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPGSheets.Models;

namespace RPGSheets.Interfaces{

    public interface ICharacterRepository
    {
        Task<IEnumerable<Character>> GetAllCharacters();
        Task<List<Character>> GetPlayerCharacters(ObjectId playerId);
        Task<Character> GetCharacter(ObjectId charId);
        Task UpdateCharacter(Character toUpdate);
        Task AddCharacter(Character toAdd);

    }

    public class CharacterRepository : ICharacterRepository
    {
        private readonly CharacterContext _context = null;

        public CharacterRepository(IOptions<Settings> settings)
        {
            _context = new CharacterContext(settings);
        }

        public async Task<IEnumerable<Character>> GetAllCharacters()
        {
            try
            {
                return await _context.Character
                        .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Character> GetCharacter(ObjectId charId)
        {
            try
            {                
                return await _context.Character
                                .Find(doc => doc.InternalId == charId)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Character>> GetPlayerCharacters(ObjectId playerId)
        {
            try{
                return await _context.Character
                                .Find(doc => doc.playerId == playerId)
                                .ToListAsync();
            }
            catch (Exception ex){
                throw ex;
            }
        }

        public async Task UpdateCharacter(Character toUpdate){
            try{
                var filter = Builders<Character>.Filter.Eq(s => s.InternalId, toUpdate.InternalId);
                await _context.Character.ReplaceOneAsync(filter, toUpdate);                
            }
            catch(Exception ex){
                throw ex;
            }
        }

        public async Task AddCharacter(Character toAdd){
            try{
                await _context.Character.InsertOneAsync(toAdd);
                return;
            }
            catch(Exception ex){
                throw ex;
            }
        }
    }

}