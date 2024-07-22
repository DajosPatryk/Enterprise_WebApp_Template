namespace server.Data.Entities;

using System.ComponentModel.DataAnnotations;
using server.Application.Authorization;

public class User : ITrackableEntity
{
    public DateTime CreatedAt { get; } = DateTime.UtcNow;
    
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    
    [Key]
    [Required]
    public string Sub { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }
}