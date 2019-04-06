using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc; 
using RPGSheets.Models;
using MongoDB.Bson;
using RPGSheets.Interfaces;
using System.Threading.Tasks;
 
namespace RPGSheets.Controllers
{
    [Produces("application/json")]
    [Route("api/rpg/sessions")]
    public class SessionAPIController : Controller
    {
        private ISessionRepository _SessionRepo;
        private ICharacterRepository _CharacterRepo;

        public SessionAPIController(ISessionRepository SessionRepo, ICharacterRepository CharacterRepo){
            _SessionRepo = SessionRepo;
            _CharacterRepo = CharacterRepo;
        }
        [HttpGet]
        public string Test(){
            return "udane";
        }

        [HttpGet("getSession")]
        public async Task<Session> Get(string sessionId){
            return await _SessionRepo.GetSession(new ObjectId(sessionId.Substring(1))) ?? new Session();
        }

        [HttpGet("getPlayerSessions")]
        public async Task<List<Session>> GetPlayerSessions(string playerId){
            return await _SessionRepo.GetPlayerSessions(new ObjectId(playerId)) ?? new List<Session>();
        }

        [HttpPost("createSession")]
        public async Task<string> AddSession([FromBody]Dictionary<string, string> content){
            Session toAdd = new Session(content["sessionName"], new ObjectId(Request.Cookies["id"]));            
            return await _SessionRepo.AddSession(toAdd);
        }

        [HttpPut("addToSession/{characterId}/{sessionId}")]
        public async Task<bool> AddCharacterToSession(string characterId, string sessionId){
            sessionId = sessionId.Substring(1);
            characterId = characterId.Substring(1);
            Session toUpdate = await _SessionRepo.GetSession(new ObjectId(sessionId));
            if(toUpdate == null){
                return false;
            }
            toUpdate.playersList.Add(Request.Cookies["id"], characterId);
            if(string.IsNullOrEmpty(toUpdate.sessionMaster.ToString())){
                toUpdate.sessionMaster = new ObjectId(Request.Cookies["id"]);
            }
            bool res = await _SessionRepo.AddPlayerToSession(toUpdate);
            if(res){
                Character update = await _CharacterRepo.GetCharacter(new ObjectId(characterId));
                update.campaignName = toUpdate.sessionName;
                await _CharacterRepo.UpdateCharacter(update);
                return true;
            }
            else{                
                return false;
            }
        }


    }
}
