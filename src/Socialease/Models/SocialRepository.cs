using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Socialease.Models
{
    public class SocialRepository : ISocialRepository
    {
        private readonly SocialContext _context;
        private readonly ILogger<SocialRepository> _logger;

        public SocialRepository(SocialContext context, ILogger<SocialRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Person> GetAllPeople()
        {
            try
            {
                return _context.People.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get people from database.", ex);
                throw;
            }
        }

        public IEnumerable<PingType> GetAllPingTypes()
        {
            try
            {
                return _context.PingTypes.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get ping types from database.", ex);
                throw;
            }
        }

        public void AddPingType(PingType pingType)
        {
            _context.Add(pingType);
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public Person GetPersonById(int id)
        {
            try
            {
                return _context.People.FirstOrDefault(p => p.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not get person with id {id} from database.", ex);
                throw;
            }
        }
    }
}
