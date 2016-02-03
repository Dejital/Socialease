using System;
using System.Collections.Generic;

namespace Socialease.Models
{
    public class Ping
    {
        public int Id { get; set; }
        public PingType Type { get; set; }
        public DateTime Date { get; set; }
        public DateTime Created { get; set; }
        public ICollection<Person> People { get; set; }
        public ICollection<Note> Notes { get; set; }
        public string UserName { get; set; }
    }
}