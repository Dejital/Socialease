using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Socialease.Models
{
    public class SocialUser : IdentityUser
    {
        public DateTime Created { get; set; }
    }
}