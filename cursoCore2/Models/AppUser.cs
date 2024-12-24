using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cursoCore2API.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }

    }
}
