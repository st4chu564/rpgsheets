using System;
using System.Text;
using RPGSheets.Interfaces;
using Microsoft.AspNetCore.Http;
using RPGSheets.Models;
using MongoDB.Bson;

namespace RPGSheets.Controllers{

    public static class Extras{

        public static string hashPassword(string password){
            var bytes  = new UTF8Encoding().GetBytes(password);
            var hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }

        public static bool CheckLogin(IRequestCookieCollection cookie, IPlayerRepository _PlayerRepo){           
            Player user = _PlayerRepo.FindPlayerById(new ObjectId(cookie["Id"].ToString()))
            .ConfigureAwait(false).GetAwaiter().GetResult();
            if (cookie["Id"] == null || user == null)
            {                
                return false;
            }
            return true;
        }
    }
}