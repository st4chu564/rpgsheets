using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc; 
using RPGSheets.Models;
using MongoDB.Bson;
using RPGSheets.Interfaces;
using System.Threading.Tasks;
using System;
using System.IO;
using Newtonsoft.Json;
using System.Linq;

namespace RPGSheets.Controllers
{
    [Produces("application/json")]
    [Route("api/rpg/characters")]
    public class CharacterAPIController : Controller
    {
        private ICharacterRepository _CharacterRepo;
        private IClassRepository _ClassRepo;
        private IPlayerRepository _PlayerRepo;

        public CharacterAPIController(ICharacterRepository CharacterRepo, IClassRepository ClassRepo, IPlayerRepository PlayerRepo){
            _CharacterRepo = CharacterRepo;
            _ClassRepo = ClassRepo;
            _PlayerRepo = PlayerRepo;
        }

        [HttpGet("playerCharacters")]
        public async Task<IEnumerable<Character>> GetPlayerCharacters(){
            return await _CharacterRepo.GetPlayerCharacters(new ObjectId(Request.Cookies["id"]));
        }

        [HttpPost("addCharacter")]
        public IActionResult AddCharacter([FromBody]Dictionary<string, string> content){
                if(!Extras.CheckLogin(Request.Cookies, _PlayerRepo))
                return Unauthorized();
            string[] jsons = content["jsons"].Split("\"split\":\"line\"");
            Character toAdd = JsonConvert.DeserializeObject<Character>(jsons[0]);
            toAdd.playerId = new ObjectId(Request.Cookies["id"]);
            Dictionary<string, int> classes = JsonConvert.DeserializeObject<Dictionary<string, int>>(jsons[1]);
            Class starterClass = _ClassRepo.GetClass(classes.Keys.First()).Result;
            Dictionary<string, int[]> attributes = JsonConvert.DeserializeObject<Dictionary<string, int[]>>(jsons[2]);
            Armor armor = JsonConvert.DeserializeObject<Armor>(jsons[3]);
            if(!jsons[4].Contains("brak")){
                Shield shield = JsonConvert.DeserializeObject<Shield>(jsons[4]);
                toAdd.shield = shield;
            }
            else{
                toAdd.shield = new Shield();
            }
            toAdd.specialAbilities.AddRange(starterClass.Poziomy[0].specjalne);
            toAdd.throws.Add("wytrwalosc", starterClass.Poziomy[0].rzuty[0]);
            toAdd.throws.Add("refleks", starterClass.Poziomy[0].rzuty[1]);
            toAdd.throws.Add("wola", starterClass.Poziomy[0].rzuty[2]);
            toAdd.classes = classes;
            toAdd.attributes = attributes;
            toAdd.armor = armor;
            toAdd.protective1 = new ProtectiveItem();
            toAdd.protective2 = new ProtectiveItem();

            _CharacterRepo.AddCharacter(toAdd);
            return Ok();
        }

        [HttpPut("levelCharacter")]
        public async Task LevelCharacter([FromBody]Dictionary<string, string> values){
            try{
                Character leveled = await _CharacterRepo.GetCharacter(new ObjectId(values["character"]));
                Class uped = await _ClassRepo.GetClass(values["class"]);
                int level = leveled.classes[values["class"]]++;
                leveled.level++;
                if(leveled.classes.Keys.Contains(values["class"])){
                    leveled.basicAttackBonus += uped.Poziomy[level].bazowa - uped.Poziomy[level - 1].bazowa;
                    leveled.throws["wytrwalosc"] += uped.Poziomy[level].rzuty[0] - uped.Poziomy[level - 1].rzuty[0];
                    leveled.throws["refleks"] += uped.Poziomy[level].rzuty[1] - uped.Poziomy[level - 1].rzuty[1];                
                    leveled.throws["wola"] += uped.Poziomy[level].rzuty[2] - uped.Poziomy[level - 1].rzuty[2];
                    leveled.specialAbilities.AddRange(uped.Poziomy[level].specjalne);
                }
                else{
                    leveled.classes.Add(uped.Klasa, 1);
                    leveled.basicAttackBonus += uped.Poziomy[level].bazowa;
                    leveled.throws["wytrwalosc"] += uped.Poziomy[level].rzuty[0];
                    leveled.throws["refleks"] += uped.Poziomy[level].rzuty[1];                
                    leveled.throws["wola"] += uped.Poziomy[level].rzuty[2];
                    leveled.specialAbilities.AddRange(uped.Poziomy[level].specjalne);
                }
                await _CharacterRepo.UpdateCharacter(leveled);
                return;
            }
            catch(Exception ex){
                throw ex;
            }
        }

        [HttpGet("getCharacter")]
        public async Task<Character> GetCharacter(string id){
            return await _CharacterRepo.GetCharacter(new ObjectId(id));
        }
    }
}
