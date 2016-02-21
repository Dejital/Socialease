using System;

namespace Socialease.ViewModels
{
    public class NoteViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public int PersonId { get; set; }
    }
}