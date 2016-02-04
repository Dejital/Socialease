using System.Collections.Generic;

namespace Socialease.Models
{
    public interface ISocialRepository
    {
        IEnumerable<Person> GetAllPeople();
        IEnumerable<PingType> GetAllPingTypes();
        void AddPingType(PingType pingType);
        bool SaveAll();
        Person GetPersonById(int id, string name);
        void AddPerson(Person person);
        void UpdatePerson(Person person);
        IEnumerable<Person> GetUserPeople(string name);
    }
}