using System;
using System.Collections.Generic;

namespace Socialease.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Notes { get; set; }
        public string Relationship { get; set; }
        public string Location { get; set; }
        public int? Priority { get; set; }
        public DateTime Created { get; set; }
        public ICollection<Ping> Pings { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<SpecialDay> SpecialDays { get; set; }
    }
}
