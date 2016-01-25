using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace Socialease.Models
{
    public class SocialContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Ping> Pings { get; set; }
        public DbSet<PingType> PingTypes { get; set; }
        public DbSet<SpecialDay> SpecialDays { get; set; }
    }
}
