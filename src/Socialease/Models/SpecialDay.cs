using System;

namespace Socialease.Models
{
    public class SpecialDay
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public DateTime Created { get; set; }
        public string Description { get; set; }
    }
}