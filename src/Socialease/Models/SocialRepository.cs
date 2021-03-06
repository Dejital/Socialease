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

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        #region PingTypes

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

        #endregion

        #region People 

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

        #endregion People

        #region Notes

        public Note GetNoteById(int id, string name)
        {
            try
            {
                return _context.Notes
                    .FirstOrDefault(p => p.Id == id && p.UserName == name);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not get note with id {id} from database.", ex);
                throw;
            }
        }

        public void AddNote(Note note)
        {
            try
            {
                _context.Add(note);
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not add new note to the database.", ex);
                throw;
            }
        }

        public void UpdateNote(Note note)
        {
            try
            {
                _context.Notes.Update(note);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not update note with id {note.Id} in the database.", ex);
            }
        }

        public IEnumerable<Note> GetUserNotes(int personId, string name)
        {
            try
            {
                return _context.Notes
                    .Where(n => n.UserName == name && n.PersonId == personId)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get notes from database.", ex);
                throw;
            }
        }

        #endregion Notes

        #region Pings

        public IEnumerable<Ping> GetAllPings(int personId, string name)
        {
            try
            {
                return _context.Pings.Where(p => p.PersonId == personId && p.UserName == name).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get all pings from the database.", ex);
                throw;
            }
        }

        public void AddPing(Ping ping)
        {
            try
            {
                _context.Add(ping);
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not add new ping to the database.", ex);
                throw;
            }
        }

        public Ping FindPing(int id, string name)
        {
            try
            {
                return _context.Pings
                    .FirstOrDefault(p => p.Id == id && p.UserName == name);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not get ping with id {id} from database.", ex);
                throw;
            }
        }

        public Ping RemovePing(int id, string name)
        {
            try
            {
                var ping = _context.Pings.FirstOrDefault(p => p.Id == id && p.UserName == name);
                if (ping != null)
                {
                    _context.Pings.Remove(ping);
                }
                return ping;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not remove ping with id {id} from database.", ex);
                throw;
            }
        }

        public void UpdatePing(Ping ping)
        {
            try
            {
                _context.Pings.Update(ping);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not update ping with id {ping.Id} from database.", ex);
                throw;
            }
        }

        #endregion Pings
    }
}
