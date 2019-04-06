using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc; 
using RPGSheets.Models;
using MongoDB.Bson;
using RPGSheets.Interfaces;
using System.Threading.Tasks;
 
namespace RPGSheets.Controllers
{
    [Produces("application/json")]
    [Route("api/rpg/classes")]
    public class ClassAPIController : Controller
    {
        private IClassRepository _ClassRepo;

        public ClassAPIController(IClassRepository ClassRepo){
            _ClassRepo = ClassRepo;
        }
        [HttpGet]
        public string Test(){
            return "udane";
        }
        [HttpGet("klasa/{name}")]
        public async Task<Class> Get(string name){
            return await _ClassRepo.GetClass(name) ?? new Class();
        }
    }
}
