using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPGSheets.Models;

namespace RPGSheets.Interfaces{

    public interface IPlayerRepository
    {
        Task<Player> CheckLogin(string login, string password);
        Task<Player> FindPlayer(string login);
        Task AddPlayer(Player toAdd);
        Task DeletePlayer(ObjectId toDelete);
        Task<Player> FindPlayerById(ObjectId playerId);
        Task<bool> ChangePassword(string login, string oldPassword, string newPassword);
    }

    public class PlayerRepository : IPlayerRepository
    {
        private readonly PlayerContext _context = null;

        public PlayerRepository(IOptions<Settings> settings)
        {
            _context = new PlayerContext(settings);
        }

        public async Task<Player> CheckLogin(string login, string password){
            try{
                return await _context.Player.Find( x => x.login == login).FirstOrDefaultAsync();
            }
            catch(Exception ex){
                throw ex;
            }
        }

        public async Task<Player> FindPlayer(string login){
            try{
                return await _context.Player.Find( x => x.login == login)
                            .FirstOrDefaultAsync(); 
            }  
            catch(Exception ex){
                throw ex;
            }
        }

        public async Task<Player> FindPlayerById(ObjectId playerId){
            try{
                return await _context.Player.Find(x => x.InternalId == playerId)
                .FirstOrDefaultAsync();
            }
            catch(Exception ex){
                throw ex;
            }
        }

        public async Task AddPlayer(Player toAdd){
            try{
                await _context.Player.InsertOneAsync(toAdd);
                return;
            }
            catch(Exception ex){
                throw ex;
            }
        }

        public async Task DeletePlayer(ObjectId toDelete){
            try
            {
                await _context.Player.DeleteOneAsync(x => x.InternalId == toDelete);
                return;
            }
            catch (Exception ex){
                throw ex;
            }
        }

        public async Task<bool> ChangePassword(string login, string oldPassword, string newPassword){
            try{
                Player toChange = await _context.Player.Find( x => x.login == login 
                && x.password == oldPassword).FirstOrDefaultAsync();
                if(toChange != null){
                    toChange.password = newPassword;
                    return true;
                }
                else{
                    return false;
                }
            }
            catch (Exception ex){
                throw ex;
            }
        }
    }
}