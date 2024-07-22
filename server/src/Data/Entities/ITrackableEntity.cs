namespace server.Data.Entities;

public interface ITrackableEntity
{
    public DateTime CreatedAt { get; }
    
    public DateTime ModifiedAt { get; set; }
}