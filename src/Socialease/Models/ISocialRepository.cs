﻿using System.Collections.Generic;

namespace Socialease.Models
{
    public interface ISocialRepository
    {
        IEnumerable<Person> GetAllPeople();
        IEnumerable<PingType> GetAllPingTypes();
        void AddPingType(PingType pingType);
        bool SaveAll();
    }
}