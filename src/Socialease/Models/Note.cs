using System;

namespace Socialease.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string UserName { get; set; }
        public int PersonId { get; set; }
    }
}