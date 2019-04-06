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
    public class Character
    {        
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId InternalId { get; set; } 
        
        [BsonElement("player")]
        public ObjectId playerId { get; set; }

        [BsonElement("characterName")]
        public string characterName { get; set; }

        
        [BsonElement("level")]
        public int level { get; set; }

        
        [BsonElement("classes")]
        public Dictionary <string, int> classes { get; set; }

        
        [BsonElement("age")]
        public int age { get; set; }

        
        [BsonElement("race")]
        public string race { get; set; }

        
        [BsonElement("sex")]
        public string sex { get; set; }

        
        [BsonElement("eyesColor")]
        public string eyesColor { get; set; }
        
        
        [BsonElement("hairColor")]
        public string hairColor { get; set; }
        
        
        [BsonElement("height")]
        public double height { get; set; }

        
        [BsonElement("alignment")]
        public string alignment { get; set; }
        
        
        [BsonElement("special")]
        public List<string> special { get; set; }
        
        
        [BsonElement("attributes")]
        public Dictionary<string, int[]> attributes { get; set; }
        
        
        [BsonElement("throws")]
        public Dictionary<string, int> throws { get; set; }
        
        
        [BsonElement("basicAttackBonus")]
        public int basicAttackBonus { get; set; }
        
        
        [BsonElement("hitpoints")]
        public int hitpoints { get; set; }
        
        
        [BsonElement("armorClass")]
        public int armorClass { get; set; }
        
        
        [BsonElement("grapple")]
        public int grapple { get; set; }
        
        
        [BsonElement("initiative")]
        public int initiative { get; set; }
        
        
        [BsonElement("skills")]
        public Dictionary<string, int> skills { get; set; }
        
        
        [BsonElement("campaignName")]
        public string campaignName { get; set; }
        
        
        [BsonElement("feats")]
        public List<string> feats { get; set; }
        
        
        [BsonElement("specialAbilities")]
        public List<string> specialAbilities { get; set; }
        
        
        [BsonElement("spells")]
        public Dictionary<string, string[]> spells { get; set; }
        
        
        [BsonElement("armor")]
        public Armor armor { get; set; }
        
        
        [BsonElement("shield")]
        public Shield shield { get; set; }
        
        
        [BsonElement("protectiveItem1")]
        public ProtectiveItem protective1 { get; set; }
        
        
        [BsonElement("protectiveItem2")]
        public ProtectiveItem protective2 { get; set; }   
        
        
        [BsonElement("money")]
        public Dictionary<string, int> money { get; set; }
        
        
        [BsonElement("possesion")]
        public Possesion[] equipment { get; set; }

        public Character(){
            playerId = new ObjectId();
            characterName = "testowa";
            level = 5;
            classes = new Dictionary<string, int>();
            age = 150;
            race = "Elf";
            sex = "M";
            eyesColor = "blue";
            hairColor = "blond";
            height = 1.8;
            alignment = "neutral";
            special = new List<string>();
            attributes = new Dictionary<string, int[]>();
            throws = new Dictionary<string, int>();
            basicAttackBonus = 11;
            hitpoints = 100;
            armorClass = 10;
            grapple = 10;
            initiative = 15;
            skills = new Dictionary<string, int>();
            campaignName = "Testowa kampania";
            feats = new List<string>();
            specialAbilities = new List<string>();
            spells = new Dictionary<string, string[]>();
            armor = new Armor();
            shield = new Shield();
            protective1 = new ProtectiveItem();
            protective2 = new ProtectiveItem();
            money = new Dictionary<string, int>();
            equipment = null;
        }
    }

    public class ProtectiveItem{
        
        [BsonElement("protectiveItemName")]
        public string name { get; set; }

        
        [BsonElement("acBonus")]
        public int acBonus { get; set; }
        
        
        [BsonElement("protectiveItemWeight")]
        public double weight { get; set; }
        
        
        [BsonElement("specialProperties")]
        public string[] specialProperties { get; set; }

        public ProtectiveItem(){
            name = "";
            acBonus = 0;
            weight = 0.0;
            specialProperties = new string[] {};
        }
    }

    public class Shield : ProtectiveItem{
        
        [BsonElement("checkPenalty")]
        public int checkPenalty { get; set; }
        
        
        [BsonElement("spellFailure")]
        public double spellFailure { get; set; }

        public Shield() : base(){
            checkPenalty = 0;
            spellFailure = 0.0;
        }
    }

    public class Armor : Shield{
        
        [BsonElement("type")]
        public string type { get; set; }
        
        
        [BsonElement("maxDex")]
        public double maxDex { get; set; }
        
        
        [BsonElement("speed")]
        public string speed { get; set; }

        public Armor(): base(){
            speed = "1";
            maxDex = 100.0;
            type = "";
        }
    }

    public class Possesion{
        
        [BsonElement("itemName")]
        public string name { get; set; }
        
        [BsonElement("amount")]
        public int amount { get; set; }

        [BsonElement("weight")]
        public double weight { get; set; }
    }
}