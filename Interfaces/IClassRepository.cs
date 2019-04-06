using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RPGSheets.Models;

namespace RPGSheets.Interfaces{

    public interface IClassRepository
    {
        Task<IEnumerable<Class>> GetAllClasses();
        Task<Class> GetClass(string name);
    }

    public class ClassRepository : IClassRepository
    {
        private readonly ClassContext _context = null;

        public ClassRepository(IOptions<Settings> settings)
        {
            _context = new ClassContext(settings);
        }

        public async Task<IEnumerable<Class>> GetAllClasses()
        {
            try
            {
                return await _context.Class
                        .Find(_ => true).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // query after Id or InternalId (BSonId value)
        //
        public async Task<Class> GetClass(string name)
        {
            try
            {                
                return await _context.Class
                                .Find(doc => doc.Klasa.ToLower() == name)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }
    }

}