using Microsoft.AspNetCore.Identity;

namespace BoiPoka.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
    }
}
