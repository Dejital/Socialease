using System.ComponentModel.DataAnnotations;

namespace Socialease.ViewModels
{
    public class PersonViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Notes { get; set; }
        public string Relationship { get; set; }
        public string Location { get; set; }
        public int? Priority { get; set; }
    }
}