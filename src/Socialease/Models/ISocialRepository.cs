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
    }
}