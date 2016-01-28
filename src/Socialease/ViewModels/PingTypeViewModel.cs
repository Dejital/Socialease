using System;
using System.ComponentModel.DataAnnotations;

namespace Socialease.ViewModels
{
    public class PingTypeViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 1)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
    }
}