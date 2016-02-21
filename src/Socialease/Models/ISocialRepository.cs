using System.Collections.Generic;

namespace Socialease.Models
{
    public interface ISocialRepository
    {
        bool SaveAll();

        IEnumerable<PingType> GetAllPingTypes();
        void AddPingType(PingType pingType);

        Person GetPersonById(int id, string name);
        void AddPerson(Person person);
        void UpdatePerson(Person person);
        IEnumerable<Person> GetUserPeople(string name);

        Note GetNoteById(int id, string name);
        void AddNote(Note note);
        void UpdateNote(Note note);
        IEnumerable<Note> GetUserNotes(int personId, string name);

        IEnumerable<Ping> GetAllPings(int personId, string name);
        void AddPing(Ping ping);
        Ping FindPing(int id, string name);
        Ping RemovePing(int id, string name);
        void UpdatePing(Ping ping);
    }
}