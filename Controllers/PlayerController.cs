using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc; 
using RPGSheets.Models;
using MongoDB.Bson;
using RPGSheets.Interfaces;
using System.Threading.Tasks;
using System.Collections;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.AspNetCore.Cors;
 
namespace RPGSheets.Controllers
{
    [Produces("application/json")]
    [Route("api/rpg/players")]
    public class PlayerAPIController : Controller
    {
        private IPlayerRepository _PlayerRepo;

        public PlayerAPIController(IPlayerRepository PlayerRepo){
            _PlayerRepo = PlayerRepo;
        }

        protected void Set(string key, string value, int? expireTime = 300){
            CookieOptions option = new CookieOptions();  
            option.Path = "/";
            option.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None;
            if (expireTime.HasValue)  
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);  
            else  
                 option.Expires = DateTime.Now.AddMilliseconds(10);   
            Response.Cookies.Append(key, value, option); 
        }
        
        [HttpGet("player")]
        public async Task<Player> Get(string playerId){
            return await _PlayerRepo.FindPlayerById(new ObjectId(playerId)) ?? new Player();
        }

        [HttpPost("login")]
        public async Task<Dictionary<string, string>> Login([FromBody]IDictionary<string, string> data) {
            Dictionary<string, string> response = new Dictionary<string, string>();
            response.Add("result", "");
            var x = await _PlayerRepo.CheckLogin(data["Login"], data["Haslo"]);
            if (x == null)
            {
                Set("login", "Nie znaleziono");
                response["result"] = "Error";
            }
            else
            {
                Set("id", x.InternalId.ToString());
                Set("login", x.login);
                response["result"] = "Sukces";
            }
            return response;
        }

        [HttpPost("dodaj")]
        public async void Add([FromBody]Player toAdd){
            await _PlayerRepo.AddPlayer(toAdd);
            return;
        }
    }
}
