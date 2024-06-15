namespace server.Entities;

using System.ComponentModel.DataAnnotations;

public class User
{
    public User(string sub, string email)
    {
        Sub = sub;
        Email = email;
    }
    
    [Key]
    [Required]
    [MaxLength(20)]
    public string Sub { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; }
}
