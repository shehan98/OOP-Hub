using Microsoft.AspNetCore.Identity;

namespace EmpiteIMS.Models.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
