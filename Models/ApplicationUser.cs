using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace BridalBlueprint.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; }
        public ICollection<WeddingUser> WeddingUsers { get; set; }
    }
}

