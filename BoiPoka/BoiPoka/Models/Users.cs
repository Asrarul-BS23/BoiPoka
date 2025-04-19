using Microsoft.AspNetCore.Identity;

namespace BoiPoka.Models;

public class Users : IdentityUser
{
    public Cart Cart { get; set; }
    public ICollection<Order> Order { get; set; } = new List<Order>();
    public string FullName { get; set; }
}
