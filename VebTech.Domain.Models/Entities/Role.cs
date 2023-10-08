namespace VebTech.Domain.Models.Entities;

public class Role
{
    public long RoleId { get; set; }
    
    public string Name { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}