namespace server.Data.Entities;

using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    [Required]
    [MaxLength(20)]
    public string Sub { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public string Email { get; set; }
}
