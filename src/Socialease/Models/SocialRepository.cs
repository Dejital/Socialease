﻿using System;
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

        public Person GetPersonById(int id, string name)
        {
            try
            {
                return _context.People
                    .FirstOrDefault(p => p.Id == id && p.UserName == name);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not get person with id {id} from database.", ex);
                throw;
            }
        }

        public void AddPerson(Person person)
        {
            _context.Add(person);
        }

        public void UpdatePerson(Person person)
        {
            _context.People.Update(person);
        }

        public IEnumerable<Person> GetUserPeople(string name)
        {
            try
            {
                return _context.People
                    .Where(p => p.UserName == name)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get people from database.", ex);
                throw;
            }
        }
    }
}
