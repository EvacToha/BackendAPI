using System.Text.Json.Serialization;

namespace VebTech.Domain.Models.Entities;

public class User
{
    public long UserId { get; set; }
    
    public string Name { get; set; }
    
    public short Age { get; set; }
    
    public string Email { get; set; }
    
    public ICollection<Role> Roles { get; set; } = new List<Role>();
}